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
            if (_cashBankService.IsNameDuplicated(c))
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

        public CashBank VHasReceiptVoucherDetail(CashBank cb, IReceiptVoucherService _rvs)
        {
            IList<ReceiptVoucher> vouchers = _rvs.GetObjectsByCashBankId(cb.Id);
            if (vouchers.Any())
            {
                cb.Errors.Add("ReceiptVoucher", "Tidak boleh ada asosiasi dengan segala macam receipt");
            }
            return cb;
        }

        public CashBank VHasPaymentVoucherDetail(CashBank cb, IPaymentVoucherService _pvs)
        {
            IList<PaymentVoucher> vouchers = _pvs.GetObjectsByCashBankId(cb.Id);
            if (vouchers.Any())
            {
                cb.Errors.Add("PaymentVoucher", "Tidak boleh ada asosiasi dengan segala macam pembayaran");
            }
            return cb;
        }

        public CashBank VDeleteObject(CashBank cb, ICashBankService _cb, IReceiptVoucherService _rvs, IPaymentVoucherService _pvs)
        {
            VHasReceiptVoucherDetail(cb, _rvs);
            VHasPaymentVoucherDetail(cb, _pvs);
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

        public bool ValidDeleteObject(CashBank cb, ICashBankService _cb, IReceiptVoucherService _rvs, IPaymentVoucherService _pvs)
        {
            cb.Errors.Clear();
            VDeleteObject(cb, _cb, _rvs, _pvs);
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
