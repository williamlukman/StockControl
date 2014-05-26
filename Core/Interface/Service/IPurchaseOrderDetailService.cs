using Core.DomainModel;
using Core.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Service
{
    public interface IPurchaseOrderDetailService
    {
        IList<PurchaseOrderDetail> GetObjectsByPurchaseOrderId(int purchaseOrderId);
        PurchaseOrderDetail GetObjectById(int Id);
        PurchaseOrderDetail CreateObject(PurchaseOrderDetail purchaseOrderDetail);
        PurchaseOrderDetail CreateObject(int purchaseOrderId, int itemId, int quantity);

        PurchaseOrderDetail UpdateObject(PurchaseOrderDetail purchaseOrderDetail);
        PurchaseOrderDetail SoftDeleteObject(PurchaseOrderDetail purchaseOrderDetail);
        bool DeleteObject(int Id);
        PurchaseOrderDetail ConfirmObject(PurchaseOrderDetail purchaseOrderDetail, IStockMutationService _stockMutationService, IItemService _itemService);
        PurchaseOrderDetail UnconfirmObject(PurchaseOrderDetail purchaseOrderDetail, IStockMutationService _stockMutationService, IItemService _itemService);
        PurchaseOrderDetail FulfilObject(PurchaseOrderDetail purchaseOrderDetail);
    }
}