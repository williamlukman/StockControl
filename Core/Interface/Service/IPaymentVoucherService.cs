using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Service
{
    public interface IPaymentVoucherService
    {
        IList<PaymentVoucher> GetObjectsByPayableId(int payableId);
        PaymentVoucher GetObjectById(int Id);
        IList<PaymentVoucher> GetObjectsByContactId(int contactId);
        PaymentVoucher CreateObject(PaymentVoucher paymentVoucher);
        PaymentVoucher UpdateObject(PaymentVoucher paymentVoucher);
        PaymentVoucher SoftDeleteObject(PaymentVoucher paymentVoucher);
        bool DeleteObject(int Id);
        PaymentVoucher ConfirmObject(PaymentVoucher paymentVoucher);
        PaymentVoucher UnconfirmObject(PaymentVoucher paymentVoucher);
    }
}