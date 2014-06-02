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
    public class DeliveryOrderRepository : EfRepository<DeliveryOrder>, IDeliveryOrderRepository
    {
        private StockControlEntities stocks;
        public DeliveryOrderRepository()
        {
            stocks = new StockControlEntities();
        }

        public IList<DeliveryOrder> GetAll()
        {
            return FindAll(d => !d.IsDeleted).ToList();
        }

        public DeliveryOrder GetObjectById(int Id)
        {
            return Find(d => d.Id == Id && !d.IsDeleted);
        }

        public IList<DeliveryOrder> GetObjectsByContactId(int contactId)
        {
            return FindAll(d => d.ContactId == contactId && !d.IsDeleted).ToList();
        }

        public DeliveryOrder CreateObject(DeliveryOrder deliveryOrder)
        {
            deliveryOrder.IsDeleted = false;
            deliveryOrder.IsConfirmed = false;
            deliveryOrder.CreatedAt = DateTime.Now;
            return Create(deliveryOrder);
        }

        public DeliveryOrder UpdateObject(DeliveryOrder deliveryOrder)
        {
            deliveryOrder.ModifiedAt = DateTime.Now;
            Update(deliveryOrder);
            return deliveryOrder;
        }

        public DeliveryOrder SoftDeleteObject(DeliveryOrder deliveryOrder)
        {
            deliveryOrder.IsDeleted = true;
            deliveryOrder.DeletedAt = DateTime.Now;
            Update(deliveryOrder);
            return deliveryOrder;
        }

        public bool DeleteObject(int Id)
        {
            DeliveryOrder d = Find(x => x.Id == Id);
            return (Delete(d) == 1) ? true : false;
        }

        public DeliveryOrder ConfirmObject(DeliveryOrder deliveryOrder)
        {
            deliveryOrder.IsConfirmed = true;
            deliveryOrder.ConfirmedAt = DateTime.Now;
            Update(deliveryOrder);
            return deliveryOrder;
        }

        public DeliveryOrder UnconfirmObject(DeliveryOrder deliveryOrder)
        {
            deliveryOrder.IsConfirmed = false;
            Update(deliveryOrder);
            return deliveryOrder;
        }
    }
}