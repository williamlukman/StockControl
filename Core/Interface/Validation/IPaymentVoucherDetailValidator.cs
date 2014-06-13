using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DomainModel;
using Core.Interface.Service;

namespace Core.Interface.Validation
{
    public interface IPaymentVoucherDetailValidator
    {
        PaymentVoucherDetail VHasPaymentVoucher(PaymentVoucherDetail paymentVoucherDetail, IPaymentVoucherService _paymentVoucherService);
        PaymentVoucherDetail VHasPayable(PaymentVoucherDetail paymentVoucherDetail, IPayableService _payableService);
        PaymentVoucherDetail VHasContact(PaymentVoucherDetail paymentVoucherDetail, IContactService _contactService);
        PaymentVoucherDetail VPayableContactIsTheSame(PaymentVoucherDetail paymentVoucherDetail, IPayableService _payableService);
        PaymentVoucherDetail VAmountLessThanCashBank(PaymentVoucherDetail paymentVoucherDetail, IPaymentVoucherService _paymentVoucherService, ICashBankService _cashBankService);
        PaymentVoucherDetail VCorrectInstantClearance(PaymentVoucherDetail paymentVoucherDetail, IPaymentVoucherService _paymentVoucherService, ICashBankService _cashBankService);
        PaymentVoucherDetail VUniqueCashBankId(PaymentVoucherDetail paymentVoucherDetail, IPaymentVoucherService _paymentVoucherService, ICashBankService _cashBankService);
        PaymentVoucherDetail VNonNegativeOrZeroAmount(PaymentVoucherDetail paymentVoucherDetail);
        PaymentVoucherDetail VIsConfirmed(PaymentVoucherDetail paymentVoucherDetail);
        PaymentVoucherDetail VUpdateContactOrPayable(PaymentVoucherDetail paymentVoucherDetail, IPaymentVoucherDetailService _paymentVoucherDetailService);

        PaymentVoucherDetail VRemainingAmount(PaymentVoucherDetail paymentVoucherDetail, IPayableService _payableService);
        PaymentVoucherDetail VIsClearedForNonInstantClearance(PaymentVoucherDetail paymentVoucherDetail);
        PaymentVoucherDetail VIsInstantClearance(PaymentVoucherDetail paymentVoucherDetail);
        PaymentVoucherDetail VClearanceDate(PaymentVoucherDetail paymentVoucherDetail, IPaymentVoucherService _paymentVoucherService);

        PaymentVoucherDetail VCreateObject(PaymentVoucherDetail paymentVoucherDetail, IPaymentVoucherService _paymentVoucherService, IPaymentVoucherDetailService _paymentVoucherDetailService, ICashBankService _cashBankService, IPayableService _payableService, IContactService _contactService);
        PaymentVoucherDetail VUpdateObject(PaymentVoucherDetail paymentVoucherDetail, IPaymentVoucherService _paymentVoucherService, IPaymentVoucherDetailService _paymentVoucherDetailService, ICashBankService _cashBankService, IPayableService _payableService, IContactService _contactService);
        PaymentVoucherDetail VDeleteObject(PaymentVoucherDetail paymentVoucherDetail);
        PaymentVoucherDetail VConfirmObject(PaymentVoucherDetail paymentVoucherDetail, IPaymentVoucherService _paymentVoucherService, IPaymentVoucherDetailService _paymentVoucherDetailService, ICashBankService _cashBankService, IPayableService _payableService);
        PaymentVoucherDetail VUnconfirmObject(PaymentVoucherDetail paymentVoucherDetail, IPaymentVoucherService _paymentVoucherService, IPaymentVoucherDetailService _paymentVoucherDetailService, ICashBankService _cashBankService, IPayableService _payableService);
        PaymentVoucherDetail VClearObject(PaymentVoucherDetail paymentVoucherDetail, IPaymentVoucherService _paymentVoucherService, IPaymentVoucherDetailService _paymentVoucherDetailService, ICashBankService _cashBankService, IPayableService _payableService);
        PaymentVoucherDetail VUnclearObject(PaymentVoucherDetail paymentVoucherDetail, IPaymentVoucherService _paymentVoucherService, IPaymentVoucherDetailService _paymentVoucherDetailService, ICashBankService _cashBankService, IPayableService _payableService);
        bool ValidCreateObject(PaymentVoucherDetail paymentVoucherDetail, IPaymentVoucherService _paymentVoucherService, IPaymentVoucherDetailService _paymentVoucherDetailService, ICashBankService _cashBankService, IPayableService _payableService, IContactService _contactService);
        bool ValidUpdateObject(PaymentVoucherDetail paymentVoucherDetail, IPaymentVoucherService _paymentVoucherService, IPaymentVoucherDetailService _paymentVoucherDetailService, ICashBankService _cashBankService, IPayableService _payableService, IContactService _contactService);
        bool ValidDeleteObject(PaymentVoucherDetail paymentVoucherDetail);
        bool ValidConfirmObject(PaymentVoucherDetail paymentVoucherDetail, IPaymentVoucherService _paymentVoucherService, IPaymentVoucherDetailService _paymentVoucherDetailService, ICashBankService _cashBankService, IPayableService _payableService);
        bool ValidUnconfirmObject(PaymentVoucherDetail paymentVoucherDetail, IPaymentVoucherService _paymentVoucherService, IPaymentVoucherDetailService _paymentVoucherDetailService, ICashBankService _cashBankService, IPayableService _payableService);
        bool ValidClearObject(PaymentVoucherDetail paymentVoucherDetail, IPaymentVoucherService _paymentVoucherService, IPaymentVoucherDetailService _paymentVoucherDetailService, ICashBankService _cashBankService, IPayableService _payableService);
        bool ValidUnclearObject(PaymentVoucherDetail paymentVoucherDetail, IPaymentVoucherService _paymentVoucherService, IPaymentVoucherDetailService _paymentVoucherDetailService, ICashBankService _cashBankService, IPayableService _payableService);
        bool isValid(PaymentVoucherDetail paymentVoucherDetail);
        string PrintError(PaymentVoucherDetail paymentVoucherDetail);
    }
}
