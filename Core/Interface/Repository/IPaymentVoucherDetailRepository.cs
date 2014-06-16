using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Repository
{
    public interface IPaymentVoucherDetailRepository : IRepository<PaymentVoucherDetail>
    {
        IList<PaymentVoucherDetail> GetObjectsByPaymentVoucherId(int paymentVoucherId);
        IList<PaymentVoucherDetail> GetObjectsByPayableId(int payableId);
        PaymentVoucherDetail GetObjectById(int Id);
        PaymentVoucherDetail CreateObject(PaymentVoucherDetail paymentVoucherDetail);
        PaymentVoucherDetail UpdateObject(PaymentVoucherDetail paymentVoucherDetail);
        PaymentVoucherDetail SoftDeleteObject(PaymentVoucherDetail paymentVoucherDetail);
        bool DeleteObject(int Id);
        PaymentVoucherDetail ConfirmObject(PaymentVoucherDetail paymentVoucherDetail);
        PaymentVoucherDetail UnconfirmObject(PaymentVoucherDetail paymentVoucherDetail);
        PaymentVoucherDetail ClearObject(PaymentVoucherDetail paymentVoucherDetail);
        PaymentVoucherDetail UnclearObject(PaymentVoucherDetail paymentVoucherDetail);
        string SetObjectCode(string ParentCode);
    }
}