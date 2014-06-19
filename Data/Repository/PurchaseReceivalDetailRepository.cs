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
            string ParentCode = "";
            using (var db = GetContext())
            {
                ParentCode = (from obj in db.PurchaseReceivals
                              where obj.Id == purchaseReceivalDetail.PurchaseReceivalId
                              select obj.Code).FirstOrDefault();
            }
            purchaseReceivalDetail.Code = SetObjectCode(ParentCode);
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

        public string SetObjectCode(string ParentCode)
        {
            // Code: #{parent_object.code}/#{total_number_objects}
            int totalobject = FindAll().Count() + 1;
            string Code = ParentCode + "/#" + totalobject;
            return Code;
        } 

    }
}