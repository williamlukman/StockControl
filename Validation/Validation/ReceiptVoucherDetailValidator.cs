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
    public class ReceiptVoucherDetailValidator : IReceiptVoucherDetailValidator
    {
        public ReceiptVoucherDetail VHasReceiptVoucher(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService)
        {
            ReceiptVoucher receiptVoucher = _receiptVoucherService.GetObjectById(receiptVoucherDetail.ReceiptVoucherId);
            if (receiptVoucher == null)
            {
                receiptVoucherDetail.Errors.Add("ReceiptVoucher", "Tidak boleh tidak ada");
            }
            return receiptVoucherDetail;
        }

        public ReceiptVoucherDetail VHasReceivable(ReceiptVoucherDetail receiptVoucherDetail, IReceivableService _receivableService)
        {
            Receivable receivable = _receivableService.GetObjectById(receiptVoucherDetail.ReceivableId);
            if (receivable == null)
            {
                receiptVoucherDetail.Errors.Add("Receivable", "Tidak boleh tidak ada");
            }
            return receiptVoucherDetail;
        }

        public ReceiptVoucherDetail VReceivableContactIsTheSame(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService, IReceivableService _receivableService)
        {
            ReceiptVoucher receiptVoucher = _receiptVoucherService.GetObjectById(receiptVoucherDetail.ReceiptVoucherId);
            Receivable receivable = _receivableService.GetObjectById(receiptVoucherDetail.ReceivableId);
            if (receivable.ContactId != receiptVoucher.ContactId)
            {
                receiptVoucherDetail.Errors.Add("Contact", "Tidak boleh tidak sama dengan Receivable");
            }
            return receiptVoucherDetail;
        }

        public ReceiptVoucherDetail VAmountLessThanCashBank(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService, ICashBankService _cashBankService)
        {
            ReceiptVoucher receiptVoucher = _receiptVoucherService.GetObjectById(receiptVoucherDetail.ReceiptVoucherId);
            CashBank cashBank = _cashBankService.GetObjectById(receiptVoucher.CashBankId);
            if (receiptVoucherDetail.Amount > cashBank.Amount)
            {
                receiptVoucherDetail.Errors.Add("Amount", "Tidak boleh lebih dari cash bank");
            }
            return receiptVoucherDetail;
        }

        public ReceiptVoucherDetail VCorrectInstantClearance(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService, ICashBankService _cashBankService)
        {
            ReceiptVoucher receiptVoucher = _receiptVoucherService.GetObjectById(receiptVoucherDetail.ReceiptVoucherId);
            CashBank cashBank = _cashBankService.GetObjectById(receiptVoucher.CashBankId);
            if ((!cashBank.IsBank) && (!receiptVoucher.IsInstantClearance))
            {
                receiptVoucherDetail.Errors.Add("IsInstantClearance", "Harus true untuk pembayaran cash");
            }
            return receiptVoucherDetail;

        }

        public ReceiptVoucherDetail VUniqueCashBankId(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService, ICashBankService _cashBankService)
        {
            // CashBank harus selalu unique untuk setiap payment voucher detail
            // Tidak pernah tidak unique karena cashBank terasosiasi dengan receiptVoucher
            return receiptVoucherDetail;
        }

        public ReceiptVoucherDetail VNonNegativeOrZeroAmount(ReceiptVoucherDetail receiptVoucherDetail)
        {
            if (receiptVoucherDetail.Amount <= 0)
            {
                receiptVoucherDetail.Errors.Add("Amount", "Tidak boleh kurag dari atau sama dengan 0");
            }
            return receiptVoucherDetail;
        }

        public ReceiptVoucherDetail VIsConfirmed(ReceiptVoucherDetail receiptVoucherDetail)
        {
            if (receiptVoucherDetail.IsConfirmed)
            {
                receiptVoucherDetail.Errors.Add("IsConfirmed", "Tidak boleh sudah dikonfirmasi");
            }
            return receiptVoucherDetail;
        }

        public ReceiptVoucherDetail VUpdateContactOrReceivable(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherDetailService _receiptVoucherDetailService)
        {
            ReceiptVoucherDetail databaseReceiptVoucherDetail = _receiptVoucherDetailService.GetObjectById(receiptVoucherDetail.Id);
            if (receiptVoucherDetail.ContactId != databaseReceiptVoucherDetail.ContactId)
            {
                receiptVoucherDetail.Errors.Add("ContactId", "Tidak boleh diubah");
            }
            if (receiptVoucherDetail.ReceivableId != databaseReceiptVoucherDetail.ReceivableId)
            {
                receiptVoucherDetail.Errors.Add("ReceivableId", "Tidak boleh diubah");
            }
            return receiptVoucherDetail;
        }

        public ReceiptVoucherDetail VRemainingAmount(ReceiptVoucherDetail receiptVoucherDetail, IReceivableService _receivableService)
        {
            Receivable receivable = _receivableService.GetObjectById(receiptVoucherDetail.ReceivableId);
            if (receivable.RemainingAmount < receiptVoucherDetail.Amount)
            {
                receiptVoucherDetail.Errors.Add("RemainingAmount", "Harus lebih atau sama dengan amount");
            }
            return receiptVoucherDetail;
        }

        public ReceiptVoucherDetail VIsClearedForNonInstantClearance(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService)
        {
            ReceiptVoucher receiptVoucher = _receiptVoucherService.GetObjectById(receiptVoucherDetail.ReceiptVoucherId);
            if (!receiptVoucher.IsInstantClearance)
            {
                if (receiptVoucherDetail.IsCleared)
                {
                    receiptVoucherDetail.Errors.Add("IsCleared", "Tidak boleh sudah di clear kan");
                }
            }
            return receiptVoucherDetail;
        }

        public ReceiptVoucherDetail VIsInstantClearance(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService)
        {
            ReceiptVoucher receiptVoucher = _receiptVoucherService.GetObjectById(receiptVoucherDetail.ReceiptVoucherId);
            if (!receiptVoucher.IsInstantClearance)
            {
                receiptVoucherDetail.Errors.Add("IsIntantClearance", "Harus true karena non-bank");
            }
            return receiptVoucherDetail;
        }

        public ReceiptVoucherDetail VIsNonInstantClearance(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService)
        {
            ReceiptVoucher receiptVoucher = _receiptVoucherService.GetObjectById(receiptVoucherDetail.ReceiptVoucherId);
            if (receiptVoucher.IsInstantClearance)
            {
                receiptVoucherDetail.Errors.Add("IsIntantClearance", "Harus false karena bank");
            }
            return receiptVoucherDetail;
        }

        public ReceiptVoucherDetail VClearanceDate(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService)
        {
            return receiptVoucherDetail;
        }

        public ReceiptVoucherDetail VCreateObject(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService, ICashBankService _cashBankService, IReceivableService _receivableService, IContactService _contactService)
        {
            VHasReceiptVoucher(receiptVoucherDetail, _receiptVoucherService);
            if (!isValid(receiptVoucherDetail)) { return receiptVoucherDetail; }
            VHasReceivable(receiptVoucherDetail, _receivableService);
            if (!isValid(receiptVoucherDetail)) { return receiptVoucherDetail; }
            VReceivableContactIsTheSame(receiptVoucherDetail, _receiptVoucherService, _receivableService);
            if (!isValid(receiptVoucherDetail)) { return receiptVoucherDetail; }
            VAmountLessThanCashBank(receiptVoucherDetail, _receiptVoucherService, _cashBankService);
            if (!isValid(receiptVoucherDetail)) { return receiptVoucherDetail; }
            VCorrectInstantClearance(receiptVoucherDetail, _receiptVoucherService, _cashBankService);
            if (!isValid(receiptVoucherDetail)) { return receiptVoucherDetail; }
            VNonNegativeOrZeroAmount(receiptVoucherDetail);
            return receiptVoucherDetail;
        }

        public ReceiptVoucherDetail VUpdateObject(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService, ICashBankService _cashBankService, IReceivableService _receivableService, IContactService _contactService)
        {
            VIsConfirmed(receiptVoucherDetail);
            if (!isValid(receiptVoucherDetail)) { return receiptVoucherDetail; }
            VHasReceiptVoucher(receiptVoucherDetail, _receiptVoucherService);
            if (!isValid(receiptVoucherDetail)) { return receiptVoucherDetail; }
            VHasReceivable(receiptVoucherDetail, _receivableService);
            if (!isValid(receiptVoucherDetail)) { return receiptVoucherDetail; }
            VUpdateContactOrReceivable(receiptVoucherDetail, _receiptVoucherDetailService);
            if (!isValid(receiptVoucherDetail)) { return receiptVoucherDetail; }
            VAmountLessThanCashBank(receiptVoucherDetail, _receiptVoucherService, _cashBankService);
            if (!isValid(receiptVoucherDetail)) { return receiptVoucherDetail; }
            VCorrectInstantClearance(receiptVoucherDetail, _receiptVoucherService, _cashBankService);
            return receiptVoucherDetail;
        }

        public ReceiptVoucherDetail VDeleteObject(ReceiptVoucherDetail receiptVoucherDetail)
        {
            VIsConfirmed(receiptVoucherDetail);
            return receiptVoucherDetail;
        }

        public ReceiptVoucherDetail VConfirmObject(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService, ICashBankService _cashBankService, IReceivableService _receivableService)
        {
            VAmountLessThanCashBank(receiptVoucherDetail, _receiptVoucherService, _cashBankService);
            return receiptVoucherDetail;
        }

        public ReceiptVoucherDetail VUnconfirmObject(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService, ICashBankService _cashBankService, IReceivableService _receivableService)
        {
            VIsClearedForNonInstantClearance(receiptVoucherDetail, _receiptVoucherService);
            return receiptVoucherDetail;
        }

        public ReceiptVoucherDetail VClearObject(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService, ICashBankService _cashBankService, IReceivableService _receivableService)
        {
            VIsNonInstantClearance(receiptVoucherDetail, _receiptVoucherService);
            if (!isValid(receiptVoucherDetail)) { return receiptVoucherDetail; }
            VClearanceDate(receiptVoucherDetail, _receiptVoucherService);
            if (!isValid(receiptVoucherDetail)) { return receiptVoucherDetail; }
            VAmountLessThanCashBank(receiptVoucherDetail, _receiptVoucherService, _cashBankService);
            return receiptVoucherDetail;
        }

        public ReceiptVoucherDetail VUnclearObject(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService, ICashBankService _cashBankService, IReceivableService _receivableService)
        {
            VIsNonInstantClearance(receiptVoucherDetail, _receiptVoucherService);
            return receiptVoucherDetail;
        }

        public ReceiptVoucherDetail VClearConfirmObject(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService, ICashBankService _cashBankService, IReceivableService _receivableService)
        {
            VIsInstantClearance(receiptVoucherDetail, _receiptVoucherService);
            if (!isValid(receiptVoucherDetail)) { return receiptVoucherDetail; }
            VClearanceDate(receiptVoucherDetail, _receiptVoucherService);
            if (!isValid(receiptVoucherDetail)) { return receiptVoucherDetail; }
            VAmountLessThanCashBank(receiptVoucherDetail, _receiptVoucherService, _cashBankService);
            return receiptVoucherDetail;
        }

        public ReceiptVoucherDetail VUnclearUnconfirmObject(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService, ICashBankService _cashBankService, IReceivableService _receivableService)
        {
            return receiptVoucherDetail;
        }

        public bool ValidCreateObject(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService, ICashBankService _cashBankService, IReceivableService _receivableService, IContactService _contactService)
        {
            VCreateObject(receiptVoucherDetail, _receiptVoucherService, _receiptVoucherDetailService, _cashBankService, _receivableService, _contactService);
            return isValid(receiptVoucherDetail);
        }

        public bool ValidUpdateObject(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService, ICashBankService _cashBankService, IReceivableService _receivableService, IContactService _contactService)
        {
            VUpdateObject(receiptVoucherDetail, _receiptVoucherService, _receiptVoucherDetailService, _cashBankService, _receivableService, _contactService);
            return isValid(receiptVoucherDetail);
        }

        public bool ValidDeleteObject(ReceiptVoucherDetail receiptVoucherDetail)
        {
            VDeleteObject(receiptVoucherDetail);
            return isValid(receiptVoucherDetail);
        }

        public bool ValidConfirmObject(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService, ICashBankService _cashBankService, IReceivableService _receivableService)
        {
            VConfirmObject(receiptVoucherDetail, _receiptVoucherService, _receiptVoucherDetailService, _cashBankService, _receivableService);
            return isValid(receiptVoucherDetail);
        }

        public bool ValidUnconfirmObject(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService, ICashBankService _cashBankService, IReceivableService _receivableService)
        {
            VUnconfirmObject(receiptVoucherDetail, _receiptVoucherService, _receiptVoucherDetailService, _cashBankService, _receivableService);
            return isValid(receiptVoucherDetail);
        }

        public bool ValidClearObject(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService, ICashBankService _cashBankService, IReceivableService _receivableService)
        {
            VClearObject(receiptVoucherDetail, _receiptVoucherService, _receiptVoucherDetailService, _cashBankService, _receivableService);
            return isValid(receiptVoucherDetail);
        }

        public bool ValidUnclearObject(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService, ICashBankService _cashBankService, IReceivableService _receivableService)
        {
            VUnclearObject(receiptVoucherDetail, _receiptVoucherService, _receiptVoucherDetailService, _cashBankService, _receivableService);
            return isValid(receiptVoucherDetail);
        }

        public bool ValidClearConfirmObject(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService, ICashBankService _cashBankService, IReceivableService _receivableService)
        {
            VClearConfirmObject(receiptVoucherDetail, _receiptVoucherService, _receiptVoucherDetailService, _cashBankService, _receivableService);
            return isValid(receiptVoucherDetail);
        }

        public bool ValidUnclearUnconfirmObject(ReceiptVoucherDetail receiptVoucherDetail, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService, ICashBankService _cashBankService, IReceivableService _receivableService)
        {
            VUnclearUnconfirmObject(receiptVoucherDetail, _receiptVoucherService, _receiptVoucherDetailService, _cashBankService, _receivableService);
            return isValid(receiptVoucherDetail);
        }

        public bool isValid(ReceiptVoucherDetail obj)
        {
            bool isValid = !obj.Errors.Any();
            return isValid;
        }

        public string PrintError(ReceiptVoucherDetail obj)
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