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
        PaymentVoucherDetail GetObjectById(int Id);
        PaymentVoucherDetail CreateObject(PaymentVoucherDetail paymentVoucherDetail);
        PaymentVoucherDetail UpdateObject(PaymentVoucherDetail paymentVoucherDetail);
        PaymentVoucherDetail SoftDeleteObject(PaymentVoucherDetail paymentVoucherDetail);
        bool DeleteObject(int Id);
        PaymentVoucherDetail ConfirmObject(PaymentVoucherDetail paymentVoucherDetail);
        PaymentVoucherDetail UnconfirmObject(PaymentVoucherDetail paymentVoucherDetail);
        PaymentVoucherDetail FulfilObject(PaymentVoucherDetail paymentVoucherDetail);

    }
}