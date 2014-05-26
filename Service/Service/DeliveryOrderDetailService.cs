using Core.DomainModel;
using Core.Interface.Repository;
using Core.Interface.Service;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class DeliveryOrderDetailService : IDeliveryOrderDetailService
    {
        private IDeliveryOrderDetailRepository _dod;
        public DeliveryOrderDetailService(IDeliveryOrderDetailRepository _deliveryOrderDetailRepository)
        {
            _dod = _deliveryOrderDetailRepository;
        }

        public IList<DeliveryOrderDetail> GetObjectsByDeliveryOrderId(int deliveryOrderId)
        {
            return _dod.GetObjectsByDeliveryOrderId(deliveryOrderId);
        }

        public DeliveryOrderDetail GetObjectById(int Id)
        {
            return _dod.GetObjectById(Id);
        }

        public DeliveryOrderDetail GetObjectBySalesOrderDetailId(int salesOrderDetailId)
        {
            return _dod.GetObjectBySalesOrderDetailId(salesOrderDetailId);
        }

        public DeliveryOrderDetail CreateObject(DeliveryOrderDetail deliveryOrderDetail)
        {
            return _dod.CreateObject(deliveryOrderDetail);
        }

        public DeliveryOrderDetail CreateObject(int deliveryOrderId, int itemId, int quantity, int salesOrderDetailId)
        {
            DeliveryOrderDetail dod = new DeliveryOrderDetail
            {
                DeliveryOrderId = deliveryOrderId,
                ItemId = itemId,
                Quantity = quantity
            };
            return _dod.CreateObject(dod);
        }

        public DeliveryOrderDetail UpdateObject(DeliveryOrderDetail deliveryOrderDetail)
        {
            return _dod.UpdateObject(deliveryOrderDetail);
        }

        public DeliveryOrderDetail SoftDeleteObject(DeliveryOrderDetail deliveryOrderDetail)
        {
            return _dod.SoftDeleteObject(deliveryOrderDetail);
        }

        public bool DeleteObject(int Id)
        {
            return _dod.DeleteObject(Id);
        }

        public DeliveryOrderDetail ConfirmObject(DeliveryOrderDetail deliveryOrderDetail, IStockMutationService _stockMutationService, IItemService _itemService)
        {
            deliveryOrderDetail = _dod.ConfirmObject(deliveryOrderDetail);
            Item item = _itemService.GetObjectById(deliveryOrderDetail.ItemId);
            IList<StockMutation> sm = _stockMutationService.CreateStockMutationForDeliveryOrder(deliveryOrderDetail, item);
            return deliveryOrderDetail;
        }

        public DeliveryOrderDetail UnconfirmObject(DeliveryOrderDetail deliveryOrderDetail, IStockMutationService _stockMutationService, IItemService _itemService)
        {
            deliveryOrderDetail = _dod.UnconfirmObject(deliveryOrderDetail);
            Item item = _itemService.GetObjectById(deliveryOrderDetail.ItemId);
            IList<StockMutation> sm = _stockMutationService.SoftDeleteStockMutationForDeliveryOrder(deliveryOrderDetail, item);
            return _dod.UnconfirmObject(deliveryOrderDetail);
        }
    }
}