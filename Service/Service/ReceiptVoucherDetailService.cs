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
    public class ReceiptVoucherDetailService : IReceiptVoucherDetailService
    {
        private IReceiptVoucherDetailRepository _repository;
        private IReceiptVoucherDetailValidator _validator;

        public ReceiptVoucherDetailService(IReceiptVoucherDetailRepository _receiptVoucherDetailRepository, IReceiptVoucherDetailValidator _receiptVoucherDetailValidator)
        {
            _repository = _receiptVoucherDetailRepository;
            _validator = _receiptVoucherDetailValidator;
        }

        public IReceiptVoucherDetailValidator GetValidator()
        {
            return _validator;
        }

        public IList<ReceiptVoucherDetail> GetObjectsByReceiptVoucherId(int receiptVoucherId)
        {
            return _repository.GetObjectsByReceiptVoucherId(receiptVoucherId);
        }

        public IList<ReceiptVoucherDetail> GetObjectsByReceivableId(int receivableId)
        {
            return _repository.GetObjectsByReceivableId(receivableId);
        }

        public ReceiptVoucherDetail GetObjectById(int Id)
        {
            return _repository.GetObjectById(Id);
        }

        public ReceiptVoucherDetail CreateObject(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService,
                                                ICashBankService _cashBankService, IReceivableService _receivableService, IContactService _contactService)
        {
            receiptVoucherDetail.Errors = new Dictionary<String, String>();
            if (_validator.ValidCreateObject(receiptVoucherDetail, _receiptVoucherService, this, _cashBankService, _receivableService, _contactService))
            {
                ReceiptVoucher receiptVoucher = _receiptVoucherService.GetObjectById(receiptVoucherDetail.ReceiptVoucherId);
                CashBank cashBank = _cashBankService.GetObjectById(receiptVoucher.CashBankId);
                receiptVoucherDetail.ContactId = receiptVoucher.ContactId;
                receiptVoucherDetail.IsInstantClearance = (cashBank.IsBank) ? receiptVoucherDetail.IsInstantClearance : true;
                return _repository.CreateObject(receiptVoucherDetail);
            }
            else
            {
                return receiptVoucherDetail;
            }
        }

        public ReceiptVoucherDetail CreateObject(int receiptVoucherId, int receivableId, decimal amount, string description, 
                                         IReceiptVoucherService _receiptVoucherService, ICashBankService _cashBankService,
                                         IReceivableService _receivableService, IContactService _contactService)
        {
            ReceiptVoucherDetail rvd = new ReceiptVoucherDetail
            {
                ReceiptVoucherId = receiptVoucherId,
                ReceivableId = receivableId,
                Amount = amount,
                Description = description,
            };
            return this.CreateObject(rvd, _receiptVoucherService, _cashBankService, _receivableService, _contactService);
        }

        public ReceiptVoucherDetail CreateObject(int receiptVoucherId, int receivableId, decimal amount, string description, bool isInstantClearance,
                                                 IReceiptVoucherService _receiptVoucherService, ICashBankService _cashBankService,
                                                 IReceivableService _receivableService, IContactService _contactService)
        {
            ReceiptVoucherDetail rvd = new ReceiptVoucherDetail
            {
                ReceiptVoucherId = receiptVoucherId,
                ReceivableId = receivableId,
                Amount = amount,
                Description = description,
                IsInstantClearance = isInstantClearance
            };
            return this.CreateObject(rvd, _receiptVoucherService, _cashBankService, _receivableService, _contactService);
        }

        public ReceiptVoucherDetail UpdateObject(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService, ICashBankService _cashBankService, IReceivableService _receivableService, IContactService _contactService)
        {
            return (_validator.ValidUpdateObject(receiptVoucherDetail, _receiptVoucherService, this, _cashBankService, _receivableService, _contactService) ?
                     _repository.UpdateObject(receiptVoucherDetail) : receiptVoucherDetail);
        }

        public ReceiptVoucherDetail SoftDeleteObject(ReceiptVoucherDetail receiptVoucherDetail)
        {
            return (_validator.ValidDeleteObject(receiptVoucherDetail) ? _repository.SoftDeleteObject(receiptVoucherDetail) : receiptVoucherDetail);
        }

        public bool DeleteObject(int Id)
        {
            return _repository.DeleteObject(Id);
        }

        public ReceiptVoucherDetail ConfirmObject(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService,
                                                  ICashBankService _cashBankService, IReceivableService _receivableService, IContactService _contactService)
        {
            ReceiptVoucher rv = _receiptVoucherService.GetObjectById(receiptVoucherDetail.ReceiptVoucherId);
            CashBank cb = _cashBankService.GetObjectById(rv.CashBankId);
            if (!cb.IsBank)
            {
                return ClearConfirmObject(receiptVoucherDetail, _receiptVoucherService, _cashBankService, _receivableService, _contactService);
            }

            if (_validator.ValidConfirmObject(receiptVoucherDetail, _receiptVoucherService, this, _cashBankService, _receivableService))
            {
                Receivable receivable = _receivableService.GetObjectById(receiptVoucherDetail.ReceivableId);
                receivable.PendingClearanceAmount += receiptVoucherDetail.Amount;
                rv.PendingClearanceAmount += receiptVoucherDetail.Amount;
                _receiptVoucherService.UpdateObject(rv, this, _receivableService, _contactService, _cashBankService);                
                receivable.RemainingAmount -= receiptVoucherDetail.Amount;
                _receivableService.UpdateObject(receivable);
                receiptVoucherDetail = _repository.ConfirmObject(receiptVoucherDetail);
            }
            return receiptVoucherDetail;
        }

        public ReceiptVoucherDetail UnconfirmObject(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService,
                                                    ICashBankService _cashBankService, IReceivableService _receivableService, IContactService _contactService)
        {
            ReceiptVoucher rv = _receiptVoucherService.GetObjectById(receiptVoucherDetail.ReceiptVoucherId);
            CashBank cb = _cashBankService.GetObjectById(rv.CashBankId);
            if (!cb.IsBank)
            {
                return UnclearUnconfirmObject(receiptVoucherDetail, _receiptVoucherService, _cashBankService, _receivableService, _contactService);
            }
            
            if (_validator.ValidUnconfirmObject(receiptVoucherDetail, _receiptVoucherService, this, _cashBankService, _receivableService))
            {
                Receivable receivable = _receivableService.GetObjectById(receiptVoucherDetail.ReceivableId);
                receivable.PendingClearanceAmount -= receiptVoucherDetail.Amount;
                rv.PendingClearanceAmount -= receiptVoucherDetail.Amount;
                _receiptVoucherService.UpdateObject(rv, this, _receivableService, _contactService, _cashBankService);
                receivable.RemainingAmount += receiptVoucherDetail.Amount;
                _receivableService.UpdateObject(receivable);
                receiptVoucherDetail = _repository.UnconfirmObject(receiptVoucherDetail);
            }
            return receiptVoucherDetail;
        }

        public ReceiptVoucherDetail ClearObject(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService,
                                                ICashBankService _cashBankService, IReceivableService _receivableService, IContactService _contactService)
        {
            if (_validator.ValidClearObject(receiptVoucherDetail, _receiptVoucherService, this, _cashBankService, _receivableService))
            {
                ReceiptVoucher rv = _receiptVoucherService.GetObjectById(receiptVoucherDetail.ReceiptVoucherId);
                Receivable receivable = _receivableService.GetObjectById(receiptVoucherDetail.ReceivableId);
                CashBank cb = _cashBankService.GetObjectById(rv.CashBankId);

                cb.Amount += receiptVoucherDetail.Amount;
                receivable.PendingClearanceAmount -= receiptVoucherDetail.Amount;
                rv.PendingClearanceAmount -= receiptVoucherDetail.Amount;

                _cashBankService.UpdateObject(cb);
                _receiptVoucherService.UpdateObject(rv, this, _receivableService, _contactService, _cashBankService);
                if (receivable.PendingClearanceAmount == 0 && receivable.RemainingAmount == 0)
                {
                    receivable.IsCompleted = true;
                    receivable.CompletionDate = receiptVoucherDetail.ClearanceDate;
                }
                _receivableService.UpdateObject(receivable);
                receiptVoucherDetail = _repository.ClearObject(receiptVoucherDetail);
            }
            return receiptVoucherDetail;
        }

        public ReceiptVoucherDetail UnclearObject(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService,
                                                    ICashBankService _cashBankService, IReceivableService _receivableService, IContactService _contactService)
        {
            if (_validator.ValidUnclearObject(receiptVoucherDetail, _receiptVoucherService, this, _cashBankService, _receivableService))
            {
                ReceiptVoucher rv = _receiptVoucherService.GetObjectById(receiptVoucherDetail.ReceiptVoucherId);
                Receivable receivable = _receivableService.GetObjectById(receiptVoucherDetail.ReceivableId);
                CashBank cb = _cashBankService.GetObjectById(rv.CashBankId);

                cb.Amount -= receiptVoucherDetail.Amount;
                receivable.PendingClearanceAmount += receiptVoucherDetail.Amount;
                rv.PendingClearanceAmount += receiptVoucherDetail.Amount;
                receivable.CompletionDate = null;

                _cashBankService.UpdateObject(cb);
                _receiptVoucherService.UpdateObject(rv, this, _receivableService, _contactService, _cashBankService);
                _receivableService.UpdateObject(receivable);
 
                receiptVoucherDetail.ClearanceDate = null;
                receiptVoucherDetail = _repository.UnclearObject(receiptVoucherDetail);
            }
            return receiptVoucherDetail;
        }

        public ReceiptVoucherDetail ClearConfirmObject(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService,
                                          ICashBankService _cashBankService, IReceivableService _receivableService, IContactService _contactService)
        {
            if (_validator.ValidClearConfirmObject(receiptVoucherDetail, _receiptVoucherService, this, _cashBankService, _receivableService))
            {
                ReceiptVoucher rv = _receiptVoucherService.GetObjectById(receiptVoucherDetail.ReceiptVoucherId);
                Receivable receivable = _receivableService.GetObjectById(receiptVoucherDetail.ReceivableId);
                CashBank cb = _cashBankService.GetObjectById(rv.CashBankId);
                cb.Amount += receiptVoucherDetail.Amount;
                _cashBankService.UpdateObject(cb);
                receiptVoucherDetail.ClearanceDate = rv.ReceiptDate;
                _repository.ClearObject(receiptVoucherDetail);
                receivable.RemainingAmount -= receiptVoucherDetail.Amount;
                if (receivable.PendingClearanceAmount == 0 && receivable.RemainingAmount == 0)
                {
                    receivable.IsCompleted = true;
                    receivable.CompletionDate = receiptVoucherDetail.ClearanceDate;
                }
                _receivableService.UpdateObject(receivable);
                receiptVoucherDetail = _repository.ConfirmObject(receiptVoucherDetail);
            }
            return receiptVoucherDetail;
        }

        public ReceiptVoucherDetail UnclearUnconfirmObject(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService,
                                                    ICashBankService _cashBankService, IReceivableService _receivableService, IContactService _contactService)
        {
            if (_validator.ValidUnclearUnconfirmObject(receiptVoucherDetail, _receiptVoucherService, this, _cashBankService, _receivableService))
            {
                ReceiptVoucher rv = _receiptVoucherService.GetObjectById(receiptVoucherDetail.ReceiptVoucherId);
                Receivable receivable = _receivableService.GetObjectById(receiptVoucherDetail.ReceivableId);
                CashBank cb = _cashBankService.GetObjectById(rv.CashBankId);
                cb.Amount -= receiptVoucherDetail.Amount;
                _cashBankService.UpdateObject(cb);
                receivable.IsCompleted = false;
                receivable.CompletionDate = null;
                receiptVoucherDetail.ClearanceDate = null;
                _repository.UnclearObject(receiptVoucherDetail);
                receivable.RemainingAmount += receiptVoucherDetail.Amount;
                _receivableService.UpdateObject(receivable);
                receiptVoucherDetail = _repository.UnconfirmObject(receiptVoucherDetail);
            }
            return receiptVoucherDetail;
        }
    }
}