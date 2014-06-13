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
    public class ReceiptVoucherValidator : IReceiptVoucherValidator
    {
        public ReceiptVoucher VHasContact(ReceiptVoucher receiptVoucher, IContactService _contactService)
        {
            Contact contact = _contactService.GetObjectById(receiptVoucher.ContactId);
            if (contact == null)
            {
                receiptVoucher.Errors.Add("ContactId", "Tidak boleh tidak ada");
            }
            return receiptVoucher;
        }

        public ReceiptVoucher VHasCashBank(ReceiptVoucher receiptVoucher, ICashBankService _cashBankService)
        {
            CashBank cashBank = _cashBankService.GetObjectById(receiptVoucher.CashBankId);
            if (cashBank == null)
            {
                receiptVoucher.Errors.Add("CashBankId", "Tidak boleh tidak ada");
            }
            return receiptVoucher;
        }

        public ReceiptVoucher VHasReceiptDate(ReceiptVoucher receiptVoucher)
        {
            // ReceiptDate tidak boleh tidak ada.
            // ReceiptDate will never be null
            return receiptVoucher;
        }

        public ReceiptVoucher VHasReceiptVoucherDetails(ReceiptVoucher receiptVoucher, IReceiptVoucherDetailService _receiptVoucherDetailService)
        {
            IList<ReceiptVoucherDetail> details = _receiptVoucherDetailService.GetObjectsByReceiptVoucherId(receiptVoucher.Id);
            if (!details.Any())
            {
                receiptVoucher.Errors.Add("ReceiptVoucherDetails", "Tidak boleh tidak ada");
            }
            return receiptVoucher;
        }

        public ReceiptVoucher VRemainingAmountDetails(ReceiptVoucher receiptVoucher, IReceiptVoucherDetailService _receiptVoucherDetailService, IReceivableService _receivableService)
        {
            IDictionary<int, decimal> ValuePairReceivableIdAmount = new Dictionary<int, decimal>();
            IList<ReceiptVoucherDetail> details = _receiptVoucherDetailService.GetObjectsByReceiptVoucherId(receiptVoucher.Id);

            // group all details by ReceivableId, and store the total amount
            foreach (var detail in details)
            {
                if (ValuePairReceivableIdAmount.ContainsKey(detail.ReceivableId))
                {
                    ValuePairReceivableIdAmount[detail.ReceivableId] += detail.Amount;
                }
                else
                {
                    ValuePairReceivableIdAmount.Add(detail.ReceivableId, detail.Amount);
                }
            }

            // check if the total amount is less than Receivable.RemainingAmount
            foreach (var valuePair in ValuePairReceivableIdAmount)
            {
                Receivable receivable = _receivableService.GetObjectById(valuePair.Key);
                if (receivable.RemainingAmount < valuePair.Value)
                {
                    receiptVoucher.Errors.Add("RemainingAmount", "Tidak boleh kurang dari amount dari Receipt Voucher Details");
                    return receiptVoucher;
                }
            }
            return receiptVoucher;
        }

        public ReceiptVoucher VUnconfirmableDetails(ReceiptVoucher receiptVoucher, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService, ICashBankService _cashBankService, IReceivableService _receivableService)
        {
            IList<ReceiptVoucherDetail> details = _receiptVoucherDetailService.GetObjectsByReceiptVoucherId(receiptVoucher.Id);
            foreach (var detail in details)
            {
                if (!_receiptVoucherDetailService.GetValidator().ValidUnconfirmObject(detail, _receiptVoucherService, _receiptVoucherDetailService, _cashBankService, _receivableService))
                {
                    foreach (var error in detail.Errors)
                    {
                        receiptVoucher.Errors.Add(error.Key, error.Value);
                    }
                    if (receiptVoucher.Errors.Any()) { return receiptVoucher; }
                }
            }
            return receiptVoucher;
        }

        public ReceiptVoucher VUpdateContact(ReceiptVoucher receiptVoucher, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService)
        {
            ReceiptVoucher databaseReceiptVoucher = _receiptVoucherService.GetObjectById(receiptVoucher.Id);
            if (isValid(VHasReceiptVoucherDetails(receiptVoucher, _receiptVoucherDetailService)))
            {
                if (receiptVoucher.ContactId != databaseReceiptVoucher.ContactId)
                {
                    receiptVoucher.Errors.Add("ContactId", "Tidak boleh diubah");
                }
            }
            return receiptVoucher;
        }

        public ReceiptVoucher VIsConfirmed(ReceiptVoucher receiptVoucher)
        {
            if (receiptVoucher.IsConfirmed)
            {
                receiptVoucher.Errors.Add("IsConfirmed", "Tidak boleh sudah dikonfirmasi");
            }
            return receiptVoucher;
        }

        public ReceiptVoucher VCreateObject(ReceiptVoucher receiptVoucher, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService, IReceivableService _receivableService, IContactService _contactService, ICashBankService _cashBankService)
        {
            VHasContact(receiptVoucher, _contactService);
            VHasCashBank(receiptVoucher, _cashBankService);
            VHasReceiptDate(receiptVoucher);
            return receiptVoucher;
        }

        public ReceiptVoucher VUpdateObject(ReceiptVoucher receiptVoucher, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService, IReceivableService _receivableService, IContactService _contactService, ICashBankService _cashBankService)
        {
            VIsConfirmed(receiptVoucher);
            VHasContact(receiptVoucher, _contactService);
            VHasCashBank(receiptVoucher, _cashBankService);
            VHasReceiptDate(receiptVoucher);
            VUpdateContact(receiptVoucher, _receiptVoucherService, _receiptVoucherDetailService);
            return receiptVoucher;
        }

        public ReceiptVoucher VDeleteObject(ReceiptVoucher receiptVoucher, IReceiptVoucherDetailService _receiptVoucherDetailService)
        {
            VIsConfirmed(receiptVoucher);
            return receiptVoucher;
        }

        public ReceiptVoucher VConfirmObject(ReceiptVoucher receiptVoucher, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService, ICashBankService _cashBankService, IReceivableService _receivableService, IContactService _contactService)
        {
            VHasReceiptVoucherDetails(receiptVoucher, _receiptVoucherDetailService);
            VRemainingAmountDetails(receiptVoucher, _receiptVoucherDetailService, _receivableService);
            return receiptVoucher;
        }

        public ReceiptVoucher VUnconfirmObject(ReceiptVoucher receiptVoucher, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService, ICashBankService _cashBankService, IReceivableService _receivableService, IContactService _contactService)
        {
            VUnconfirmableDetails(receiptVoucher, _receiptVoucherService, _receiptVoucherDetailService, _cashBankService, _receivableService);
            return receiptVoucher;
        }

        public bool ValidCreateObject(ReceiptVoucher receiptVoucher, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService, IReceivableService _receivableService, IContactService _contactService, ICashBankService _cashBankService)
        {
            VCreateObject(receiptVoucher, _receiptVoucherService, _receiptVoucherDetailService, _receivableService, _contactService, _cashBankService);
            return isValid(receiptVoucher);
        }

        public bool ValidUpdateObject(ReceiptVoucher receiptVoucher, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService, IReceivableService _receivableService, IContactService _contactService, ICashBankService _cashBankService)
        {
            receiptVoucher.Errors.Clear();
            VUpdateObject(receiptVoucher, _receiptVoucherService, _receiptVoucherDetailService, _receivableService, _contactService, _cashBankService);
            return isValid(receiptVoucher);
        }

        public bool ValidDeleteObject(ReceiptVoucher receiptVoucher, IReceiptVoucherDetailService _receiptVoucherDetailService)
        {
            receiptVoucher.Errors.Clear();
            VDeleteObject(receiptVoucher, _receiptVoucherDetailService);
            return isValid(receiptVoucher);
        }

        public bool ValidConfirmObject(ReceiptVoucher receiptVoucher, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService, ICashBankService _cashBankService, IReceivableService _receivableService, IContactService _contactService)
        {
            receiptVoucher.Errors.Clear();
            VConfirmObject(receiptVoucher, _receiptVoucherService, _receiptVoucherDetailService, _cashBankService, _receivableService, _contactService);
            return isValid(receiptVoucher);
        }

        public bool ValidUnconfirmObject(ReceiptVoucher receiptVoucher, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService, ICashBankService _cashBankService, IReceivableService _receivableService, IContactService _contactService)
        {
            receiptVoucher.Errors.Clear();
            VUnconfirmObject(receiptVoucher, _receiptVoucherService, _receiptVoucherDetailService, _cashBankService, _receivableService, _contactService);
            return isValid(receiptVoucher);
        }

        public bool isValid(ReceiptVoucher obj)
        {
            bool isValid = !obj.Errors.Any();
            return isValid;
        }

        public string PrintError(ReceiptVoucher obj)
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