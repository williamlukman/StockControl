using Core.DomainModel;
using Core.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Service
{
    public interface IPurchaseOrderService
    {
        public IList<PurchaseOrder> GetAll(IPurchaseOrderRepository _p);
        public PurchaseOrder GetObjectById(int Id, IPurchaseOrderRepository _p);
        public PurchaseOrder CreateObject(PurchaseOrder purchaseOrder, IPurchaseOrderRepository _p);
        public PurchaseOrder UpdateObject(PurchaseOrder purchaseOrder, IPurchaseOrderRepository _p);
        public PurchaseOrder SoftDeleteObject(PurchaseOrder purchaseOrder, IPurchaseOrderRepository _p);
        public bool DeleteObject(int Id, IPurchaseOrderRepository _p);
        public PurchaseOrder ConfirmObject(PurchaseOrder purchaseOrder, IPurchaseOrderRepository _p);
        public PurchaseOrder UnconfirmObject(PurchaseOrder purchaseOrder, IPurchaseOrderRepository _p);
    }
}