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
            List<PurchaseOrder> pos = (from po in stocks.purchaseOrders
                                       where !po.IsDeleted
                                       select po).ToList();
            return pos;
        }

        PurchaseOrder GetObjectById(int Id)
        {
            PurchaseOrder po = (from p in stocks.purchaseOrders
                                where p.Id == Id && !p.IsDeleted
                                select p).FirstOrDefault();
            return po;
        }

        PurchaseOrder CreateObject(PurchaseOrder purchaseOrder)
        {
            PurchaseOrder po = new PurchaseOrder();
            po.CustomerId = purchaseOrder.CustomerId;
            po.PurchaseDate = purchaseOrder.PurchaseDate;
            po.IsDeleted = false;
            po.IsConfirmed = false;
            po.CreatedAt = DateTime.Now;
            return Create(po);
        }

        PurchaseOrder UpdateObject(PurchaseOrder purchaseOrder);
        PurchaseOrder SoftDeleteObject(PurchaseOrder purchaseOrder);
        bool DeleteObject(int Id);
        bool ConfirmObject(PurchaseOrder purchaseOrder);
        bool UnconfirmObject(PurchaseOrder purchaseOrder);

    }
}