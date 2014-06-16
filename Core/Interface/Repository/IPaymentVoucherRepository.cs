using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Repository
{
    public interface IPaymentVoucherRepository : IRepository<PaymentVoucher>
    {
        IList<PaymentVoucher> GetAll();
        IList<PaymentVoucher> GetObjectsByCashBankId(int cashBankId);
        PaymentVoucher GetObjectById(int Id);
        IList<PaymentVoucher> GetObjectsByContactId(int contactId);
        PaymentVoucher CreateObject(PaymentVoucher paymentVoucher);
        PaymentVoucher UpdateObject(PaymentVoucher paymentVoucher);
        PaymentVoucher SoftDeleteObject(PaymentVoucher paymentVoucher);
        bool DeleteObject(int Id);
        PaymentVoucher ConfirmObject(PaymentVoucher paymentVoucher);
        PaymentVoucher UnconfirmObject(PaymentVoucher paymentVoucher);
        string SetObjectCode();
    }
}