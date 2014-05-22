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

        public PurchaseReceivalDetail ConfirmObject(PurchaseReceivalDetail purchaseReceivalDetail)
        {
            return _pd.ConfirmObject(purchaseReceivalDetail);
        }

        public PurchaseReceivalDetail UnconfirmObject(PurchaseReceivalDetail purchaseReceivalDetail)
        {
            return _pd.UnconfirmObject(purchaseReceivalDetail);
        }

        public PurchaseReceivalDetail FulfilObject(PurchaseReceivalDetail purchaseReceivalDetail)
        {
            return _pd.FulfilObject(purchaseReceivalDetail);
        }
    }
}