using Core.DomainModel;
using Core.Interface.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Service
{
    public interface ICashBankService
    {
        ICashBankValidator GetValidator();
        IList<CashBank> GetAll();
        CashBank GetObjectById(int Id);
        CashBank CreateObject(CashBank cashBank);
        CashBank UpdateObject(CashBank cashBank);
        CashBank SoftDeleteObject(CashBank cashBank, IReceiptVoucherDetailService _rvds, IPaymentVoucherDetailService _pvds);
        bool DeleteObject(int Id);
        bool IsNameDuplicated(String name);
    }
}