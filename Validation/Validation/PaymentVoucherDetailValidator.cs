using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Core.Interface.Validation;
using Core.DomainModel;
using Core.Interface.Service;
using Core.Constant;

namespace Validation.Validation
{
    public class PaymentVoucherDetailValidator : IPaymentVoucherDetailValidator
    {
        public PaymentVoucherDetail VHasPaymentVoucher(PaymentVoucherDetail paymentVoucherDetail, IPaymentVoucherService _paymentVoucherService)
        {
            PaymentVoucher paymentVoucher = _paymentVoucherService.GetObjectById(paymentVoucherDetail.PaymentVoucherId);
            if (paymentVoucher == null)
            {
                paymentVoucherDetail.Errors.Add("PaymentVoucher", "Tidak boleh tidak ada");
            }
            return paymentVoucherDetail;
        }

        public PaymentVoucherDetail VHasPayable(PaymentVoucherDetail paymentVoucherDetail, IPayableService _payableService)
        {
            Payable payable = _payableService.GetObjectById(paymentVoucherDetail.PayableId);
            if (payable == null)
            {
                paymentVoucherDetail.Errors.Add("Payable", "Tidak boleh tidak ada");
            }
            return paymentVoucherDetail;
        }

        public PaymentVoucherDetail VHasContact(PaymentVoucherDetail paymentVoucherDetail, IContactService _contactService)
        {
            Contact contact = _contactService.GetObjectById(paymentVoucherDetail.ContactId);
            if (contact == null)
            {
                paymentVoucherDetail.Errors.Add("Contact", "Tidak boleh tidak ada");
            }
            return paymentVoucherDetail;
        }

        public PaymentVoucherDetail VPayableContactIsTheSame(PaymentVoucherDetail paymentVoucherDetail, IPayableService _payableService)
        {
            Payable payable = _payableService.GetObjectById(paymentVoucherDetail.PayableId);
            if (payable.ContactId != paymentVoucherDetail.ContactId)
            {
                paymentVoucherDetail.Errors.Add("Contact", "Tidak boleh tidak sama dengan Payable");
            }
            return paymentVoucherDetail;
        }

        public PaymentVoucherDetail VAmountLessThanCashBank(PaymentVoucherDetail paymentVoucherDetail, IPaymentVoucherService _paymentVoucherService, ICashBankService _cashBankService)
        {
            PaymentVoucher paymentVoucher = _paymentVoucherService.GetObjectById(paymentVoucherDetail.PaymentVoucherId);
            CashBank cashBank = _cashBankService.GetObjectById(paymentVoucher.CashBankId);
            if (paymentVoucherDetail.Amount > cashBank.Amount)
            {
                paymentVoucherDetail.Errors.Add("Amount", "Tidak boleh lebih dari cash bank");
            }
            return paymentVoucherDetail;
        }

        public PaymentVoucherDetail VCorrectInstantClearance(PaymentVoucherDetail paymentVoucherDetail, IPaymentVoucherService _paymentVoucherService, ICashBankService _cashBankService)
        {
            PaymentVoucher paymentVoucher = _paymentVoucherService.GetObjectById(paymentVoucherDetail.PaymentVoucherId);
            CashBank cashBank = _cashBankService.GetObjectById(paymentVoucher.CashBankId);
            if (!cashBank.IsBank && !paymentVoucherDetail.IsInstantClearance)
            {
                paymentVoucherDetail.Errors.Add("IsInstantClearance", "Harus true untuk pembayaran cash");
            }
            return paymentVoucherDetail;

        }

        public PaymentVoucherDetail VUniqueCashBankId(PaymentVoucherDetail paymentVoucherDetail, IPaymentVoucherService _paymentVoucherService, ICashBankService _cashBankService)
        {
            // CashBank harus selalu unique untuk setiap payment voucher detail
            // Tidak pernah tidak unique karena cashBank terasosiasi dengan paymentVoucher
            return paymentVoucherDetail;
        }

        public PaymentVoucherDetail VNonNegativeOrZeroAmount(PaymentVoucherDetail paymentVoucherDetail)
        {
            if (paymentVoucherDetail.Amount <= 0)
            {
                paymentVoucherDetail.Errors.Add("Amount", "Tidak boleh kurag dari atau sama dengan 0");
            }
            return paymentVoucherDetail;
        }

        public PaymentVoucherDetail VIsConfirmed(PaymentVoucherDetail paymentVoucherDetail)
        {
            if (paymentVoucherDetail.IsConfirmed)
            {
                paymentVoucherDetail.Errors.Add("IsConfirmed", "Tidak boleh sudah dikonfirmasi");
            }
            return paymentVoucherDetail;
        }

        public PaymentVoucherDetail VUpdateContactOrPayable(PaymentVoucherDetail paymentVoucherDetail, IPaymentVoucherDetailService _paymentVoucherDetailService)
        {
            PaymentVoucherDetail databasePaymentVoucherDetail = _paymentVoucherDetailService.GetObjectById(paymentVoucherDetail.Id);
            if (paymentVoucherDetail.ContactId != databasePaymentVoucherDetail.ContactId)
            {
                paymentVoucherDetail.Errors.Add("ContactId", "Tidak boleh diubah");
            }
            if (paymentVoucherDetail.PayableId != databasePaymentVoucherDetail.PayableId)
            {
                paymentVoucherDetail.Errors.Add("PayableId", "Tidak boleh diubah");
            }
            return paymentVoucherDetail;
        }

        public PaymentVoucherDetail VRemainingAmount(PaymentVoucherDetail paymentVoucherDetail, IPayableService _payableService)
        {
            return paymentVoucherDetail;
        }

        public PaymentVoucherDetail VIsClearedForNonInstantClearance(PaymentVoucherDetail paymentVoucherDetail)
        {
            return paymentVoucherDetail;
        }

        public PaymentVoucherDetail VIsInstantClearance(PaymentVoucherDetail paymentVoucherDetail)
        {
            return paymentVoucherDetail;
        }

        public PaymentVoucherDetail VClearanceDate(PaymentVoucherDetail paymentVoucherDetail, IPaymentVoucherService _paymentVoucherService)
        {
            return paymentVoucherDetail;
        }

        public PaymentVoucherDetail VCreateObject(PaymentVoucherDetail paymentVoucherDetail, IPaymentVoucherService _paymentVoucherService, IPaymentVoucherDetailService _paymentVoucherDetailService, ICashBankService _cashBankService, IPayableService _payableService, IContactService _contactService)
        {
            VHasPaymentVoucher(paymentVoucherDetail, _paymentVoucherService);
            VHasPayable(paymentVoucherDetail, _payableService);
            VHasContact(paymentVoucherDetail, _contactService);
            VPayableContactIsTheSame(paymentVoucherDetail, _payableService);
            VAmountLessThanCashBank(paymentVoucherDetail, _paymentVoucherService, _cashBankService);
            VCorrectInstantClearance(paymentVoucherDetail, _paymentVoucherService, _cashBankService);
            VNonNegativeOrZeroAmount(paymentVoucherDetail);
            return paymentVoucherDetail;
        }

        public PaymentVoucherDetail VUpdateObject(PaymentVoucherDetail paymentVoucherDetail, IPaymentVoucherService _paymentVoucherService, IPaymentVoucherDetailService _paymentVoucherDetailService, ICashBankService _cashBankService, IPayableService _payableService, IContactService _contactService)
        {
            VIsConfirmed(paymentVoucherDetail);
            VHasPaymentVoucher(paymentVoucherDetail, _paymentVoucherService);
            VHasPayable(paymentVoucherDetail, _payableService);
            VHasContact(paymentVoucherDetail, _contactService);
            VPayableContactIsTheSame(paymentVoucherDetail, _payableService);
            VAmountLessThanCashBank(paymentVoucherDetail, _paymentVoucherService, _cashBankService);
            VCorrectInstantClearance(paymentVoucherDetail, _paymentVoucherService, _cashBankService);
            return paymentVoucherDetail;    
        }

        public PaymentVoucherDetail VDeleteObject(PaymentVoucherDetail paymentVoucherDetail)
        {
            VIsConfirmed(paymentVoucherDetail);
            return paymentVoucherDetail;
        }

        public PaymentVoucherDetail VConfirmObject(PaymentVoucherDetail paymentVoucherDetail, IPaymentVoucherService _paymentVoucherService, IPaymentVoucherDetailService _paymentVoucherDetailService, ICashBankService _cashBankService, IPayableService _payableService)
        {
            VAmountLessThanCashBank(paymentVoucherDetail, _paymentVoucherService, _cashBankService);
            return paymentVoucherDetail;
        }

        public PaymentVoucherDetail VUnconfirmObject(PaymentVoucherDetail paymentVoucherDetail, IPaymentVoucherService _paymentVoucherService, IPaymentVoucherDetailService _paymentVoucherDetailService, ICashBankService _cashBankService, IPayableService _payableService)
        {
            VIsClearedForNonInstantClearance(paymentVoucherDetail);
            return paymentVoucherDetail;
        }

        public PaymentVoucherDetail VClearObject(PaymentVoucherDetail paymentVoucherDetail, IPaymentVoucherService _paymentVoucherService, IPaymentVoucherDetailService _paymentVoucherDetailService, ICashBankService _cashBankService, IPayableService _payableService)
        {
            VIsInstantClearance(paymentVoucherDetail);
            VClearanceDate(paymentVoucherDetail, _paymentVoucherService);
            VAmountLessThanCashBank(paymentVoucherDetail, _paymentVoucherService, _cashBankService);
            return paymentVoucherDetail;
        }

        public PaymentVoucherDetail VUnclearObject(PaymentVoucherDetail paymentVoucherDetail, IPaymentVoucherService _paymentVoucherService, IPaymentVoucherDetailService _paymentVoucherDetailService, ICashBankService _cashBankService, IPayableService _payableService)
        {
            VIsInstantClearance(paymentVoucherDetail);
            return paymentVoucherDetail;
        }

        public bool ValidCreateObject(PaymentVoucherDetail paymentVoucherDetail, IPaymentVoucherService _paymentVoucherService, IPaymentVoucherDetailService _paymentVoucherDetailService, ICashBankService _cashBankService, IPayableService _payableService, IContactService _contactService)
        {
            VCreateObject(paymentVoucherDetail, _paymentVoucherService, _paymentVoucherDetailService, _cashBankService, _payableService, _contactService);
            return isValid(paymentVoucherDetail);
        }

        public bool ValidUpdateObject(PaymentVoucherDetail paymentVoucherDetail, IPaymentVoucherService _paymentVoucherService, IPaymentVoucherDetailService _paymentVoucherDetailService, ICashBankService _cashBankService, IPayableService _payableService, IContactService _contactService)
        {
            VUpdateObject(paymentVoucherDetail, _paymentVoucherService, _paymentVoucherDetailService, _cashBankService, _payableService, _contactService);
            return isValid(paymentVoucherDetail);
        }

        public bool ValidDeleteObject(PaymentVoucherDetail paymentVoucherDetail)
        {
            VDeleteObject(paymentVoucherDetail);
            return isValid(paymentVoucherDetail);
        }

        public bool ValidConfirmObject(PaymentVoucherDetail paymentVoucherDetail, IPaymentVoucherService _paymentVoucherService, IPaymentVoucherDetailService _paymentVoucherDetailService, ICashBankService _cashBankService, IPayableService _payableService)
        {
            VConfirmObject(paymentVoucherDetail, _paymentVoucherService, _paymentVoucherDetailService, _cashBankService, _payableService);
            return isValid(paymentVoucherDetail);
        }

        public bool ValidUnconfirmObject(PaymentVoucherDetail paymentVoucherDetail, IPaymentVoucherService _paymentVoucherService, IPaymentVoucherDetailService _paymentVoucherDetailService, ICashBankService _cashBankService, IPayableService _payableService)
        {
            VUnconfirmObject(paymentVoucherDetail, _paymentVoucherService, _paymentVoucherDetailService, _cashBankService, _payableService);
            return isValid(paymentVoucherDetail);
        }

        public bool ValidClearObject(PaymentVoucherDetail paymentVoucherDetail, IPaymentVoucherService _paymentVoucherService, IPaymentVoucherDetailService _paymentVoucherDetailService, ICashBankService _cashBankService, IPayableService _payableService)
        {
            VClearObject(paymentVoucherDetail, _paymentVoucherService, _paymentVoucherDetailService, _cashBankService, _payableService);
            return isValid(paymentVoucherDetail);
        }

        public bool ValidUnclearObject(PaymentVoucherDetail paymentVoucherDetail, IPaymentVoucherService _paymentVoucherService, IPaymentVoucherDetailService _paymentVoucherDetailService, ICashBankService _cashBankService, IPayableService _payableService)
        {
            VUnclearObject(paymentVoucherDetail, _paymentVoucherService, _paymentVoucherDetailService, _cashBankService, _payableService);
            return isValid(paymentVoucherDetail);
        }

        public bool isValid(PaymentVoucherDetail obj)
        {
            bool isValid = !obj.Errors.Any();
            return isValid;
        }

        public string PrintError(PaymentVoucherDetail obj)
        {
            string erroroutput = "";
            KeyValuePair<string, string> first = obj.Errors.ElementAt(0);
            erroroutput += first.Key + "," + first.Value;
            foreach (KeyValuePair<string, string> pair in obj.Errors.Skip(1))
            {
                erroroutput += Environment.NewLine;
                erroroutput += pair.Key + "," + pair.Value;
            }
            return erroroutput;
        }
    }
}