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
    public class PurchaseReceivalDetailService : IPurchaseReceivalDetailService
    {
        private IPurchaseReceivalDetailRepository _pd;
        public PurchaseReceivalDetailService(IPurchaseReceivalDetailRepository _purchaseReceivalDetailRepository)
        {
            _pd = _purchaseReceivalDetailRepository;
        }

        public IList<PurchaseReceivalDetail> GetObjectsByPurchaseReceivalId(int purchaseReceivalId)
        {
            return _pd.GetObjectsByPurchaseReceivalId(purchaseReceivalId);
        }

        public PurchaseReceivalDetail GetObjectById(int Id)
        {
            return _pd.GetObjectById(Id);
        }

        public PurchaseReceivalDetail GetObjectByPurchaseOrderDetailId(int purchaseOrderDetailId)
        {
            return _pd.GetObjectByPurchaseOrderDetailId(purchaseOrderDetailId);
        }

        public PurchaseReceivalDetail CreateObject(PurchaseReceivalDetail purchaseReceivalDetail)
        {
            return _pd.CreateObject(purchaseReceivalDetail);
        }

        public PurchaseReceivalDetail CreateObject(int purchaseReceivalId, int itemId, int quantity, int purchaseOrderDetailId)
        {
            PurchaseReceivalDetail prd = new PurchaseReceivalDetail
            {
                PurchaseReceivalId = purchaseReceivalId,
                ItemId = itemId,
                Quantity = quantity,
                PurchaseOrderDetailId = purchaseOrderDetailId
            };
            return _pd.CreateObject(prd);
        }


        public PurchaseReceivalDetail UpdateObject(PurchaseReceivalDetail purchaseReceivalDetail)
        {
            return _pd.UpdateObject(purchaseReceivalDetail);
        }

        public PurchaseReceivalDetail SoftDeleteObject(PurchaseReceivalDetail purchaseReceivalDetail)
        {
            return _pd.SoftDeleteObject(purchaseReceivalDetail);
        }

        public bool DeleteObject(int Id)
        {
            return _pd.DeleteObject(Id);
        }

        public PurchaseReceivalDetail ConfirmObject(PurchaseReceivalDetail purchaseReceivalDetail, IStockMutationService _stockMutationService, IItemService _itemService)
        {
            purchaseReceivalDetail = _pd.ConfirmObject(purchaseReceivalDetail);
            Item item = _itemService.GetObjectById(purchaseReceivalDetail.ItemId);
            IList<StockMutation> sm = _stockMutationService.CreateStockMutationForPurchaseReceival(purchaseReceivalDetail, item);
            return purchaseReceivalDetail;
        }

        public PurchaseReceivalDetail UnconfirmObject(PurchaseReceivalDetail purchaseReceivalDetail, IStockMutationService _stockMutationService, IItemService _itemService)
        {
            purchaseReceivalDetail = _pd.UnconfirmObject(purchaseReceivalDetail);
            Item item = _itemService.GetObjectById(purchaseReceivalDetail.ItemId);
            IList<StockMutation> sm = _stockMutationService.SoftDeleteStockMutationForPurchaseReceival(purchaseReceivalDetail, item);
            return _pd.UnconfirmObject(purchaseReceivalDetail);
        }


        public PurchaseReceivalDetail FulfilObject(PurchaseReceivalDetail purchaseReceivalDetail, bool isFulfilled)
        {
            return _pd.FulfilObject(purchaseReceivalDetail, isFulfilled);
        }
    }
}