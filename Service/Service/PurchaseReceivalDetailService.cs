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
    public class PurchaseReceivalDetailService : IPurchaseReceivalDetailService
    {
        private IPurchaseReceivalDetailRepository _pd;
        private IPurchaseReceivalDetailValidator _validator;

        public PurchaseReceivalDetailService(IPurchaseReceivalDetailRepository _purchaseReceivalDetailRepository, IPurchaseReceivalDetailValidator _purchaseReceivalDetailValidator)
        {
            _pd = _purchaseReceivalDetailRepository;
            _validator = _purchaseReceivalDetailValidator;
        }

        public IPurchaseReceivalDetailValidator GetValidator()
        {
            return _validator;
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

        public PurchaseReceivalDetail CreateObject(PurchaseReceivalDetail purchaseReceivalDetail, IPurchaseReceivalService _purchaseReceivalService,
                                                     IPurchaseOrderDetailService _purchaseOrderDetailService, IPurchaseOrderService _purchaseOrderService,
                                                     IItemService _itemService, IContactService _contactService)
        {
            purchaseReceivalDetail.Errors = new HashSet<string>();
            return (_validator.ValidCreateObject(purchaseReceivalDetail, this, _purchaseReceivalService,
                                        _purchaseOrderDetailService, _purchaseOrderService, _itemService, _contactService) ?
                                        _pd.CreateObject(purchaseReceivalDetail) : purchaseReceivalDetail);
        }

        public PurchaseReceivalDetail CreateObject(int purchaseReceivalId, int itemId, int quantity, int purchaseOrderDetailId,
                                                    IPurchaseReceivalService _purchaseReceivalService, IPurchaseOrderDetailService _purchaseOrderDetailService,
                                                    IPurchaseOrderService _purchaseOrderService, IItemService _itemService, IContactService _contactService)
        {
            PurchaseReceivalDetail prd = new PurchaseReceivalDetail
            {
                PurchaseReceivalId = purchaseReceivalId,
                ItemId = itemId,
                Quantity = quantity,
                PurchaseOrderDetailId = purchaseOrderDetailId
            };
            return this.CreateObject(prd, _purchaseReceivalService, _purchaseOrderDetailService, _purchaseOrderService, _itemService, _contactService);
        }


        public PurchaseReceivalDetail UpdateObject(PurchaseReceivalDetail purchaseReceivalDetail,
                                                    IPurchaseReceivalService _purchaseReceivalService, IPurchaseOrderDetailService _purchaseOrderDetailService,
                                                    IPurchaseOrderService _purchaseOrderService, IItemService _itemService, IContactService _contactService)
        {
            return (_validator.ValidUpdateObject(purchaseReceivalDetail, this, _purchaseReceivalService, _purchaseOrderDetailService, _purchaseOrderService, _itemService, _contactService) ?
                    _pd.UpdateObject(purchaseReceivalDetail) : purchaseReceivalDetail);
        }

        public PurchaseReceivalDetail SoftDeleteObject(PurchaseReceivalDetail purchaseReceivalDetail)
        {
            return (_validator.ValidDeleteObject(purchaseReceivalDetail) ? _pd.SoftDeleteObject(purchaseReceivalDetail) : purchaseReceivalDetail);
        }

        public bool DeleteObject(int Id)
        {
            return _pd.DeleteObject(Id);
        }

        public PurchaseReceivalDetail ConfirmObject(PurchaseReceivalDetail purchaseReceivalDetail, IStockMutationService _stockMutationService, IItemService _itemService)
        {
            if (_validator.ValidConfirmObject(purchaseReceivalDetail))
            {
                purchaseReceivalDetail = _pd.ConfirmObject(purchaseReceivalDetail);
                Item item = _itemService.GetObjectById(purchaseReceivalDetail.ItemId);
                item.PendingReceival -= purchaseReceivalDetail.Quantity;
                item.Ready += purchaseReceivalDetail.Quantity;
                _itemService.UpdateObject(item);
                IList<StockMutation> sm = _stockMutationService.CreateStockMutationForPurchaseReceival(purchaseReceivalDetail, item);
            }
            return purchaseReceivalDetail;
        }

        public PurchaseReceivalDetail UnconfirmObject(PurchaseReceivalDetail purchaseReceivalDetail, IStockMutationService _stockMutationService, IItemService _itemService)
        {
            if (_validator.ValidUnconfirmObject(purchaseReceivalDetail, this, _itemService))
            {
                purchaseReceivalDetail = _pd.UnconfirmObject(purchaseReceivalDetail);
                Item item = _itemService.GetObjectById(purchaseReceivalDetail.ItemId);
                item.PendingReceival += purchaseReceivalDetail.Quantity;
                item.Ready -= purchaseReceivalDetail.Quantity;
                _itemService.UpdateObject(item);
                IList<StockMutation> sm = _stockMutationService.SoftDeleteStockMutationForPurchaseReceival(purchaseReceivalDetail, item);
            }
            return purchaseReceivalDetail;
        }
    }
}