using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DomainModel;
using Core.Interface.Service;

namespace Core.Interface.Validation
{
    public interface IPaymentVoucherValidator
    {
        PaymentVoucher VContact(PaymentVoucher pv, IContactService _cs);
        PaymentVoucher VPurchaseDate(PaymentVoucher pv);
        PaymentVoucher VIsConfirmed(PaymentVoucher pv);
        PaymentVoucher VHasPaymentVoucherDetails(PaymentVoucher pv, IPaymentVoucherDetailService _pods);
        PaymentVoucher VCreateObject(PaymentVoucher paymentVoucher, IPaymentVoucherService _paymentVoucherService, IPaymentVoucherDetailService _paymentVoucherDetailService, IPayableService _payableService, IContactService _contactService);
        PaymentVoucher VUpdateObject(PaymentVoucher paymentVoucher, IPaymentVoucherService _paymentVoucherService, IPaymentVoucherDetailService _paymentVoucherDetailService, IPayableService _payableService, IContactService _contactService);
        PaymentVoucher VDeleteObject(PaymentVoucher pv, IPaymentVoucherDetailService _pods);
        PaymentVoucher VConfirmObject(PaymentVoucher pv, IPaymentVoucherService _paymentVoucherService, IPaymentVoucherDetailService _paymentVoucherDetailService, ICashBankService _cashBankService, IPayableService _payableService, IContactService _contactService);
        PaymentVoucher VUnconfirmObject(PaymentVoucher pv, IPaymentVoucherService _paymentVoucherService, IPaymentVoucherDetailService _paymentVoucherDetailService, ICashBankService _cashBankService, IPayableService _payableService, IContactService _contactService);
        bool ValidCreateObject(PaymentVoucher paymentVoucher, IPaymentVoucherService _paymentVoucherService, IPaymentVoucherDetailService _paymentVoucherDetailService, IPayableService _payableService, IContactService _contactService);
        bool ValidUpdateObject(PaymentVoucher paymentVoucher, IPaymentVoucherService _paymentVoucherService, IPaymentVoucherDetailService _paymentVoucherDetailService, IPayableService _payableService, IContactService _contactService);
        bool ValidDeleteObject(PaymentVoucher pv, IPaymentVoucherDetailService _pvds);
        bool ValidConfirmObject(PaymentVoucher pv, IPaymentVoucherService _paymentVoucherService, IPaymentVoucherDetailService _paymentVoucherDetailService, ICashBankService _cashBankService, IPayableService _payableService, IContactService _contactService);
        bool ValidUnconfirmObject(PaymentVoucher pv, IPaymentVoucherService _paymentVoucherService, IPaymentVoucherDetailService _paymentVoucherDetailService, ICashBankService _cashBankService, IPayableService _payableService, IContactService _contactService);
        bool isValid(PaymentVoucher pv);
        string PrintError(PaymentVoucher pv);
    }
}
