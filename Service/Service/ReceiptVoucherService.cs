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
    public class ReceiptVoucherService : IReceiptVoucherService
    {
        private IReceiptVoucherRepository _repository;
        private IReceiptVoucherValidator _validator;

        public ReceiptVoucherService(IReceiptVoucherRepository _receiptVoucherRepository, IReceiptVoucherValidator _receiptVoucherValidator)
        {
            _repository = _receiptVoucherRepository;
            _validator = _receiptVoucherValidator;
        }

        public IReceiptVoucherValidator GetValidator()
        {
            return _validator;
        }

        public IList<ReceiptVoucher> GetAll()
        {
            return _repository.GetAll();
        }

        public IList<ReceiptVoucher> GetObjectsByCashBankId(int cashBankId)
        {
            return _repository.GetObjectsByCashBankId(cashBankId);
        }

        public ReceiptVoucher GetObjectById(int Id)
        {
            return _repository.GetObjectById(Id);
        }

        public IList<ReceiptVoucher> GetObjectsByContactId(int contactId)
        {
            return _repository.GetObjectsByContactId(contactId);
        }

        public ReceiptVoucher CreateObject(ReceiptVoucher receiptVoucher, IReceiptVoucherDetailService _receiptVoucherDetailService,
                                            IReceivableService _receivableService, IContactService _contactService, ICashBankService _cashBankService)
        {
            receiptVoucher.Errors = new Dictionary<String, String>();
            if (_validator.ValidCreateObject(receiptVoucher, this, _receiptVoucherDetailService, _receivableService, _contactService, _cashBankService))
            {
                CashBank cashBank = _cashBankService.GetObjectById(receiptVoucher.CashBankId);
                receiptVoucher.IsInstantClearance = (cashBank.IsBank) ? receiptVoucher.IsInstantClearance : true;
                return _repository.CreateObject(receiptVoucher);
            }
            else
            {
                return receiptVoucher;
            }
        }

        public ReceiptVoucher CreateObject(int cashBankId, int contactId, DateTime receiptDate, decimal totalAmount,
                                            IReceiptVoucherDetailService _receiptVoucherDetailService, IReceivableService _receivableService,
                                            IContactService _contactService, ICashBankService _cashBankService)
        {
            ReceiptVoucher rv = new ReceiptVoucher
            {
                CashBankId = cashBankId,
                ContactId = contactId,
                ReceiptDate = receiptDate,
                TotalAmount = totalAmount
            };
            return this.CreateObject(rv, _receiptVoucherDetailService, _receivableService, _contactService, _cashBankService);
        }

        public ReceiptVoucher CreateObject(int cashBankId, int contactId, DateTime receiptDate, decimal totalAmount, bool IsInstantClearance,
                                    IReceiptVoucherDetailService _receiptVoucherDetailService, IReceivableService _receivableService,
                                    IContactService _contactService, ICashBankService _cashBankService)
        {
            ReceiptVoucher rv = new ReceiptVoucher
            {
                CashBankId = cashBankId,
                ContactId = contactId,
                ReceiptDate = receiptDate,
                TotalAmount = totalAmount,
                IsInstantClearance = IsInstantClearance
            };
            return this.CreateObject(rv, _receiptVoucherDetailService, _receivableService, _contactService, _cashBankService);
        }

        public ReceiptVoucher UpdateAmount(ReceiptVoucher receiptVoucher)
        {
            return _repository.UpdateObject(receiptVoucher);
        }

        public ReceiptVoucher UpdateObject(ReceiptVoucher receiptVoucher, IReceiptVoucherDetailService _receiptVoucherDetailService, IReceivableService _receivableService, IContactService _contactService, ICashBankService _cashBankService)
        {
            return (_validator.ValidUpdateObject(receiptVoucher, this, _receiptVoucherDetailService, _receivableService, _contactService, _cashBankService) ? _repository.UpdateObject(receiptVoucher) : receiptVoucher);
        }

        public ReceiptVoucher SoftDeleteObject(ReceiptVoucher receiptVoucher, IReceiptVoucherDetailService _receiptVoucherDetailService)
        {
            return (_validator.ValidDeleteObject(receiptVoucher, _receiptVoucherDetailService) ? _repository.SoftDeleteObject(receiptVoucher) : receiptVoucher);
        }

        public bool DeleteObject(int Id)
        {
            return _repository.DeleteObject(Id);
        }


        public ReceiptVoucher ConfirmObject(ReceiptVoucher receiptVoucher, IReceiptVoucherDetailService _receiptVoucherDetailService,
                                            ICashBankService _cashBankService, IReceivableService _receivableService, IContactService _contactService)
        {

            if (_validator.ValidConfirmObject(receiptVoucher, this, _receiptVoucherDetailService, _cashBankService, _receivableService, _contactService))
            {
                IList<ReceiptVoucherDetail> details = _receiptVoucherDetailService.GetObjectsByReceiptVoucherId(receiptVoucher.Id);
                if (receiptVoucher.IsInstantClearance)
                {
                    receiptVoucher.ClearanceDate = receiptVoucher.ReceiptDate;
                    _repository.ConfirmObject(receiptVoucher);
                    _repository.ClearObject(receiptVoucher);
                    foreach (var detail in details)
                    {
                        detail.ConfirmedAt = receiptVoucher.ConfirmedAt;
                        detail.ClearanceDate = receiptVoucher.ReceiptDate;
                        _receiptVoucherDetailService.ConfirmObject(detail, this, _cashBankService, _receivableService, _contactService);
                    }
                }
                else
                {
                    _repository.ConfirmObject(receiptVoucher);
                    foreach (var detail in details)
                    {
                        detail.ConfirmedAt = receiptVoucher.ConfirmedAt;
                        _receiptVoucherDetailService.ConfirmObject(detail, this, _cashBankService, _receivableService, _contactService);
                    }
                }
            }
            return receiptVoucher;
        }

        public ReceiptVoucher UnconfirmObject(ReceiptVoucher receiptVoucher, IReceiptVoucherDetailService _receiptVoucherDetailService,
                                            ICashBankService _cashBankService, IReceivableService _receivableService, IContactService _contactService)
        {
            if (_validator.ValidUnconfirmObject(receiptVoucher, this, _receiptVoucherDetailService, _cashBankService, _receivableService, _contactService))
            {
                IList<ReceiptVoucherDetail> details = _receiptVoucherDetailService.GetObjectsByReceiptVoucherId(receiptVoucher.Id);
                if (receiptVoucher.IsInstantClearance)
                {
                    receiptVoucher.ClearanceDate = null;
                    _repository.UnconfirmObject(receiptVoucher);
                    _repository.UnclearObject(receiptVoucher);
                    foreach (var detail in details)
                    {
                        detail.ConfirmedAt = null;
                        detail.ClearanceDate = null;
                        _receiptVoucherDetailService.UnconfirmObject(detail, this, _cashBankService, _receivableService, _contactService);
                    }
                }
                else
                {
                    _repository.UnconfirmObject(receiptVoucher);
                    foreach (var detail in details)
                    {
                        _receiptVoucherDetailService.UnconfirmObject(detail, this, _cashBankService, _receivableService, _contactService);
                    }
                }
            }
            return receiptVoucher;
        }

        public ReceiptVoucher ClearObject(ReceiptVoucher receiptVoucher, IReceiptVoucherDetailService _receiptVoucherDetailService,
                             ICashBankService _cashBankService, IReceivableService _receivableService, IContactService _contactService)
        {
            if (_validator.ValidClearObject(receiptVoucher, this, _receiptVoucherDetailService, _cashBankService, _receivableService, _contactService))
            {
                _repository.ClearObject(receiptVoucher);
                IList<ReceiptVoucherDetail> details = _receiptVoucherDetailService.GetObjectsByReceiptVoucherId(receiptVoucher.Id);
                foreach (var detail in details)
                {
                    _receiptVoucherDetailService.ClearObject(detail, this, _cashBankService, _receivableService, _contactService);
                }
            }
            return receiptVoucher;
        }

        public ReceiptVoucher UnclearObject(ReceiptVoucher receiptVoucher, IReceiptVoucherDetailService _receiptVoucherDetailService,
                                     ICashBankService _cashBankService, IReceivableService _receivableService, IContactService _contactService)
        {
            if (_validator.ValidUnclearObject(receiptVoucher, this, _receiptVoucherDetailService, _cashBankService, _receivableService, _contactService))
            {
                _repository.UnclearObject(receiptVoucher);
                IList<ReceiptVoucherDetail> details = _receiptVoucherDetailService.GetObjectsByReceiptVoucherId(receiptVoucher.Id);
                foreach (var detail in details)
                {
                    _receiptVoucherDetailService.UnclearObject(detail, this, _cashBankService, _receivableService, _contactService);
                }
            }
            return receiptVoucher;
        }
    }
}