using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Repository
{
    interface IPurchaseOrderRepository
    {
        IList<PurchaseOrder> GetAll();
        PurchaseOrder GetObjectById(int Id);
        PurchaseOrder CreateObject(PurchaseOrder purchaseOrder);
        PurchaseOrder UpdateObject(PurchaseOrder purchaseOrder);
        PurchaseOrder SoftDeleteObject(PurchaseOrder purchaseOrder);
        bool DeleteObject(int Id);
        bool ConfirmObject(PurchaseOrder purchaseOrder);
        bool UnconfirmObject(PurchaseOrder purchaseOrder);

    }
}