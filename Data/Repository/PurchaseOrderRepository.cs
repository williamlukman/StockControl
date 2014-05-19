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
            List<PurchaseOrder> pos = (from po in stocks.PurchaseOrders
                                       where !po.IsDeleted
                                       select po).ToList();
            return pos;
        }

        public PurchaseOrder GetObjectById(int Id)
        {
            PurchaseOrder po = (from p in stocks.PurchaseOrders
                                where p.Id == Id && !p.IsDeleted
                                select p).FirstOrDefault();
            return po;
        }

        public PurchaseOrder CreateObject(PurchaseOrder purchaseOrder)
        {
            PurchaseOrder po = new PurchaseOrder();
            po.CustomerId = purchaseOrder.CustomerId;
            po.PurchaseDate = purchaseOrder.PurchaseDate;
            po.IsDeleted = false;
            po.IsConfirmed = false;
            po.CreatedAt = DateTime.Now;
            return Create(po);
        }

        public PurchaseOrder UpdateObject(PurchaseOrder purchaseOrder)
        {
            PurchaseOrder po = new PurchaseOrder();
            po.CustomerId = purchaseOrder.CustomerId;
            po.PurchaseDate = purchaseOrder.PurchaseDate;
            po.IsDeleted = purchaseOrder.IsDeleted;
            po.IsConfirmed = purchaseOrder.IsConfirmed;
            po.IsConfirmed = purchaseOrder.IsConfirmed;
            po.ModifiedAt = DateTime.Now;
            Update(po);
            return po;
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
            purchaseOrder.ModifiedAt = DateTime.Now;
            Update(purchaseOrder);
            return purchaseOrder;
        }

        public PurchaseOrder UnconfirmObject(PurchaseOrder purchaseOrder)
        {
            purchaseOrder.IsConfirmed = false;
            purchaseOrder.ModifiedAt = DateTime.Now;
            Update(purchaseOrder);
            return purchaseOrder;
        }
    }
}