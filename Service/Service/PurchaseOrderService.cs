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
        public IList<PurchaseOrder> GetAll(IPurchaseOrderRepository _p)
        {
            return _p.GetAll();
        }

        public PurchaseOrder GetObjectById(int Id, IPurchaseOrderRepository _p)
        {
            return _p.GetObjectById(Id);
        }

        public PurchaseOrder CreateObject(PurchaseOrder purchaseOrder, IPurchaseOrderRepository _p)
        {
            return _p.CreateObject(purchaseOrder);
        }

        public PurchaseOrder UpdateObject(PurchaseOrder purchaseOrder, IPurchaseOrderRepository _p)
        {
            return _p.UpdateObject(purchaseOrder);
        }

        public PurchaseOrder SoftDeleteObject(PurchaseOrder purchaseOrder, IPurchaseOrderRepository _p)
        {
            return _p.SoftDeleteObject(purchaseOrder);
        }

        public bool DeleteObject(int Id, IPurchaseOrderRepository _p)
        {
            return _p.DeleteObject(Id);
        }

        public PurchaseOrder ConfirmObject(PurchaseOrder purchaseOrder, IPurchaseOrderRepository _p)
        {
            return _p.ConfirmObject(purchaseOrder);
        }

        public PurchaseOrder UnconfirmObject(PurchaseOrder purchaseOrder, IPurchaseOrderRepository _p)
        {
            return _p.UnconfirmObject(purchaseOrder);
        }
    }
}