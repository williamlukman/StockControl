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
    public class PurchaseOrderDetailRepository : EfRepository<PurchaseOrderDetail>, IPurchaseOrderDetailRepository
    {
        private StockControlEntities stocks;
        public PurchaseOrderDetailRepository()
        {
            stocks = new StockControlEntities();
        }

        public IList<PurchaseOrderDetail> GetObjectsByPurchaseOrderId(int purchaseOrderId)
        {
            List<PurchaseOrderDetail> pods = (from pod in stocks.purchaseOrderDetails
                                         where pod.PurchaseOrderId == purchaseOrderId && !pod.IsDeleted
                                         select pod).ToList();
            return pods;
        }

        public PurchaseOrderDetail GetObjectById(int Id)
        {
            PurchaseOrderDetail pod = (from p in stocks.purchaseOrderDetails
                                       where p.Id == Id && !p.IsDeleted
                                       select p).FirstOrDefault();
            return pod;
        }

        public PurchaseOrderDetail CreateObject(PurchaseOrderDetail purchaseOrderDetail)
        {
            PurchaseOrderDetail pod = new PurchaseOrderDetail();
            pod.PurchaseOrderId = purchaseOrderDetail.PurchaseOrderId;
            pod.ItemId = purchaseOrderDetail.ItemId;
            pod.Quantity = purchaseOrderDetail.Quantity;
            pod.IsConfirmed = false;
            pod.IsDeleted = false;
            pod.CreatedAt = DateTime.Now;

            return Create(pod);
        }

        public PurchaseOrderDetail UpdateObject(PurchaseOrderDetail purchaseOrderDetail)
        {
            PurchaseOrderDetail pod = new PurchaseOrderDetail();
            pod.PurchaseOrderId = purchaseOrderDetail.PurchaseOrderId;
            pod.ItemId = purchaseOrderDetail.ItemId;
            pod.Quantity = purchaseOrderDetail.Quantity;
            pod.IsConfirmed = purchaseOrderDetail.IsConfirmed;
            pod.IsDeleted = purchaseOrderDetail.IsDeleted;
            pod.ModifiedAt = DateTime.Now;

            Update(pod);
            return pod;
        }

        public PurchaseOrderDetail SoftDeleteObject(PurchaseOrderDetail purchaseOrderDetail)
        {
            purchaseOrderDetail.IsDeleted = true;
            purchaseOrderDetail.DeletedAt = DateTime.Now;
            Update(purchaseOrderDetail);
            return purchaseOrderDetail;
        }

        public bool DeleteObject(int Id)
        {
            PurchaseOrderDetail pod = Find(x => x.Id == Id && !x.IsDeleted);
            return (Delete(pod) == 1) ? true : false;
        }

        PurchaseOrderDetail ConfirmObject(PurchaseOrderDetail purchaseOrderDetail)
        {
            purchaseOrderDetail.IsConfirmed = true;
            purchaseOrderDetail.ModifiedAt = DateTime.Now;
            Update(purchaseOrderDetail);
            return purchaseOrderDetail;
        }

        PurchaseOrderDetail UnconfirmObject(PurchaseOrderDetail purchaseOrderDetail)
        {
            purchaseOrderDetail.IsConfirmed = false;
            purchaseOrderDetail.ModifiedAt = DateTime.Now;
            Update(purchaseOrderDetail);
            return purchaseOrderDetail;
        }
    }
}