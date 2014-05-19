using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Repository
{
    interface IPurchaseOrderDetailRepository
    {
        IList<PurchaseOrderDetail> GetObjectsByPurchaseOrderId(int purchaseOrderId);
        PurchaseOrderDetail GetObjectById(int Id);
        PurchaseOrderDetail CreateObject(PurchaseOrderDetail purchaseOrderDetail);
        PurchaseOrderDetail UpdateObject(PurchaseOrderDetail purchaseOrderDetail);
        PurchaseOrderDetail SoftDeleteObject(PurchaseOrderDetail purchaseOrderDetail);
        bool DeleteObject(int Id);
        bool ConfirmObject (PurchaseOrderDetail purchaseOrderDetail);
        bool UnconfirmObject(PurchaseOrderDetail purchaseOrderDetail);

    }
}