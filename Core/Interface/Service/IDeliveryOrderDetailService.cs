using Core.DomainModel;
using Core.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Service
{
    public interface IDeliveryOrderDetailService
    {
        IList<DeliveryOrderDetail> GetObjectsByDeliveryOrderId(int deliveryOrderId);
        DeliveryOrderDetail GetObjectById(int Id);
        DeliveryOrderDetail GetObjectBySalesOrderDetailId(int salesOrderDetailId);
        DeliveryOrderDetail CreateObject(DeliveryOrderDetail deliveryOrderDetail);
        DeliveryOrderDetail CreateObject(int deliveryOrderId, int itemId, int quantity, int salesOrderDetailId);
        DeliveryOrderDetail UpdateObject(DeliveryOrderDetail deliveryOrderDetail);
        DeliveryOrderDetail SoftDeleteObject(DeliveryOrderDetail deliveryOrderDetail);
        bool DeleteObject(int Id);
        DeliveryOrderDetail ConfirmObject(DeliveryOrderDetail deliveryOrderDetail, IStockMutationService _stockMutationService, IItemService _itemService);
        DeliveryOrderDetail UnconfirmObject(DeliveryOrderDetail deliveryOrderDetail, IStockMutationService _stockMutationService, IItemService _itemService);
    }
}