using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Repository
{
    public interface IReceiptVoucherDetailRepository : IRepository<ReceiptVoucherDetail>
    {
        IList<ReceiptVoucherDetail> GetObjectsByReceiptVoucherId(int receiptVoucherId);
        IList<ReceiptVoucherDetail> GetObjectsByReceivableId(int receivableId);
        ReceiptVoucherDetail GetObjectById(int Id);
        ReceiptVoucherDetail CreateObject(ReceiptVoucherDetail receiptVoucherDetail);
        ReceiptVoucherDetail UpdateObject(ReceiptVoucherDetail receiptVoucherDetail);
        ReceiptVoucherDetail SoftDeleteObject(ReceiptVoucherDetail receiptVoucherDetail);
        bool DeleteObject(int Id);
        ReceiptVoucherDetail ConfirmObject(ReceiptVoucherDetail receiptVoucherDetail);
        ReceiptVoucherDetail UnconfirmObject(ReceiptVoucherDetail receiptVoucherDetail);
        ReceiptVoucherDetail ClearObject(ReceiptVoucherDetail receiptVoucherDetail);
        ReceiptVoucherDetail UnclearObject(ReceiptVoucherDetail receiptVoucherDetail);
        string SetObjectCode(string ParentCode);
    }
}