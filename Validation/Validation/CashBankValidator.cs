using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Core.Interface.Validation;
using Core.DomainModel;
using Core.Interface.Service;

namespace Validation.Validation
{
    public class CashBankValidator : ICashBankValidator
    {

        public CashBank VName(CashBank c, ICashBankService _cashBankService)
        {
            if (String.IsNullOrEmpty(c.Name) || c.Name.Trim() == "")
            {
                c.Errors.Add("Name", "Tidak boleh kosong");
            }
            if (_cashBankService.IsNameDuplicated(c.Name))
            {
                c.Errors.Add("Name", "Tidak boleh ada duplikasi");
            }
            return c;
        }

        public CashBank VIsBank(CashBank c)
        {
            /* IsBank value will never be null
            if (c.IsBank == null)
            {
                c.Errors.Add("IsBank", "Tidak boleh kosong");
            }
             */
            return c;
        }

        public CashBank VCreateObject(CashBank c, ICashBankService _cashBankService)
        {
            VName(c, _cashBankService);
            VIsBank(c);
            return c;
        }

        public CashBank VUpdateObject(CashBank c, ICashBankService _cashBankService)
        {
            VName(c, _cashBankService);
            VIsBank(c);
            return c;
        }

        public CashBank VHasReceiptVoucherDetail(CashBank cb, IReceiptVoucherDetailService _rvds)
        {
            IList<ReceiptVoucherDetail> vouchers = _rvds.GetObjectsByCashBankId(cb.Id);
            if (!vouchers.Any())
            {
                cb.Errors.Add("ReceiptVoucherDetail", "Tidak boleh ada asosiasi dengan segala macam pembayaran");
            }
            return cb;
        }

        public CashBank VHasPaymentVoucherDetail(CashBank cb, IPaymentVoucherDetailService _pvds)
        {
            IList<PaymentVoucherDetail> vouchers = _pvds.GetObjectsByCashBankId(cb.Id);
            if (!vouchers.Any())
            {
                cb.Errors.Add("PaymentVoucherDetail", "Tidak boleh ada asosiasi dengan segala macam pembayaran");
            }
            return cb;
        }

        public CashBank VDeleteObject(CashBank cb, ICashBankService _cb, IReceiptVoucherDetailService _rvds, IPaymentVoucherDetailService _pvds)
        {
            VHasReceiptVoucherDetail(cb, _rvds);
            VHasPaymentVoucherDetail(cb, _pvds);
            return cb;
        }

        public bool ValidCreateObject(CashBank c, ICashBankService _cashBankService)
        {
            VCreateObject(c, _cashBankService);
            return isValid(c);
        }

        public bool ValidUpdateObject(CashBank c, ICashBankService _cashBankService)
        {
            c.Errors.Clear();
            VUpdateObject(c, _cashBankService);
            return isValid(c);
        }

        public bool ValidDeleteObject(CashBank cb, ICashBankService _cb, IReceiptVoucherDetailService _rvds, IPaymentVoucherDetailService _pvds)
        {
            cb.Errors.Clear();
            VDeleteObject(cb, _cb, _rvds, _pvds);
            return isValid(cb);
        }

        public bool isValid(CashBank obj)
        {
            bool isValid = !obj.Errors.Any();
            return isValid;
        }

        public string PrintError(CashBank obj)
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
