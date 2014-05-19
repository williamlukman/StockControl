using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Service
{
    public interface IPurchaseOrderDetailService
    {
        public IList<PurchaseOrderDetail> GetObjectsByPurchaseOrderId(int purchaseOrderId);
        public PurchaseOrderDetail GetObjectById(int Id);
        public PurchaseOrderDetail CreateObject(PurchaseOrderDetail purchaseOrderDetail);
        public PurchaseOrderDetail UpdateObject(PurchaseOrderDetail purchaseOrderDetail);
        public PurchaseOrderDetail SoftDeleteObject(PurchaseOrderDetail purchaseOrderDetail);
        public bool DeleteObject(int Id);
        public PurchaseOrderDetail ConfirmObject(PurchaseOrderDetail purchaseOrderDetail);
        public PurchaseOrderDetail UnconfirmObject(PurchaseOrderDetail purchaseOrderDetail);
    }
}