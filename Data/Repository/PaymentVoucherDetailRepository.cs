using Core.DomainModel;
using Core.Interface.Repository;
using Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class PaymentVoucherDetailRepository : EfRepository<PaymentVoucherDetail>, IPaymentVoucherDetailRepository
    {
        private StockControlEntities stocks;
        public PaymentVoucherDetailRepository()
        {
            stocks = new StockControlEntities();
        }

        public IList<PaymentVoucherDetail> GetObjectsByPaymentVoucherId(int paymentVoucherId)
        {
            return FindAll(pvd => pvd.PaymentVoucherId == paymentVoucherId && !pvd.IsDeleted).ToList();
        }

        public IList<PaymentVoucherDetail> GetObjectsByPayableId(int payableId)
        {
            return FindAll(pvd => pvd.PayableId == payableId && !pvd.IsDeleted).ToList();
        }

        public PaymentVoucherDetail GetObjectById(int Id)
        {
            return Find(pvd => pvd.Id == Id);
        }

        public PaymentVoucherDetail CreateObject(PaymentVoucherDetail paymentVoucherDetail)
        {
            paymentVoucherDetail.IsConfirmed = false;
            paymentVoucherDetail.IsDeleted = false;
            paymentVoucherDetail.CreatedAt = DateTime.Now;
            return Create(paymentVoucherDetail);
        }

        public PaymentVoucherDetail UpdateObject(PaymentVoucherDetail paymentVoucherDetail)
        {
            paymentVoucherDetail.ModifiedAt = DateTime.Now;
            Update(paymentVoucherDetail);
            return paymentVoucherDetail;
        }

        public PaymentVoucherDetail SoftDeleteObject(PaymentVoucherDetail paymentVoucherDetail)
        {
            paymentVoucherDetail.IsDeleted = true;
            paymentVoucherDetail.DeletedAt = DateTime.Now;
            Update(paymentVoucherDetail);
            return paymentVoucherDetail;
        }

        public bool DeleteObject(int Id)
        {
            PaymentVoucherDetail pvd = Find(x => x.Id == Id);
            return (Delete(pvd) == 1) ? true : false;
        }

        public PaymentVoucherDetail ConfirmObject(PaymentVoucherDetail paymentVoucherDetail)
        {
            paymentVoucherDetail.IsConfirmed = true;
            Update(paymentVoucherDetail);
            return paymentVoucherDetail;
        }

        public PaymentVoucherDetail UnconfirmObject(PaymentVoucherDetail paymentVoucherDetail)
        {
            paymentVoucherDetail.IsConfirmed = false;
            Update(paymentVoucherDetail);
            return paymentVoucherDetail;
        }

        public PaymentVoucherDetail ClearObject(PaymentVoucherDetail paymentVoucherDetail)
        {
            paymentVoucherDetail.IsCleared = true;
            Update(paymentVoucherDetail);
            return paymentVoucherDetail;
        }

        public PaymentVoucherDetail UnclearObject(PaymentVoucherDetail paymentVoucherDetail)
        {
            paymentVoucherDetail.IsCleared = false;
            Update(paymentVoucherDetail);
            return paymentVoucherDetail;
        }

    }
}