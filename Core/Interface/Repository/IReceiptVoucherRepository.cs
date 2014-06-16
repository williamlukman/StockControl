using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Repository
{
    public interface IReceiptVoucherRepository : IRepository<ReceiptVoucher>
    {
        IList<ReceiptVoucher> GetAll();
        IList<ReceiptVoucher> GetObjectsByCashBankId(int cashBankId);
        ReceiptVoucher GetObjectById(int Id);
        IList<ReceiptVoucher> GetObjectsByContactId(int contactId);
        ReceiptVoucher CreateObject(ReceiptVoucher receiptVoucher);
        ReceiptVoucher UpdateObject(ReceiptVoucher receiptVoucher);
        ReceiptVoucher SoftDeleteObject(ReceiptVoucher receiptVoucher);
        bool DeleteObject(int Id);
        ReceiptVoucher ConfirmObject(ReceiptVoucher receiptVoucher);
        ReceiptVoucher UnconfirmObject(ReceiptVoucher receiptVoucher);
        string SetObjectCode();
    }
}