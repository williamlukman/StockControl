using Core.DomainModel;
using Core.Interface.Repository;
using Core.Interface.Service;
using Core.Interface.Validation;
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
        private IDeliveryOrderDetailValidator _validator;

        public DeliveryOrderDetailService(IDeliveryOrderDetailRepository _deliveryOrderDetailRepository, IDeliveryOrderDetailValidator _deliveryOrderDetailValidator)
        {
            _dod = _deliveryOrderDetailRepository;
            _validator = _deliveryOrderDetailValidator;
        }

        public IDeliveryOrderDetailValidator GetValidator()
        {
            return _validator;
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

        public DeliveryOrderDetail CreateObject(DeliveryOrderDetail deliveryOrderDetail, 
            IDeliveryOrderService _dos, ISalesOrderDetailService _sods,
            ISalesOrderService _sos, IItemService _is, IContactService _cs)
        {
            deliveryOrderDetail.Errors = new HashSet<string>();
            return (_validator.ValidCreateObject(deliveryOrderDetail, this, _dos, _sods, _sos, _is, _cs) ? _dod.CreateObject(deliveryOrderDetail) : deliveryOrderDetail);
        }

        public DeliveryOrderDetail CreateObject(int deliveryOrderId, int itemId, int quantity, int salesOrderDetailId,
            IDeliveryOrderService _dos, ISalesOrderDetailService _sods,
            ISalesOrderService _sos, IItemService _is, IContactService _cs)
        {
            DeliveryOrderDetail dod = new DeliveryOrderDetail
            {
                DeliveryOrderId = deliveryOrderId,
                ItemId = itemId,
                Quantity = quantity,
                SalesOrderDetailId = salesOrderDetailId
            };
            return this.CreateObject(dod, _dos, _sods, _sos, _is, _cs);
        }

        public DeliveryOrderDetail UpdateObject(DeliveryOrderDetail deliveryOrderDetail,
            IDeliveryOrderService _dos, ISalesOrderDetailService _sods,
            ISalesOrderService _sos, IItemService _is, IContactService _cs)
        {
            return (_validator.ValidUpdateObject(deliveryOrderDetail, this, _dos, _sods, _sos, _is, _cs) ?
                    _dod.UpdateObject(deliveryOrderDetail) : deliveryOrderDetail);
        }

        public DeliveryOrderDetail SoftDeleteObject(DeliveryOrderDetail deliveryOrderDetail)
        {
            return (_validator.ValidDeleteObject(deliveryOrderDetail) ? _dod.SoftDeleteObject(deliveryOrderDetail) : deliveryOrderDetail);
        }

        public bool DeleteObject(int Id)
        {
            return _dod.DeleteObject(Id);
        }

        public DeliveryOrderDetail ConfirmObject(DeliveryOrderDetail deliveryOrderDetail, IStockMutationService _stockMutationService, IItemService _itemService)
        {
            if (_validator.ValidConfirmObject(deliveryOrderDetail))
            {
                deliveryOrderDetail = _dod.ConfirmObject(deliveryOrderDetail);
                Item item = _itemService.GetObjectById(deliveryOrderDetail.ItemId);
                item.PendingDelivery -= deliveryOrderDetail.Quantity;
                item.Ready -= deliveryOrderDetail.Quantity;
                _itemService.UpdateObject(item);
                IList<StockMutation> sm = _stockMutationService.CreateStockMutationForDeliveryOrder(deliveryOrderDetail, item);
            }
            return deliveryOrderDetail;
        }

        public DeliveryOrderDetail UnconfirmObject(DeliveryOrderDetail deliveryOrderDetail, IStockMutationService _stockMutationService, IItemService _itemService)
        {
            if (_validator.ValidUnconfirmObject(deliveryOrderDetail, this, _itemService))
            {
                deliveryOrderDetail = _dod.UnconfirmObject(deliveryOrderDetail);
                Item item = _itemService.GetObjectById(deliveryOrderDetail.ItemId);
                item.PendingDelivery += deliveryOrderDetail.Quantity;
                item.Ready += deliveryOrderDetail.Quantity;
                _itemService.UpdateObject(item);
                IList<StockMutation> sm = _stockMutationService.SoftDeleteStockMutationForDeliveryOrder(deliveryOrderDetail, item);
            }
            return deliveryOrderDetail;
        }
    }
}