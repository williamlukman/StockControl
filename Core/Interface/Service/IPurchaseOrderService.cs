using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Service
{
    public interface IPurchaseOrderService
    {
        public IList<PurchaseOrder> GetAll();
        public PurchaseOrder GetObjectById(int Id);
        public PurchaseOrder CreateObject(PurchaseOrder purchaseOrder);
        public PurchaseOrder UpdateObject(PurchaseOrder purchaseOrder);
        public PurchaseOrder SoftDeleteObject(PurchaseOrder purchaseOrder);
        public bool DeleteObject(int Id);
        public PurchaseOrder ConfirmObject(PurchaseOrder purchaseOrder);
        public PurchaseOrder UnconfirmObject(PurchaseOrder purchaseOrder);
    }
}