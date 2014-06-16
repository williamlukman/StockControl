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
            return FindAll(pod => pod.PurchaseOrderId == purchaseOrderId && !pod.IsDeleted).ToList();
        }

        public PurchaseOrderDetail GetObjectById(int Id)
        {
            PurchaseOrderDetail detail = Find(pod => pod.Id == Id);
            if (detail != null) { detail.Errors = new Dictionary<string, string>(); }
            return detail;
        }

        public PurchaseOrderDetail CreateObject(PurchaseOrderDetail purchaseOrderDetail)
        {
            string ParentCode = "";
            using (var db = GetContext())
            {
                ParentCode = (from obj in db.PurchaseOrders
                              where obj.Id == purchaseOrderDetail.PurchaseOrderId
                              select obj.Code).FirstOrDefault();
            }
            purchaseOrderDetail.Code = SetObjectCode(ParentCode);
            purchaseOrderDetail.IsConfirmed = false;
            purchaseOrderDetail.IsFulfilled = false;
            purchaseOrderDetail.IsDeleted = false;
            purchaseOrderDetail.CreatedAt = DateTime.Now;
            return Create(purchaseOrderDetail);
        }

        public PurchaseOrderDetail UpdateObject(PurchaseOrderDetail purchaseOrderDetail)
        {
            purchaseOrderDetail.ModifiedAt = DateTime.Now;
            Update(purchaseOrderDetail);
            return purchaseOrderDetail;
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
            PurchaseOrderDetail pod = Find(x => x.Id == Id);
            return (Delete(pod) == 1) ? true : false;
        }

        public PurchaseOrderDetail ConfirmObject(PurchaseOrderDetail purchaseOrderDetail)
        {
            purchaseOrderDetail.IsConfirmed = true;
            Update(purchaseOrderDetail);
            return purchaseOrderDetail;
        }

        public PurchaseOrderDetail UnconfirmObject(PurchaseOrderDetail purchaseOrderDetail)
        {
            purchaseOrderDetail.IsConfirmed = false;
            Update(purchaseOrderDetail);
            return purchaseOrderDetail;
        }

        public PurchaseOrderDetail FulfilObject(PurchaseOrderDetail purchaseOrderDetail)
        {
            purchaseOrderDetail.IsFulfilled = true;
            Update(purchaseOrderDetail);
            return purchaseOrderDetail;
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