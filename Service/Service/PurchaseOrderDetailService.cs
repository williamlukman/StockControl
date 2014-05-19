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
        public IList<PurchaseOrderDetail> GetObjectsByPurchaseOrderId(int purchaseOrderId, IPurchaseOrderDetailRepository _pd)
        {
            return _p.GetObjectsByPurchaseOrderId(purchaseOrderId);
        }

        public PurchaseOrderDetail GetObjectById(int Id, IPurchaseOrderDetailRepository _pd)
        {
            return _p.GetObjectById(Id);
        }

        public PurchaseOrderDetail CreateObject(PurchaseOrderDetail purchaseOrderDetail, IPurchaseOrderDetailRepository _pd)
        {
            return _p.CreateObject(purchaseOrderDetail);
        }

        public PurchaseOrderDetail UpdateObject(PurchaseOrderDetail purchaseOrderDetail, IPurchaseOrderDetailRepository _pd)
        {
            return _p.UpdateObject(purchaseOrderDetail);
        }

        public PurchaseOrderDetail SoftDeleteObject(PurchaseOrderDetail purchaseOrderDetail, IPurchaseOrderDetailRepository _pd)
        {
            return _p.SoftDeleteObject(purchaseOrderDetail);
        }

        public bool DeleteObject(int Id, IPurchaseOrderDetailRepository _pd)
        {
            return _p.DeleteObject(Id);
        }

        public PurchaseOrderDetail ConfirmObject(PurchaseOrderDetail purchaseOrderDetail, IPurchaseOrderDetailRepository _pd)
        {
            return _p.ConfirmObject(purchaseOrderDetail);
        }

        public PurchaseOrderDetail UnconfirmObject(PurchaseOrderDetail purchaseOrderDetail, IPurchaseOrderDetailRepository _pd)
        {
            return _p.UnconfirmObject(purchaseOrderDetail);
        }
    }
}