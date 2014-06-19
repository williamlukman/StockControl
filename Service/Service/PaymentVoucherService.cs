using Core.DomainModel;
using Core.Interface.Repository;
using Core.Interface.Service;
using Core.Interface.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class PaymentVoucherService : IPaymentVoucherService
    {
        private IPaymentVoucherRepository _repository;
        private IPaymentVoucherValidator _validator;

        public PaymentVoucherService(IPaymentVoucherRepository _paymentVoucherRepository, IPaymentVoucherValidator _paymentVoucherValidator)
        {
            _repository = _paymentVoucherRepository;
            _validator = _paymentVoucherValidator;
        }

        public IPaymentVoucherValidator GetValidator()
        {
            return _validator;
        }

        public IList<PaymentVoucher> GetAll()
        {
            return _repository.GetAll();
        }

        public IList<PaymentVoucher> GetObjectsByCashBankId(int cashBankId)
        {
            return _repository.GetObjectsByCashBankId(cashBankId);
        }

        public PaymentVoucher GetObjectById(int Id)
        {
            return _repository.GetObjectById(Id);
        }

        public IList<PaymentVoucher> GetObjectsByContactId(int contactId)
        {
            return _repository.GetObjectsByContactId(contactId);
        }

        public PaymentVoucher CreateObject(PaymentVoucher paymentVoucher, IPaymentVoucherDetailService _paymentVoucherDetailService,
                                            IPayableService _payableService, IContactService _contactService, ICashBankService _cashBankService)
        {
            paymentVoucher.Errors = new Dictionary<String, String>();
            if (_validator.ValidCreateObject(paymentVoucher, this, _paymentVoucherDetailService, _payableService, _contactService, _cashBankService))
            {
                CashBank cashBank = _cashBankService.GetObjectById(paymentVoucher.CashBankId);
                paymentVoucher.IsInstantClearance = (cashBank.IsBank) ? paymentVoucher.IsInstantClearance : true;
                return _repository.CreateObject(paymentVoucher);
            }
            else
            {
                return paymentVoucher;
            }
        }

        public PaymentVoucher CreateObject(int cashBankId, int contactId, DateTime paymentDate, decimal totalAmount,
                                            IPaymentVoucherDetailService _paymentVoucherDetailService, IPayableService _payableService,
                                            IContactService _contactService, ICashBankService _cashBankService)
        {
            PaymentVoucher pv = new PaymentVoucher
            {
                CashBankId = cashBankId,
                ContactId = contactId,
                PaymentDate = paymentDate,
                TotalAmount = totalAmount,
            };
            return this.CreateObject(pv, _paymentVoucherDetailService, _payableService, _contactService, _cashBankService);
        }

        public PaymentVoucher CreateObject(int cashBankId, int contactId, DateTime paymentDate, decimal totalAmount, bool IsInstantCleareance,
                                    IPaymentVoucherDetailService _paymentVoucherDetailService, IPayableService _payableService,
                                    IContactService _contactService, ICashBankService _cashBankService)
        {
            PaymentVoucher pv = new PaymentVoucher
            {
                CashBankId = cashBankId,
                ContactId = contactId,
                PaymentDate = paymentDate,
                TotalAmount = totalAmount,
                IsInstantClearance = IsInstantCleareance
            };
            return this.CreateObject(pv, _paymentVoucherDetailService, _payableService, _contactService, _cashBankService);
        }

        public PaymentVoucher UpdateObject(PaymentVoucher paymentVoucher, IPaymentVoucherDetailService _paymentVoucherDetailService, IPayableService _payableService, IContactService _contactService, ICashBankService _cashBankService)
        {
            return (_validator.ValidUpdateObject(paymentVoucher, this, _paymentVoucherDetailService, _payableService, _contactService, _cashBankService) ? _repository.UpdateObject(paymentVoucher) : paymentVoucher);
        }

        public PaymentVoucher UpdateAmount(PaymentVoucher paymentVoucher)
        {
            return _repository.UpdateObject(paymentVoucher);
        }

        public PaymentVoucher SoftDeleteObject(PaymentVoucher paymentVoucher, IPaymentVoucherDetailService _paymentVoucherDetailService)
        {
            return (_validator.ValidDeleteObject(paymentVoucher, _paymentVoucherDetailService) ? _repository.SoftDeleteObject(paymentVoucher) : paymentVoucher);
        }

        public bool DeleteObject(int Id)
        {
            return _repository.DeleteObject(Id);
        }

        public PaymentVoucher ConfirmObject(PaymentVoucher paymentVoucher, IPaymentVoucherDetailService _paymentVoucherDetailService,
                                            ICashBankService _cashBankService, IPayableService _payableService, IContactService _contactService)
        {

            if (_validator.ValidConfirmObject(paymentVoucher, this, _paymentVoucherDetailService, _cashBankService, _payableService, _contactService))
            {
                IList<PaymentVoucherDetail> details = _paymentVoucherDetailService.GetObjectsByPaymentVoucherId(paymentVoucher.Id);
                if (paymentVoucher.IsInstantClearance)
                {
                    paymentVoucher.ClearanceDate = paymentVoucher.PaymentDate;
                    _repository.ConfirmObject(paymentVoucher);
                    _repository.ClearObject(paymentVoucher);
                    foreach (var detail in details)
                    {
                        detail.ConfirmedAt = paymentVoucher.ConfirmedAt;
                        detail.ClearanceDate = paymentVoucher.PaymentDate;
                        _paymentVoucherDetailService.ConfirmObject(detail, this, _cashBankService, _payableService, _contactService);
                    }
                }
                else
                {
                    _repository.ConfirmObject(paymentVoucher);
                    foreach (var detail in details)
                    {
                        detail.ConfirmedAt = paymentVoucher.ConfirmedAt;
                        _paymentVoucherDetailService.ConfirmObject(detail, this, _cashBankService, _payableService, _contactService);
                    }
                }
            }
            return paymentVoucher;
        }

        public PaymentVoucher UnconfirmObject(PaymentVoucher paymentVoucher, IPaymentVoucherDetailService _paymentVoucherDetailService,
                                            ICashBankService _cashBankService, IPayableService _payableService, IContactService _contactService)
        {
            if (_validator.ValidUnconfirmObject(paymentVoucher, this, _paymentVoucherDetailService, _cashBankService, _payableService, _contactService))
            {
                IList<PaymentVoucherDetail> details = _paymentVoucherDetailService.GetObjectsByPaymentVoucherId(paymentVoucher.Id);
                if (paymentVoucher.IsInstantClearance)
                {
                    paymentVoucher.ClearanceDate = null;
                    _repository.UnconfirmObject(paymentVoucher);
                    _repository.UnclearObject(paymentVoucher);
                    foreach (var detail in details)
                    {
                        detail.ConfirmedAt = null;
                        detail.ClearanceDate = null;
                        _paymentVoucherDetailService.UnconfirmObject(detail, this, _cashBankService, _payableService, _contactService);
                    }
                }
                else
                {
                    _repository.UnconfirmObject(paymentVoucher);
                    foreach (var detail in details)
                    {
                        _paymentVoucherDetailService.UnconfirmObject(detail, this, _cashBankService, _payableService, _contactService);
                    }
                }
            }
            return paymentVoucher;
        }

        public PaymentVoucher ClearObject(PaymentVoucher paymentVoucher, IPaymentVoucherDetailService _paymentVoucherDetailService,
                             ICashBankService _cashBankService, IPayableService _payableService, IContactService _contactService)
        {
            if (_validator.ValidClearObject(paymentVoucher, this, _paymentVoucherDetailService, _cashBankService, _payableService, _contactService))
            {
                _repository.ClearObject(paymentVoucher);
                IList<PaymentVoucherDetail> details = _paymentVoucherDetailService.GetObjectsByPaymentVoucherId(paymentVoucher.Id);
                foreach (var detail in details)
                {
                    _paymentVoucherDetailService.ClearObject(detail, this, _cashBankService, _payableService, _contactService);
                }
            }
            return paymentVoucher;
        }

        public PaymentVoucher UnclearObject(PaymentVoucher paymentVoucher, IPaymentVoucherDetailService _paymentVoucherDetailService,
                                     ICashBankService _cashBankService, IPayableService _payableService, IContactService _contactService)
        {
            if (_validator.ValidUnclearObject(paymentVoucher, this, _paymentVoucherDetailService, _cashBankService, _payableService, _contactService))
            {
                _repository.UnclearObject(paymentVoucher);
                IList<PaymentVoucherDetail> details = _paymentVoucherDetailService.GetObjectsByPaymentVoucherId(paymentVoucher.Id);
                foreach (var detail in details)
                {
                    _paymentVoucherDetailService.UnclearObject(detail, this, _cashBankService, _payableService, _contactService);
                }
            }
            return paymentVoucher;
        }
    }
}