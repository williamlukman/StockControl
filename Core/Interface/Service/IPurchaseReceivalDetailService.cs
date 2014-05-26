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
    public interface IPurchaseReceivalDetailService
    {
        IPurchaseReceivalDetailValidator GetValidator();
        IList<PurchaseReceivalDetail> GetObjectsByPurchaseReceivalId(int purchaseReceivalId);
        PurchaseReceivalDetail GetObjectById(int Id);
        PurchaseReceivalDetail GetObjectByPurchaseOrderDetailId(int purchaseOrderDetailId);
        PurchaseReceivalDetail CreateObject(PurchaseReceivalDetail purchaseReceivalDetail,
            IPurchaseReceivalService _purchaseReceivalService, IPurchaseOrderDetailService _purchaseOrderDetailService,
            IPurchaseOrderService _purchaseOrderService, IItemService _itemService, IContactService _contactService);
        PurchaseReceivalDetail CreateObject(int purchaseReceivalId, int itemId, int quantity, int purchaseOrderDetailId,
            IPurchaseReceivalService _purchaseReceivalService, IPurchaseOrderDetailService _purchaseOrderDetailService,
            IPurchaseOrderService _purchaseOrderService, IItemService _itemService, IContactService _contactService);
        PurchaseReceivalDetail UpdateObject(PurchaseReceivalDetail purchaseReceivalDetail,
            IPurchaseReceivalService _purchaseReceivalService, IPurchaseOrderDetailService _purchaseOrderDetailService,
            IPurchaseOrderService _purchaseOrderService, IItemService _itemService, IContactService _contactService);
        PurchaseReceivalDetail SoftDeleteObject(PurchaseReceivalDetail purchaseReceivalDetail);
        bool DeleteObject(int Id);
        PurchaseReceivalDetail ConfirmObject(PurchaseReceivalDetail purchaseReceivalDetail, IStockMutationService _stockMutationService, IItemService _itemService);
        PurchaseReceivalDetail UnconfirmObject(PurchaseReceivalDetail purchaseReceivalDetail, IStockMutationService _stockMutationService, IItemService _itemService);
    }
}