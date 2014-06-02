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
    public class PurchaseOrderRepository : EfRepository<PurchaseOrder>, IPurchaseOrderRepository
    {
        private StockControlEntities stocks;
        public PurchaseOrderRepository()
        {
            stocks = new StockControlEntities();
        }

        public IList<PurchaseOrder> GetAll()
        {
            return FindAll(po => !po.IsDeleted).ToList();
        }

        public PurchaseOrder GetObjectById(int Id)
        {
            return Find(po => po.Id == Id && !po.IsDeleted);
        }

        public IList<PurchaseOrder> GetObjectsByContactId(int contactId)
        {
            return FindAll(po => po.ContactId == contactId && !po.IsDeleted).ToList();
        }

        public PurchaseOrder CreateObject(PurchaseOrder purchaseOrder)
        {
            purchaseOrder.IsDeleted = false;
            purchaseOrder.IsConfirmed = false;
            purchaseOrder.CreatedAt = DateTime.Now;
            return Create(purchaseOrder);
        }

        public PurchaseOrder UpdateObject(PurchaseOrder purchaseOrder)
        {
            purchaseOrder.ModifiedAt = DateTime.Now;
            Update(purchaseOrder);
            return purchaseOrder;
        }

        public PurchaseOrder SoftDeleteObject(PurchaseOrder purchaseOrder)
        {
            purchaseOrder.IsDeleted = true;
            purchaseOrder.DeletedAt = DateTime.Now;
            Update(purchaseOrder);
            return purchaseOrder;
        }

        public bool DeleteObject(int Id)
        {
            PurchaseOrder po = Find(x => x.Id == Id);
            return (Delete(po) == 1) ? true : false;
        }

        public PurchaseOrder ConfirmObject(PurchaseOrder purchaseOrder)
        {
            purchaseOrder.IsConfirmed = true;
            purchaseOrder.ConfirmedAt = DateTime.Now;
            Update(purchaseOrder);
            return purchaseOrder;
        }

        public PurchaseOrder UnconfirmObject(PurchaseOrder purchaseOrder)
        {
            purchaseOrder.IsConfirmed = false;
            Update(purchaseOrder);
            return purchaseOrder;
        }
    }
}