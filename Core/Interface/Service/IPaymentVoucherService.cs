using Core.DomainModel;
using Core.Interface.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Service
{
    public interface IPaymentVoucherService
    {
        IPaymentVoucherValidator GetValidator();
        IList<PaymentVoucher> GetAll();
        IList<PaymentVoucher> GetObjectsByCashBankId(int cashBankId);
        PaymentVoucher GetObjectById(int Id);
        IList<PaymentVoucher> GetObjectsByContactId(int contactId);
        PaymentVoucher CreateObject(PaymentVoucher paymentVoucher, IPaymentVoucherDetailService _paymentVoucherDetailService, IPayableService _payableService, IContactService _contactService, ICashBankService _cashBankService);
        PaymentVoucher UpdateObject(PaymentVoucher paymentVoucher, IPaymentVoucherDetailService _paymentVoucherDetailService, IPayableService _payableService, IContactService _contactService, ICashBankService _cashBankService);
        PaymentVoucher SoftDeleteObject(PaymentVoucher paymentVoucher, IPaymentVoucherDetailService _paymentVoucherDetailService);
        bool DeleteObject(int Id);
        PaymentVoucher ConfirmObject(PaymentVoucher paymentVoucher, IPaymentVoucherDetailService _paymentVoucherDetailService,
                                     ICashBankService _cashBankService, IPayableService _payableService, IContactService _contactService);
        PaymentVoucher UnconfirmObject(PaymentVoucher paymentVoucher, IPaymentVoucherDetailService _paymentVoucherDetailService,
                                     ICashBankService _cashBankService, IPayableService _payableService, IContactService _contactService);
    }
}