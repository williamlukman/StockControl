using Core.DomainModel;
using Core.Interface.Repository;
using Core.Interface.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Service
{
    public interface IDeliveryOrderService
    {
        IDeliveryOrderValidator GetValidator();
        IList<DeliveryOrder> GetAll();
        DeliveryOrder GetObjectById(int Id);
        IList<DeliveryOrder> GetObjectsByContactId(int contactId);
        DeliveryOrder CreateObject(DeliveryOrder deliveryOrder, IContactService _cs);
        DeliveryOrder CreateObject(int contactId, DateTime deliveryDate, IContactService _cs);
        DeliveryOrder UpdateObject(DeliveryOrder deliveryOrder, IContactService _cs);
        DeliveryOrder SoftDeleteObject(DeliveryOrder deliveryOrder, IDeliveryOrderDetailService _dods);
        bool DeleteObject(int Id);
        DeliveryOrder ConfirmObject(DeliveryOrder deliveryOrder, IDeliveryOrderDetailService _dods,
                                    ISalesOrderDetailService _sods, IStockMutationService _stockMutationService, IItemService _itemService);
        DeliveryOrder UnconfirmObject(DeliveryOrder deliveryOrder, IDeliveryOrderDetailService _dods,
                                    IStockMutationService _stockMutationService, IItemService _itemService);
    }
}