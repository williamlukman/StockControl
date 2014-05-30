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
    public class PurchaseOrderDetailService : IPurchaseOrderDetailService
    {
        private IPurchaseOrderDetailRepository _pd;
        private IPurchaseOrderDetailValidator _validator;

        public PurchaseOrderDetailService(IPurchaseOrderDetailRepository _purchaseOrderDetailRepository, IPurchaseOrderDetailValidator _purchaseOrderDetailValidator)
        {
            _pd = _purchaseOrderDetailRepository;
            _validator = _purchaseOrderDetailValidator;
        }

        public IPurchaseOrderDetailValidator GetValidator()
        {
            return _validator;
        }

        public IList<PurchaseOrderDetail> GetObjectsByPurchaseOrderId(int purchaseOrderId)
        {
            return _pd.GetObjectsByPurchaseOrderId(purchaseOrderId);
        }

        public PurchaseOrderDetail GetObjectById(int Id)
        {
            return _pd.GetObjectById(Id);
        }

        public PurchaseOrderDetail CreateObject(PurchaseOrderDetail purchaseOrderDetail, IPurchaseOrderService _purchaseOrderService, IItemService _itemService)
        {
            purchaseOrderDetail.Errors = new HashSet<string>();
            return (_validator.ValidCreateObject(purchaseOrderDetail, this, _purchaseOrderService, _itemService) ? _pd.CreateObject(purchaseOrderDetail) : purchaseOrderDetail);
        }

        public PurchaseOrderDetail CreateObject(int purchaseOrderId, int itemId, int quantity, IPurchaseOrderService _purchaseOrderService, IItemService _itemService)
        {
            PurchaseOrderDetail pod = new PurchaseOrderDetail
            {
                PurchaseOrderId = purchaseOrderId,
                ItemId = itemId,
                Quantity = quantity,
            };
            return this.CreateObject(pod, _purchaseOrderService, _itemService);
        }

        public PurchaseOrderDetail UpdateObject(PurchaseOrderDetail purchaseOrderDetail, IPurchaseOrderService _purchaseOrderService, IItemService _itemService)
        {
            return (_validator.ValidUpdateObject(purchaseOrderDetail, this, _purchaseOrderService, _itemService) ?
                     _pd.UpdateObject(purchaseOrderDetail) : purchaseOrderDetail);
        }

        public PurchaseOrderDetail SoftDeleteObject(PurchaseOrderDetail purchaseOrderDetail)
        {
            return (_validator.ValidDeleteObject(purchaseOrderDetail) ? _pd.SoftDeleteObject(purchaseOrderDetail) : purchaseOrderDetail);
        }

        public bool DeleteObject(int Id)
        {
            return _pd.DeleteObject(Id);
        }

        public PurchaseOrderDetail ConfirmObject(PurchaseOrderDetail purchaseOrderDetail, IStockMutationService _stockMutationService, IItemService _itemService)
        {
            if (_validator.ValidConfirmObject(purchaseOrderDetail))
            {
                purchaseOrderDetail = _pd.ConfirmObject(purchaseOrderDetail);
                Item item = _itemService.GetObjectById(purchaseOrderDetail.ItemId);
                item.PendingReceival += purchaseOrderDetail.Quantity;
                _itemService.UpdateObject(item);
                StockMutation sm = _stockMutationService.CreateStockMutationForPurchaseOrder(purchaseOrderDetail, item);
            }
            return purchaseOrderDetail;
        }

        public PurchaseOrderDetail UnconfirmObject(PurchaseOrderDetail purchaseOrderDetail, IPurchaseReceivalDetailService _purchaseReceivalDetailService, IStockMutationService _stockMutationService, IItemService _itemService)
        {
            if (_validator.ValidUnconfirmObject(purchaseOrderDetail, this, _purchaseReceivalDetailService, _itemService))
            {
                purchaseOrderDetail = _pd.UnconfirmObject(purchaseOrderDetail);
                Item item = _itemService.GetObjectById(purchaseOrderDetail.ItemId);
                item.PendingReceival -= purchaseOrderDetail.Quantity;
                _itemService.UpdateObject(item);
                IList<StockMutation> sm = _stockMutationService.SoftDeleteStockMutationForPurchaseOrder(purchaseOrderDetail, item);
            }
            return purchaseOrderDetail;
        }

        public PurchaseOrderDetail FulfilObject(PurchaseOrderDetail purchaseOrderDetail)
        {
            return _pd.FulfilObject(purchaseOrderDetail);
        }

    }
}