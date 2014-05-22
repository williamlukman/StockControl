using Core.DomainModel;
using Core.Interface.Repository;
using Core.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private IPurchaseOrderRepository _p;
        public PurchaseOrderService(IPurchaseOrderRepository _purchaseOrderRepository)
        {
            _p = _purchaseOrderRepository;
        }

        public IList<PurchaseOrder> GetAll()
        {
            return _p.GetAll();
        }

        public PurchaseOrder GetObjectById(int Id)
        {
            return _p.GetObjectById(Id);
        }

        public PurchaseOrder CreateObject(PurchaseOrder purchaseOrder)
        {
            return _p.CreateObject(purchaseOrder);
        }

        public PurchaseOrder CreateObject(int contactId, DateTime purchaseDate)
        {
            PurchaseOrder po = new PurchaseOrder
            {
                CustomerId = contactId,
                PurchaseDate = purchaseDate
            };
            return _p.CreateObject(po);
        }

        public PurchaseOrder UpdateObject(PurchaseOrder purchaseOrder)
        {
            return _p.UpdateObject(purchaseOrder);
        }

        public PurchaseOrder SoftDeleteObject(PurchaseOrder purchaseOrder)
        {
            return _p.SoftDeleteObject(purchaseOrder);
        }

        public bool DeleteObject(int Id)
        {
            return _p.DeleteObject(Id);
        }

        public PurchaseOrder ConfirmObject(PurchaseOrder purchaseOrder)
        {
            return _p.ConfirmObject(purchaseOrder);
        }

        public PurchaseOrder UnconfirmObject(PurchaseOrder purchaseOrder)
        {
            return _p.UnconfirmObject(purchaseOrder);
        }
    }
}