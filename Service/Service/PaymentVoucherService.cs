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
                                            IPayableService _payableService, IContactService _contactService)
        {
            paymentVoucher.Errors = new Dictionary<String, String>();
            return (_validator.ValidCreateObject(paymentVoucher, this, _paymentVoucherDetailService, _payableService, _contactService) ? _repository.CreateObject(paymentVoucher) : paymentVoucher);
        }

        public PaymentVoucher CreateObject(int cashBankId, int contactId, DateTime paymentDate, decimal totalAmount,
                                            IPaymentVoucherDetailService _paymentVoucherDetailService, IPayableService _payableService,
                                            IContactService _contactService)
        {
            PaymentVoucher pv = new PaymentVoucher
            {
                CashBankId = cashBankId,
                ContactId = contactId,
                PaymentDate = paymentDate,
                TotalAmount = totalAmount,
                PendingClearanceAmount = 0
            };
            return this.CreateObject(pv, _paymentVoucherDetailService, _payableService, _contactService);
        }

        public PaymentVoucher UpdateObject(PaymentVoucher paymentVoucher, IPaymentVoucherDetailService _paymentVoucherDetailService, IPayableService _payableService, IContactService _contactService)
        {
            paymentVoucher.Errors.Clear();
            return (_validator.ValidUpdateObject(paymentVoucher, this, _paymentVoucherDetailService, _payableService, _contactService) ? _repository.UpdateObject(paymentVoucher) : paymentVoucher);
        }

        public PaymentVoucher SoftDeleteObject(PaymentVoucher paymentVoucher, IPaymentVoucherDetailService _paymentVoucherDetailService)
        {
            paymentVoucher.Errors.Clear();
            return (_validator.ValidDeleteObject(paymentVoucher, _paymentVoucherDetailService) ? _repository.SoftDeleteObject(paymentVoucher) : paymentVoucher);
        }

        public bool DeleteObject(int Id)
        {
            return _repository.DeleteObject(Id);
        }

        public PaymentVoucher ConfirmObject(PaymentVoucher paymentVoucher, IPaymentVoucherDetailService _paymentVoucherDetailService,
                                            ICashBankService _cashBankService, IPayableService _payableService, IContactService _contactService)
        {
            paymentVoucher.Errors.Clear();
            if (_validator.ValidConfirmObject(paymentVoucher, this, _paymentVoucherDetailService, _cashBankService, _payableService, _contactService))
            {
                _repository.ConfirmObject(paymentVoucher);
                IList<PaymentVoucherDetail> details = _paymentVoucherDetailService.GetObjectsByPaymentVoucherId(paymentVoucher.Id);
                foreach (var detail in details)
                {
                    detail.ConfirmedAt = paymentVoucher.ConfirmedAt;
                    _paymentVoucherDetailService.ConfirmObject(detail, this, _cashBankService, _payableService, _contactService);
                }
            }
            return paymentVoucher;
        }

        public PaymentVoucher UnconfirmObject(PaymentVoucher paymentVoucher, IPaymentVoucherDetailService _paymentVoucherDetailService,
                                            ICashBankService _cashBankService, IPayableService _payableService, IContactService _contactService)
        {
            paymentVoucher.Errors.Clear();
            if (_validator.ValidUnconfirmObject(paymentVoucher, this, _paymentVoucherDetailService, _cashBankService, _payableService, _contactService))
            {
                _repository.UnconfirmObject(paymentVoucher);
                IList<PaymentVoucherDetail> details = _paymentVoucherDetailService.GetObjectsByPaymentVoucherId(paymentVoucher.Id);
                foreach (var detail in details)
                {
                    _paymentVoucherDetailService.UnconfirmObject(detail, this, _cashBankService, _payableService, _contactService);
                }
            }
            return paymentVoucher;
        }
    }
}