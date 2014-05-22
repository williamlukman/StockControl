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

        public PurchaseOrderDetail ConfirmObject(PurchaseOrderDetail purchaseOrderDetail)
        {
            return _pd.ConfirmObject(purchaseOrderDetail);
        }

        public PurchaseOrderDetail UnconfirmObject(PurchaseOrderDetail purchaseOrderDetail)
        {
            return _pd.UnconfirmObject(purchaseOrderDetail);
        }

        public PurchaseOrderDetail FulfilObject(PurchaseOrderDetail purchaseOrderDetail)
        {
            return _pd.FulfilObject(purchaseOrderDetail);
        }

    }
}