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
    public class PurchaseInvoiceDetailRepository : EfRepository<PurchaseInvoiceDetail>, IPurchaseInvoiceDetailRepository
    {
        private StockControlEntities stocks;
        public PurchaseInvoiceDetailRepository()
        {
            stocks = new StockControlEntities();
        }

        public IList<PurchaseInvoiceDetail> GetObjectsByPurchaseInvoiceId(int purchaseInvoiceId)
        {
            return FindAll(pid => pid.PurchaseInvoiceId == purchaseInvoiceId && !pid.IsDeleted).ToList();
        }

        public PurchaseInvoiceDetail GetObjectById(int Id)
        {
            PurchaseInvoiceDetail detail = Find(pid => pid.Id == Id);
            if (detail != null) { detail.Errors = new Dictionary<string, string>(); }
            return detail;
        }

        public PurchaseInvoiceDetail CreateObject(PurchaseInvoiceDetail purchaseInvoiceDetail)
        {
            string ParentCode = "";
            using (var db = GetContext())
            {
                ParentCode = (from obj in db.PurchaseInvoices
                              where obj.Id == purchaseInvoiceDetail.PurchaseInvoiceId
                              select obj.Code).FirstOrDefault();
            }
            purchaseInvoiceDetail.Code = SetObjectCode(ParentCode);
            purchaseInvoiceDetail.IsConfirmed = false;
            purchaseInvoiceDetail.IsDeleted = false;
            purchaseInvoiceDetail.CreatedAt = DateTime.Now;
            return Create(purchaseInvoiceDetail);
        }

        public PurchaseInvoiceDetail UpdateObject(PurchaseInvoiceDetail purchaseInvoiceDetail)
        {
            purchaseInvoiceDetail.ModifiedAt = DateTime.Now;
            Update(purchaseInvoiceDetail);
            return purchaseInvoiceDetail;
        }

        public PurchaseInvoiceDetail SoftDeleteObject(PurchaseInvoiceDetail purchaseInvoiceDetail)
        {
            purchaseInvoiceDetail.IsDeleted = true;
            purchaseInvoiceDetail.DeletedAt = DateTime.Now;
            Update(purchaseInvoiceDetail);
            return purchaseInvoiceDetail;
        }

        public bool DeleteObject(int Id)
        {
            PurchaseInvoiceDetail pid = Find(x => x.Id == Id);
            return (Delete(pid) == 1) ? true : false;
        }

        public PurchaseInvoiceDetail ConfirmObject(PurchaseInvoiceDetail purchaseInvoiceDetail)
        {
            purchaseInvoiceDetail.IsConfirmed = true;
            Update(purchaseInvoiceDetail);
            return purchaseInvoiceDetail;
        }

        public PurchaseInvoiceDetail UnconfirmObject(PurchaseInvoiceDetail purchaseInvoiceDetail)
        {
            purchaseInvoiceDetail.IsConfirmed = false;
            Update(purchaseInvoiceDetail);
            return purchaseInvoiceDetail;
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