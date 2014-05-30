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
    public class PurchaseReceivalRepository : EfRepository<PurchaseReceival>, IPurchaseReceivalRepository
    {
        private StockControlEntities stocks;
        public PurchaseReceivalRepository()
        {
            stocks = new StockControlEntities();
        }

        public IList<PurchaseReceival> GetAll()
        {
            return FindAll(pr => !pr.IsDeleted).ToList();
        }

        public PurchaseReceival GetObjectById(int Id)
        {
            return Find(pr => pr.Id == Id && !pr.IsDeleted);
        }

        public IList<PurchaseReceival> GetObjectsByContactId(int contactId)
        {
            return FindAll(pr => pr.CustomerId == contactId && !pr.IsDeleted).ToList();
        }

        public PurchaseReceival CreateObject(PurchaseReceival purchaseReceival)
        {
            purchaseReceival.IsDeleted = false;
            purchaseReceival.IsConfirmed = false;
            purchaseReceival.CreatedAt = DateTime.Now;
            return Create(purchaseReceival);
        }

        public PurchaseReceival UpdateObject(PurchaseReceival purchaseReceival)
        {
            purchaseReceival.ModifiedAt = DateTime.Now;
            Update(purchaseReceival);
            return purchaseReceival;
        }

        public PurchaseReceival SoftDeleteObject(PurchaseReceival purchaseReceival)
        {
            purchaseReceival.IsDeleted = true;
            purchaseReceival.DeletedAt = DateTime.Now;
            Update(purchaseReceival);
            return purchaseReceival;
        }

        public bool DeleteObject(int Id)
        {
            PurchaseReceival pr = Find(x => x.Id == Id);
            return (Delete(pr) == 1) ? true : false;
        }

        public PurchaseReceival ConfirmObject(PurchaseReceival purchaseReceival)
        {
            purchaseReceival.IsConfirmed = true;
            purchaseReceival.ConfirmedAt = DateTime.Now;
            Update(purchaseReceival);
            return purchaseReceival;
        }

        public PurchaseReceival UnconfirmObject(PurchaseReceival purchaseReceival)
        {
            purchaseReceival.IsConfirmed = false;
            Update(purchaseReceival);
            return purchaseReceival;
        }
    }
}