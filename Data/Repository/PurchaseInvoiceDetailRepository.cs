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
            return Find(pid => pid.Id == Id);
        }

        public PurchaseInvoiceDetail CreateObject(PurchaseInvoiceDetail purchaseInvoiceDetail)
        {
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
    }
}