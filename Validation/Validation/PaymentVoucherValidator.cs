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
    public class PaymentVoucherValidator : IPaymentVoucherValidator
    {
        public PaymentVoucher VHasContact(PaymentVoucher paymentVoucher, IContactService _contactService)
        {
            Contact contact = _contactService.GetObjectById(paymentVoucher.ContactId);
            if (contact == null)
            {
                paymentVoucher.Errors.Add("ContactId", "Tidak boleh tidak ada");
            }
            return paymentVoucher;
        }

        public PaymentVoucher VHasCashBank(PaymentVoucher paymentVoucher, ICashBankService _cashBankService)
        {
            CashBank cashBank = _cashBankService.GetObjectById(paymentVoucher.CashBankId);
            if (cashBank == null)
            {
                paymentVoucher.Errors.Add("CashBankId", "Tidak boleh tidak ada");
            }
            return paymentVoucher;
        }

        public PaymentVoucher VHasPaymentDate(PaymentVoucher paymentVoucher)
        {
            // PaymentDate tidak boleh tidak ada.
            // PaymentDate will never be null
            return paymentVoucher;
        }

        public PaymentVoucher VHasPaymentVoucherDetails(PaymentVoucher paymentVoucher, IPaymentVoucherDetailService _paymentVoucherDetailService)
        {
            IList<PaymentVoucherDetail> details = _paymentVoucherDetailService.GetObjectsByPaymentVoucherId(paymentVoucher.Id);
            if (!details.Any())
            {
                paymentVoucher.Errors.Add("PaymentVoucherDetails", "Tidak boleh tidak ada");
            }
            return paymentVoucher;
        }

        public PaymentVoucher VRemainingAmountDetails(PaymentVoucher paymentVoucher, IPaymentVoucherDetailService _paymentVoucherDetailService, IPayableService _payableService)
        {
            IDictionary<int, decimal> ValuePairPayableIdAmount = new Dictionary<int, decimal>();
            IList<PaymentVoucherDetail> details = _paymentVoucherDetailService.GetObjectsByPaymentVoucherId(paymentVoucher.Id);

            // group all details by PayableId, and store the total amount
            foreach (var detail in details)
            {
                if (ValuePairPayableIdAmount.ContainsKey(detail.PayableId))
                {
                    ValuePairPayableIdAmount[detail.PayableId] += detail.Amount;
                }
                else
                {
                    ValuePairPayableIdAmount.Add(detail.PayableId, detail.Amount);
                }
            }

            // check if the total amount is less than Payable.RemainingAmount
            foreach (var valuePair in ValuePairPayableIdAmount)
            {
                Payable payable = _payableService.GetObjectById(valuePair.Key);
                if (payable.RemainingAmount < valuePair.Value)
                {
                    paymentVoucher.Errors.Add("RemainingAmount", "Tidak boleh kurang dari amount dari Payment Voucher Details");
                    return paymentVoucher;
                }
            }
            return paymentVoucher;
        }

        public PaymentVoucher VUnconfirmableDetails(PaymentVoucher paymentVoucher, IPaymentVoucherService _paymentVoucherService, IPaymentVoucherDetailService _paymentVoucherDetailService, ICashBankService _cashBankService, IPayableService _payableService)
        {
            IList<PaymentVoucherDetail> details = _paymentVoucherDetailService.GetObjectsByPaymentVoucherId(paymentVoucher.Id);
            foreach (var detail in details)
            {
                if (!_paymentVoucherDetailService.GetValidator().ValidUnconfirmObject(detail, _paymentVoucherService, _paymentVoucherDetailService, _cashBankService, _payableService))
                {
                    foreach (var error in detail.Errors)
                    {
                        paymentVoucher.Errors.Add(error.Key, error.Value);
                    }
                    if (paymentVoucher.Errors.Any()) { return paymentVoucher; }
                }
            }
            return paymentVoucher;
        }

        public PaymentVoucher VUpdateContact(PaymentVoucher paymentVoucher, IPaymentVoucherService _paymentVoucherService, IPaymentVoucherDetailService _paymentVoucherDetailService)
        {
            PaymentVoucher databasePaymentVoucher = _paymentVoucherService.GetObjectById(paymentVoucher.Id);
            if (isValid(VHasPaymentVoucherDetails(paymentVoucher, _paymentVoucherDetailService)))
            {
                if (paymentVoucher.ContactId != databasePaymentVoucher.ContactId)
                {
                    paymentVoucher.Errors.Add("ContactId", "Tidak boleh diubah");
                }
            }
            return paymentVoucher;
        }

        public PaymentVoucher VIsConfirmed(PaymentVoucher paymentVoucher)
        {
            if (paymentVoucher.IsConfirmed)
            {
                paymentVoucher.Errors.Add("IsConfirmed", "Tidak boleh sudah dikonfirmasi");
            }
            return paymentVoucher;
        }

        public PaymentVoucher VCreateObject(PaymentVoucher paymentVoucher, IPaymentVoucherService _paymentVoucherService, IPaymentVoucherDetailService _paymentVoucherDetailService, IPayableService _payableService, IContactService _contactService, ICashBankService _cashBankService)
        {
            VHasContact(paymentVoucher, _contactService);
            VHasCashBank(paymentVoucher, _cashBankService);
            VHasPaymentDate(paymentVoucher);
            return paymentVoucher;
        }

        public PaymentVoucher VUpdateObject(PaymentVoucher paymentVoucher, IPaymentVoucherService _paymentVoucherService, IPaymentVoucherDetailService _paymentVoucherDetailService, IPayableService _payableService, IContactService _contactService, ICashBankService _cashBankService)
        {
            VIsConfirmed(paymentVoucher);
            VHasContact(paymentVoucher, _contactService);
            VHasCashBank(paymentVoucher, _cashBankService);
            VHasPaymentDate(paymentVoucher);
            VUpdateContact(paymentVoucher, _paymentVoucherService, _paymentVoucherDetailService);
            return paymentVoucher;
        }

        public PaymentVoucher VDeleteObject(PaymentVoucher paymentVoucher, IPaymentVoucherDetailService _paymentVoucherDetailService)
        {
            VIsConfirmed(paymentVoucher);
            return paymentVoucher;
        }

        public PaymentVoucher VConfirmObject(PaymentVoucher paymentVoucher, IPaymentVoucherService _paymentVoucherService, IPaymentVoucherDetailService _paymentVoucherDetailService, ICashBankService _cashBankService, IPayableService _payableService, IContactService _contactService)
        {
            VHasPaymentVoucherDetails(paymentVoucher, _paymentVoucherDetailService);
            VRemainingAmountDetails(paymentVoucher, _paymentVoucherDetailService, _payableService);
            return paymentVoucher;
        }

        public PaymentVoucher VUnconfirmObject(PaymentVoucher paymentVoucher, IPaymentVoucherService _paymentVoucherService, IPaymentVoucherDetailService _paymentVoucherDetailService, ICashBankService _cashBankService, IPayableService _payableService, IContactService _contactService)
        {
            VUnconfirmableDetails(paymentVoucher, _paymentVoucherService, _paymentVoucherDetailService, _cashBankService, _payableService);
            return paymentVoucher;
        }

        public bool ValidCreateObject(PaymentVoucher paymentVoucher, IPaymentVoucherService _paymentVoucherService, IPaymentVoucherDetailService _paymentVoucherDetailService, IPayableService _payableService, IContactService _contactService, ICashBankService _cashBankService)
        {
            VCreateObject(paymentVoucher, _paymentVoucherService, _paymentVoucherDetailService, _payableService, _contactService, _cashBankService);
            return isValid(paymentVoucher);
        }

        public bool ValidUpdateObject(PaymentVoucher paymentVoucher, IPaymentVoucherService _paymentVoucherService, IPaymentVoucherDetailService _paymentVoucherDetailService, IPayableService _payableService, IContactService _contactService, ICashBankService _cashBankService)
        {
            paymentVoucher.Errors.Clear();
            VUpdateObject(paymentVoucher, _paymentVoucherService, _paymentVoucherDetailService, _payableService, _contactService, _cashBankService);
            return isValid(paymentVoucher);
        }

        public bool ValidDeleteObject(PaymentVoucher paymentVoucher, IPaymentVoucherDetailService _paymentVoucherDetailService)
        {
            paymentVoucher.Errors.Clear();
            VDeleteObject(paymentVoucher, _paymentVoucherDetailService);
            return isValid(paymentVoucher);
        }

        public bool ValidConfirmObject(PaymentVoucher paymentVoucher, IPaymentVoucherService _paymentVoucherService, IPaymentVoucherDetailService _paymentVoucherDetailService, ICashBankService _cashBankService, IPayableService _payableService, IContactService _contactService)
        {
            paymentVoucher.Errors.Clear();
            VConfirmObject(paymentVoucher, _paymentVoucherService, _paymentVoucherDetailService, _cashBankService, _payableService, _contactService);
            return isValid(paymentVoucher);
        }

        public bool ValidUnconfirmObject(PaymentVoucher paymentVoucher, IPaymentVoucherService _paymentVoucherService, IPaymentVoucherDetailService _paymentVoucherDetailService, ICashBankService _cashBankService, IPayableService _payableService, IContactService _contactService)
        {
            paymentVoucher.Errors.Clear();
            VUnconfirmObject(paymentVoucher, _paymentVoucherService, _paymentVoucherDetailService, _cashBankService, _payableService, _contactService);
            return isValid(paymentVoucher);
        }

        public bool isValid(PaymentVoucher obj)
        {
            bool isValid = !obj.Errors.Any();
            return isValid;
        }

        public string PrintError(PaymentVoucher obj)
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