using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Service
{
    public interface IPaymentVoucherDetailService
    {
        IList<PaymentVoucherDetail> GetObjectsByPaymentVoucherId(int paymentVoucherId);
        PaymentVoucherDetail GetObjectById(int Id);
        IList<PaymentVoucherDetail> GetObjectsByCashBankId(int BankId);
        IList<PaymentVoucherDetail> GetObjectsByPayableId(int PayableId);
        PaymentVoucherDetail CreateObject(PaymentVoucherDetail paymentVoucherDetail);
        PaymentVoucherDetail UpdateObject(PaymentVoucherDetail paymentVoucherDetail);
        PaymentVoucherDetail SoftDeleteObject(PaymentVoucherDetail paymentVoucherDetail);
        bool DeleteObject(int Id);
        PaymentVoucherDetail ConfirmObject(PaymentVoucherDetail paymentVoucherDetail);
        PaymentVoucherDetail UnconfirmObject(PaymentVoucherDetail paymentVoucherDetail);
        PaymentVoucherDetail FulfilObject(PaymentVoucherDetail paymentVoucherDetail);

    }
}