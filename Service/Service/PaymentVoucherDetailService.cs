using Core.DomainModel;
using Core.Interface.Repository;
using Core.Interface.Service;
using Core.Interface.Validation;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class PaymentVoucherDetailService : IPaymentVoucherDetailService
    {
        private IPaymentVoucherDetailRepository _repository;
        private IPaymentVoucherDetailValidator _validator;

        public PaymentVoucherDetailService(IPaymentVoucherDetailRepository _paymentVoucherDetailRepository, IPaymentVoucherDetailValidator _paymentVoucherDetailValidator)
        {
            _repository = _paymentVoucherDetailRepository;
            _validator = _paymentVoucherDetailValidator;
        }

        public IPaymentVoucherDetailValidator GetValidator()
        {
            return _validator;
        }

        public IList<PaymentVoucherDetail> GetObjectsByPaymentVoucherId(int paymentVoucherId)
        {
            return _repository.GetObjectsByPaymentVoucherId(paymentVoucherId);
        }

        public IList<PaymentVoucherDetail> GetObjectsByPayableId(int payableId)
        {
            return _repository.GetObjectsByPayableId(payableId);
        }

        public PaymentVoucherDetail GetObjectById(int Id)
        {
            return _repository.GetObjectById(Id);
        }

        public PaymentVoucherDetail CreateObject(PaymentVoucherDetail paymentVoucherDetail, IPaymentVoucherService _paymentVoucherService,
                                                ICashBankService _cashBankService, IPayableService _payableService, IContactService _contactService)
        {
            paymentVoucherDetail.Errors = new Dictionary<String, String>();
            if (_validator.ValidCreateObject(paymentVoucherDetail, _paymentVoucherService, this, _cashBankService, _payableService, _contactService))
            {
                PaymentVoucher paymentVoucher = _paymentVoucherService.GetObjectById(paymentVoucherDetail.PaymentVoucherId);
                paymentVoucherDetail.ContactId = paymentVoucher.ContactId;
                return _repository.CreateObject(paymentVoucherDetail);
            }
            else
            {
                return paymentVoucherDetail;
            }
        }

        public PaymentVoucherDetail CreateObject(int paymentVoucherId, int payableId, decimal amount, string description, 
                                         IPaymentVoucherService _paymentVoucherService, ICashBankService _cashBankService,
                                         IPayableService _payableService, IContactService _contactService)
        {
            PaymentVoucherDetail pvd = new PaymentVoucherDetail
            {
                PaymentVoucherId = paymentVoucherId,
                PayableId = payableId,
                Amount = amount,
                Description = description,
            };
            return this.CreateObject(pvd, _paymentVoucherService, _cashBankService, _payableService, _contactService);
        }

        public PaymentVoucherDetail UpdateObject(PaymentVoucherDetail paymentVoucherDetail, IPaymentVoucherService _paymentVoucherService, ICashBankService _cashBankService, IPayableService _payableService, IContactService _contactService)
        {
            return (_validator.ValidUpdateObject(paymentVoucherDetail, _paymentVoucherService, this, _cashBankService, _payableService, _contactService) ?
                     _repository.UpdateObject(paymentVoucherDetail) : paymentVoucherDetail);
        }

        public PaymentVoucherDetail SoftDeleteObject(PaymentVoucherDetail paymentVoucherDetail)
        {
            return (_validator.ValidDeleteObject(paymentVoucherDetail) ? _repository.SoftDeleteObject(paymentVoucherDetail) : paymentVoucherDetail);
        }

        public bool DeleteObject(int Id)
        {
            return _repository.DeleteObject(Id);
        }

        public PaymentVoucherDetail ConfirmObject(PaymentVoucherDetail paymentVoucherDetail, IPaymentVoucherService _paymentVoucherService,
                                                  ICashBankService _cashBankService, IPayableService _payableService, IContactService _contactService)
        {
            PaymentVoucher pv = _paymentVoucherService.GetObjectById(paymentVoucherDetail.PaymentVoucherId);
            CashBank cb = _cashBankService.GetObjectById(pv.CashBankId);
            if (!cb.IsBank)
            {
                return ClearConfirmObject(paymentVoucherDetail, _paymentVoucherService, _cashBankService, _payableService, _contactService);
            }

            if (_validator.ValidConfirmObject(paymentVoucherDetail, _paymentVoucherService, this, _cashBankService, _payableService))
            {
                Payable payable = _payableService.GetObjectById(paymentVoucherDetail.PayableId);
                payable.PendingClearanceAmount += paymentVoucherDetail.Amount;
                pv.PendingClearanceAmount += paymentVoucherDetail.Amount;
                _paymentVoucherService.UpdateAmount(pv);                
                payable.RemainingAmount -= paymentVoucherDetail.Amount;
                _payableService.UpdateObject(payable);
                paymentVoucherDetail = _repository.ConfirmObject(paymentVoucherDetail);
            }
            return paymentVoucherDetail;
        }

        public PaymentVoucherDetail UnconfirmObject(PaymentVoucherDetail paymentVoucherDetail, IPaymentVoucherService _paymentVoucherService,
                                                    ICashBankService _cashBankService, IPayableService _payableService, IContactService _contactService)
        {
            PaymentVoucher pv = _paymentVoucherService.GetObjectById(paymentVoucherDetail.PaymentVoucherId);
            CashBank cb = _cashBankService.GetObjectById(pv.CashBankId);
            if (!cb.IsBank)
            {
                return UnclearUnconfirmObject(paymentVoucherDetail, _paymentVoucherService, _cashBankService, _payableService, _contactService);
            }
            
            if (_validator.ValidUnconfirmObject(paymentVoucherDetail, _paymentVoucherService, this, _cashBankService, _payableService))
            {
                Payable payable = _payableService.GetObjectById(paymentVoucherDetail.PayableId);
                payable.PendingClearanceAmount -= paymentVoucherDetail.Amount;
                pv.PendingClearanceAmount -= paymentVoucherDetail.Amount;
                _paymentVoucherService.UpdateAmount(pv);
                payable.RemainingAmount += paymentVoucherDetail.Amount;
                _payableService.UpdateObject(payable);
                paymentVoucherDetail = _repository.UnconfirmObject(paymentVoucherDetail);
            }
            return paymentVoucherDetail;
        }

        public PaymentVoucherDetail ClearObject(PaymentVoucherDetail paymentVoucherDetail, IPaymentVoucherService _paymentVoucherService,
                                                ICashBankService _cashBankService, IPayableService _payableService, IContactService _contactService)
        {
            if (_validator.ValidClearObject(paymentVoucherDetail, _paymentVoucherService, this, _cashBankService, _payableService))
            {
                PaymentVoucher pv = _paymentVoucherService.GetObjectById(paymentVoucherDetail.PaymentVoucherId);
                Payable payable = _payableService.GetObjectById(paymentVoucherDetail.PayableId);
                CashBank cb = _cashBankService.GetObjectById(pv.CashBankId);

                cb.Amount -= paymentVoucherDetail.Amount;
                payable.PendingClearanceAmount -= paymentVoucherDetail.Amount;
                pv.PendingClearanceAmount -= paymentVoucherDetail.Amount;

                _cashBankService.UpdateObject(cb);
                _paymentVoucherService.UpdateAmount(pv);
                if (payable.PendingClearanceAmount == 0 && payable.RemainingAmount == 0)
                {
                    payable.IsCompleted = true;
                    payable.CompletionDate = paymentVoucherDetail.ClearanceDate;
                }
                _payableService.UpdateObject(payable);
                paymentVoucherDetail = _repository.ClearObject(paymentVoucherDetail);
            }
            return paymentVoucherDetail;
        }

        public PaymentVoucherDetail UnclearObject(PaymentVoucherDetail paymentVoucherDetail, IPaymentVoucherService _paymentVoucherService,
                                                    ICashBankService _cashBankService, IPayableService _payableService, IContactService _contactService)
        {
            if (_validator.ValidUnclearObject(paymentVoucherDetail, _paymentVoucherService, this, _cashBankService, _payableService))
            {
                PaymentVoucher pv = _paymentVoucherService.GetObjectById(paymentVoucherDetail.PaymentVoucherId);
                Payable payable = _payableService.GetObjectById(paymentVoucherDetail.PayableId);
                CashBank cb = _cashBankService.GetObjectById(pv.CashBankId);

                cb.Amount += paymentVoucherDetail.Amount;
                payable.PendingClearanceAmount += paymentVoucherDetail.Amount;
                pv.PendingClearanceAmount += paymentVoucherDetail.Amount;
                payable.CompletionDate = null;

                _cashBankService.UpdateObject(cb);
                _paymentVoucherService.UpdateAmount(pv);
                _payableService.UpdateObject(payable);
 
                paymentVoucherDetail.ClearanceDate = null;
                paymentVoucherDetail = _repository.UnclearObject(paymentVoucherDetail);
            }
            return paymentVoucherDetail;
        }

        public PaymentVoucherDetail ClearConfirmObject(PaymentVoucherDetail paymentVoucherDetail, IPaymentVoucherService _paymentVoucherService,
                                          ICashBankService _cashBankService, IPayableService _payableService, IContactService _contactService)
        {
            if (_validator.ValidClearConfirmObject(paymentVoucherDetail, _paymentVoucherService, this, _cashBankService, _payableService))
            {
                PaymentVoucher pv = _paymentVoucherService.GetObjectById(paymentVoucherDetail.PaymentVoucherId);
                Payable payable = _payableService.GetObjectById(paymentVoucherDetail.PayableId);
                CashBank cb = _cashBankService.GetObjectById(pv.CashBankId);
                cb.Amount -= paymentVoucherDetail.Amount;
                _cashBankService.UpdateObject(cb);
                paymentVoucherDetail.ClearanceDate = pv.PaymentDate;
                _repository.ClearObject(paymentVoucherDetail);
                payable.RemainingAmount -= paymentVoucherDetail.Amount;
                if (payable.PendingClearanceAmount == 0 && payable.RemainingAmount == 0)
                {
                    payable.IsCompleted = true;
                    payable.CompletionDate = paymentVoucherDetail.ClearanceDate;
                }
                _payableService.UpdateObject(payable);
                paymentVoucherDetail = _repository.ConfirmObject(paymentVoucherDetail);
            }
            return paymentVoucherDetail;
        }

        public PaymentVoucherDetail UnclearUnconfirmObject(PaymentVoucherDetail paymentVoucherDetail, IPaymentVoucherService _paymentVoucherService,
                                                    ICashBankService _cashBankService, IPayableService _payableService, IContactService _contactService)
        {
            if (_validator.ValidUnclearUnconfirmObject(paymentVoucherDetail, _paymentVoucherService, this, _cashBankService, _payableService))
            {
                PaymentVoucher pv = _paymentVoucherService.GetObjectById(paymentVoucherDetail.PaymentVoucherId);
                Payable payable = _payableService.GetObjectById(paymentVoucherDetail.PayableId);
                CashBank cb = _cashBankService.GetObjectById(pv.CashBankId);
                cb.Amount += paymentVoucherDetail.Amount;
                _cashBankService.UpdateObject(cb);
                payable.IsCompleted = false;
                payable.CompletionDate = null;
                paymentVoucherDetail.ClearanceDate = null;
                _repository.UnclearObject(paymentVoucherDetail);
                payable.RemainingAmount += paymentVoucherDetail.Amount;
                _payableService.UpdateObject(payable);
                paymentVoucherDetail = _repository.UnconfirmObject(paymentVoucherDetail);
            }
            return paymentVoucherDetail;
        }

    }
}