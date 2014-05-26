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
    public interface IDeliveryOrderDetailService
    {
        IDeliveryOrderDetailValidator GetValidator();
        IList<DeliveryOrderDetail> GetObjectsByDeliveryOrderId(int deliveryOrderId);
        DeliveryOrderDetail GetObjectById(int Id);
        DeliveryOrderDetail GetObjectBySalesOrderDetailId(int salesOrderDetailId);
        DeliveryOrderDetail CreateObject(DeliveryOrderDetail deliveryOrderDetail,
            IDeliveryOrderService _dos, ISalesOrderDetailService _sods,
            ISalesOrderService _sos, IItemService _is, IContactService _cs);
        DeliveryOrderDetail CreateObject(int deliveryOrderId, int itemId, int quantity, int salesOrderDetailId,
            IDeliveryOrderService _dos, ISalesOrderDetailService _sods,
            ISalesOrderService _sos, IItemService _is, IContactService _cs);
        DeliveryOrderDetail UpdateObject(DeliveryOrderDetail deliveryOrderDetail,
            IDeliveryOrderService _dos, ISalesOrderDetailService _sods,
            ISalesOrderService _sos, IItemService _is, IContactService _cs);
        DeliveryOrderDetail SoftDeleteObject(DeliveryOrderDetail deliveryOrderDetail);
        bool DeleteObject(int Id);
        DeliveryOrderDetail ConfirmObject(DeliveryOrderDetail deliveryOrderDetail, IStockMutationService _stockMutationService, IItemService _itemService);
        DeliveryOrderDetail UnconfirmObject(DeliveryOrderDetail deliveryOrderDetail, IStockMutationService _stockMutationService, IItemService _itemService);
    }
}