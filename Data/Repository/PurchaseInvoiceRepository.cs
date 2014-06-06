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
    public class PurchaseInvoiceRepository : EfRepository<PurchaseInvoice>, IPurchaseInvoiceRepository
    {
        private StockControlEntities stocks;
        public PurchaseInvoiceRepository()
        {
            stocks = new StockControlEntities();
        }

        public IList<PurchaseInvoice> GetAll()
        {
            return FindAll(pi => !pi.IsDeleted).ToList();
        }

        public PurchaseInvoice GetObjectById(int Id)
        {
            PurchaseInvoice purchaseInvoice = Find(pi => pi.Id == Id && !pi.IsDeleted);
            if (purchaseInvoice != null) { purchaseInvoice.Errors = new Dictionary<string, string>(); }
            return purchaseInvoice;
        }

        public IList<PurchaseInvoice> GetObjectsByContactId(int contactId)
        {
            return FindAll(pi => pi.ContactId == contactId && !pi.IsDeleted).ToList();
        }

        public PurchaseInvoice CreateObject(PurchaseInvoice purchaseInvoice)
        {
            purchaseInvoice.IsDeleted = false;
            purchaseInvoice.IsConfirmed = false;
            purchaseInvoice.CreatedAt = DateTime.Now;
            return Create(purchaseInvoice);
        }

        public PurchaseInvoice UpdateObject(PurchaseInvoice purchaseInvoice)
        {
            purchaseInvoice.ModifiedAt = DateTime.Now;
            Update(purchaseInvoice);
            return purchaseInvoice;
        }

        public PurchaseInvoice SoftDeleteObject(PurchaseInvoice purchaseInvoice)
        {
            purchaseInvoice.IsDeleted = true;
            purchaseInvoice.DeletedAt = DateTime.Now;
            Update(purchaseInvoice);
            return purchaseInvoice;
        }

        public bool DeleteObject(int Id)
        {
            PurchaseInvoice pi = Find(x => x.Id == Id);
            return (Delete(pi) == 1) ? true : false;
        }

        public PurchaseInvoice ConfirmObject(PurchaseInvoice purchaseInvoice)
        {
            purchaseInvoice.IsConfirmed = true;
            Update(purchaseInvoice);
            return purchaseInvoice;
        }

        public PurchaseInvoice UnconfirmObject(PurchaseInvoice purchaseInvoice)
        {
            purchaseInvoice.IsConfirmed = false;
            Update(purchaseInvoice);
            return purchaseInvoice;
        }
    }
}