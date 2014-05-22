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
        IList<PurchaseOrder> GetAll();
        PurchaseOrder GetObjectById(int Id);
        PurchaseOrder CreateObject(PurchaseOrder purchaseOrder);
        PurchaseOrder CreateObject(int contactId, DateTime purchaseDate);
        PurchaseOrder UpdateObject(PurchaseOrder purchaseOrder);
        PurchaseOrder SoftDeleteObject(PurchaseOrder purchaseOrder);
        bool DeleteObject(int Id);
        PurchaseOrder ConfirmObject(PurchaseOrder purchaseOrder);
        PurchaseOrder UnconfirmObject(PurchaseOrder purchaseOrder);
    }
}