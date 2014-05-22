using Core.DomainModel;
using Core.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Service
{
    public interface IDeliveryOrderService
    {
        IList<DeliveryOrder> GetAll();
        DeliveryOrder GetObjectById(int Id);
        DeliveryOrder CreateObject(DeliveryOrder deliveryOrder);
        DeliveryOrder UpdateObject(DeliveryOrder deliveryOrder);
        DeliveryOrder SoftDeleteObject(DeliveryOrder deliveryOrder);
        bool DeleteObject(int Id);
        DeliveryOrder ConfirmObject(DeliveryOrder deliveryOrder);
        DeliveryOrder UnconfirmObject(DeliveryOrder deliveryOrder);
    }
}