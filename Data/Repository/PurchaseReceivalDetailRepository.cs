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
    public class PurchaseReceivalDetailRepository : EfRepository<PurchaseReceivalDetail>, IPurchaseReceivalDetailRepository
    {
        private StockControlEntities stocks;
        public PurchaseReceivalDetailRepository()
        {
            stocks = new StockControlEntities();
        }

        public IList<PurchaseReceivalDetail> GetObjectsByPurchaseReceivalId(int purchaseReceivalId)
        {
            return FindAll(prd => prd.PurchaseReceivalId == purchaseReceivalId && !prd.IsDeleted).ToList();
        }

        public PurchaseReceivalDetail GetObjectById(int Id)
        {
            PurchaseReceivalDetail purchaseReceivalDetail = Find(prd => prd.Id == Id && !prd.IsDeleted);
            if (purchaseReceivalDetail != null) { purchaseReceivalDetail.Errors = new Dictionary<string, string>(); }
            return purchaseReceivalDetail;
        }

        public PurchaseReceivalDetail GetObjectByPurchaseOrderDetailId(int purchaseOrderDetailId)
        {
            return Find(prd => prd.PurchaseOrderDetailId == purchaseOrderDetailId && !prd.IsDeleted);
        }

        public PurchaseReceivalDetail CreateObject(PurchaseReceivalDetail purchaseReceivalDetail)
        {
            purchaseReceivalDetail.IsConfirmed = false;
            purchaseReceivalDetail.IsDeleted = false;
            purchaseReceivalDetail.CreatedAt = DateTime.Now;
            return Create(purchaseReceivalDetail);
        }

        public PurchaseReceivalDetail UpdateObject(PurchaseReceivalDetail purchaseReceivalDetail)
        {
            purchaseReceivalDetail.ModifiedAt = DateTime.Now;
            Update(purchaseReceivalDetail);
            return purchaseReceivalDetail;
        }

        public PurchaseReceivalDetail SoftDeleteObject(PurchaseReceivalDetail purchaseReceivalDetail)
        {
            purchaseReceivalDetail.IsDeleted = true;
            purchaseReceivalDetail.DeletedAt = DateTime.Now;
            Update(purchaseReceivalDetail);
            return purchaseReceivalDetail;
        }

        public bool DeleteObject(int Id)
        {
            PurchaseReceivalDetail prd = Find(x => x.Id == Id);
            return (Delete(prd) == 1) ? true : false;
        }

        public PurchaseReceivalDetail ConfirmObject(PurchaseReceivalDetail purchaseReceivalDetail)
        {
            purchaseReceivalDetail.IsConfirmed = true;
            Update(purchaseReceivalDetail);
            return purchaseReceivalDetail;
        }

        public PurchaseReceivalDetail UnconfirmObject(PurchaseReceivalDetail purchaseReceivalDetail)
        {
            purchaseReceivalDetail.IsConfirmed = false;
            Update(purchaseReceivalDetail);
            return purchaseReceivalDetail;
        }
    }
}