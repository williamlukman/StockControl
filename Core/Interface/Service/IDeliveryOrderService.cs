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
        IList<DeliveryOrder> GetObjectsByContactId(int contactId);
        DeliveryOrder CreateObject(DeliveryOrder deliveryOrder);
        DeliveryOrder CreateObject(int contactId, DateTime deliveryDate);
        DeliveryOrder UpdateObject(DeliveryOrder deliveryOrder);
        DeliveryOrder SoftDeleteObject(DeliveryOrder deliveryOrder);
        bool DeleteObject(int Id);
        DeliveryOrder ConfirmObject(DeliveryOrder deliveryOrder, IDeliveryOrderDetailService _dods,
                                    IStockMutationService _stockMutationService, IItemService _itemService);
        DeliveryOrder UnconfirmObject(DeliveryOrder deliveryOrder, IDeliveryOrderDetailService _dods,
                                    IStockMutationService _stockMutationService, IItemService _itemService);
    }
}