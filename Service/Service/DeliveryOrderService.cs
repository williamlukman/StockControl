using Core.DomainModel;
using Core.Interface.Repository;
using Core.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class DeliveryOrderService : IDeliveryOrderService
    {
        private IDeliveryOrderRepository _do;
        public DeliveryOrderService(IDeliveryOrderRepository _deliveryOrderRepository)
        {
            _do = _deliveryOrderRepository;
        }

        public IList<DeliveryOrder> GetAll()
        {
            return _do.GetAll();
        }

        public DeliveryOrder GetObjectById(int Id)
        {
            return _do.GetObjectById(Id);
        }

        public DeliveryOrder CreateObject(DeliveryOrder deliveryOrder)
        {
            return _do.CreateObject(deliveryOrder);
        }

        public DeliveryOrder CreateObject(int contactId, DateTime deliveryDate)
        {
            DeliveryOrder deliveryOrder = new DeliveryOrder
            {
                CustomerId = contactId,
                DeliveryDate = deliveryDate
            };
            return _do.CreateObject(deliveryOrder);
        }

        public DeliveryOrder UpdateObject(DeliveryOrder deliveryOrder)
        {
            return _do.UpdateObject(deliveryOrder);
        }

        public DeliveryOrder SoftDeleteObject(DeliveryOrder deliveryOrder)
        {
            return _do.SoftDeleteObject(deliveryOrder);
        }

        public bool DeleteObject(int Id)
        {
            return _do.DeleteObject(Id);
        }

        public DeliveryOrder ConfirmObject(DeliveryOrder deliveryOrder)
        {
            return _do.ConfirmObject(deliveryOrder);
        }

        public DeliveryOrder UnconfirmObject(DeliveryOrder deliveryOrder)
        {
            return _do.UnconfirmObject(deliveryOrder);
        }
    }
}