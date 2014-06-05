using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DomainModel;
using Core.Interface.Service;

namespace Core.Interface.Validation
{
    public interface ICashBankValidator
    {
        CashBank VName(CashBank cb, ICashBankService _cb);
        CashBank VIsBank(CashBank cb);
        CashBank VHasReceiptVoucherDetail(CashBank cb, IReceiptVoucherDetailService _rvds);
        CashBank VHasPaymentVoucherDetail(CashBank cb, IPaymentVoucherDetailService _pvds);
        CashBank VCreateObject(CashBank cb, ICashBankService _cb);
        CashBank VUpdateObject(CashBank cb, ICashBankService _cb);
        CashBank VDeleteObject(CashBank cb, ICashBankService _cb, IReceiptVoucherDetailService _rvds, IPaymentVoucherDetailService _pvds);
        bool ValidCreateObject(CashBank cb, ICashBankService _cb);
        bool ValidUpdateObject(CashBank cb, ICashBankService _cb);
        bool ValidDeleteObject(CashBank cb, ICashBankService _cb, IReceiptVoucherDetailService _rvds, IPaymentVoucherDetailService _pvds);
        bool isValid(CashBank cb);
        string PrintError(CashBank cb);
    }
}
