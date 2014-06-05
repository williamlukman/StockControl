using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Service
{
    public interface IReceiptVoucherDetailService
    {
        IList<ReceiptVoucherDetail> GetObjectsByReceiptVoucherId(int receiptVoucherId);
        ReceiptVoucherDetail GetObjectById(int Id);
        IList<ReceiptVoucherDetail> GetObjectsByCashBankId(int BankId);
        ReceiptVoucherDetail CreateObject(ReceiptVoucherDetail receiptVoucherDetail);
        ReceiptVoucherDetail UpdateObject(ReceiptVoucherDetail receiptVoucherDetail);
        ReceiptVoucherDetail SoftDeleteObject(ReceiptVoucherDetail receiptVoucherDetail);
        bool DeleteObject(int Id);
        ReceiptVoucherDetail ConfirmObject(ReceiptVoucherDetail receiptVoucherDetail);
        ReceiptVoucherDetail UnconfirmObject(ReceiptVoucherDetail receiptVoucherDetail);
        ReceiptVoucherDetail FulfilObject(ReceiptVoucherDetail receiptVoucherDetail);

    }
}