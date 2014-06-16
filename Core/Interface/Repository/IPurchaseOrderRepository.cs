using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Repository
{
    public interface IPurchaseOrderRepository : IRepository<PurchaseOrder>
    {
        IList<PurchaseOrder> GetAll();
        PurchaseOrder GetObjectById(int Id);
        IList<PurchaseOrder> GetObjectsByContactId(int contactId);
        PurchaseOrder CreateObject(PurchaseOrder purchaseOrder);
        PurchaseOrder UpdateObject(PurchaseOrder purchaseOrder);
        PurchaseOrder SoftDeleteObject(PurchaseOrder purchaseOrder);
        bool DeleteObject(int Id);
        PurchaseOrder ConfirmObject(PurchaseOrder purchaseOrder);
        PurchaseOrder UnconfirmObject(PurchaseOrder purchaseOrder);
        string SetObjectCode();
    }
}