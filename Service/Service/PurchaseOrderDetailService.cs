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
    public class PurchaseOrderDetailService : IPurchaseOrderDetailService
    {
        private IPurchaseOrderDetailRepository _pd;
        public PurchaseOrderDetailService(IPurchaseOrderDetailRepository _purchaseOrderDetailRepository)
        {
            _pd = _purchaseOrderDetailRepository;
        }

        public IList<PurchaseOrderDetail> GetObjectsByPurchaseOrderId(int purchaseOrderId)
        {
            return _pd.GetObjectsByPurchaseOrderId(purchaseOrderId);
        }

        public PurchaseOrderDetail GetObjectById(int Id)
        {
            return _pd.GetObjectById(Id);
        }

        public PurchaseOrderDetail CreateObject(PurchaseOrderDetail purchaseOrderDetail)
        {
            return _pd.CreateObject(purchaseOrderDetail);
        }

        public PurchaseOrderDetail CreateObject(int purchaseOrderId, int itemId, int quantity)
        {
            PurchaseOrderDetail pod = new PurchaseOrderDetail
            {
                PurchaseOrderId = purchaseOrderId,
                ItemId = itemId,
                Quantity = quantity,
            };
            return _pd.CreateObject(pod);
        }

        public PurchaseOrderDetail UpdateObject(PurchaseOrderDetail purchaseOrderDetail)
        {
            return _pd.UpdateObject(purchaseOrderDetail);
        }

        public PurchaseOrderDetail SoftDeleteObject(PurchaseOrderDetail purchaseOrderDetail)
        {
            return _pd.SoftDeleteObject(purchaseOrderDetail);
        }

        public bool DeleteObject(int Id)
        {
            return _pd.DeleteObject(Id);
        }

        public PurchaseOrderDetail ConfirmObject(PurchaseOrderDetail purchaseOrderDetail, IStockMutationService _stockMutationService, IItemService _itemService)
        {
            purchaseOrderDetail = _pd.ConfirmObject(purchaseOrderDetail);
            Item item = _itemService.GetObjectById(purchaseOrderDetail.ItemId);
            item.PendingReceival += purchaseOrderDetail.Quantity;
            _itemService.UpdateObject(item);
            StockMutation sm = _stockMutationService.CreateStockMutationForPurchaseOrder(purchaseOrderDetail, item);
            return purchaseOrderDetail;
        }

        public PurchaseOrderDetail UnconfirmObject(PurchaseOrderDetail purchaseOrderDetail, IStockMutationService _stockMutationService, IItemService _itemService)
        {
            purchaseOrderDetail = _pd.UnconfirmObject(purchaseOrderDetail);
            Item item = _itemService.GetObjectById(purchaseOrderDetail.ItemId);
            item.PendingReceival -= purchaseOrderDetail.Quantity;
            _itemService.UpdateObject(item);
            IList<StockMutation> sm = _stockMutationService.SoftDeleteStockMutationForPurchaseOrder(purchaseOrderDetail, item);
            return _pd.UnconfirmObject(purchaseOrderDetail);
        }

        public PurchaseOrderDetail FulfilObject(PurchaseOrderDetail purchaseOrderDetail)
        {
            return _pd.FulfilObject(purchaseOrderDetail);
        }

    }
}