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
        public IList<PurchaseOrderDetail> GetObjectsByPurchaseOrderId(int purchaseOrderId, IPurchaseOrderDetailRepository _pd);
        public PurchaseOrderDetail GetObjectById(int Id, IPurchaseOrderDetailRepository _pd);
        public PurchaseOrderDetail CreateObject(PurchaseOrderDetail purchaseOrderDetail, IPurchaseOrderDetailRepository _pd);
        public PurchaseOrderDetail UpdateObject(PurchaseOrderDetail purchaseOrderDetail, IPurchaseOrderDetailRepository _pd);
        public PurchaseOrderDetail SoftDeleteObject(PurchaseOrderDetail purchaseOrderDetail, IPurchaseOrderDetailRepository _pd);
        public bool DeleteObject(int Id, IPurchaseOrderDetailRepository _pd);
        public PurchaseOrderDetail ConfirmObject(PurchaseOrderDetail purchaseOrderDetail, IPurchaseOrderDetailRepository _pd);
        public PurchaseOrderDetail UnconfirmObject(PurchaseOrderDetail purchaseOrderDetail, IPurchaseOrderDetailRepository _pd);
    }
}