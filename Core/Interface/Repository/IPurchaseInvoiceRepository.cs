using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Repository
{
    public interface IPurchaseInvoiceRepository : IRepository<PurchaseInvoice>
    {
        IList<PurchaseInvoice> GetAll();
        PurchaseInvoice GetObjectById(int Id);
        IList<PurchaseInvoice> GetObjectsByContactId(int contactId);
        PurchaseInvoice CreateObject(PurchaseInvoice purchaseInvoice);
        PurchaseInvoice UpdateObject(PurchaseInvoice purchaseInvoice);
        PurchaseInvoice SoftDeleteObject(PurchaseInvoice purchaseInvoice);
        bool DeleteObject(int Id);
        PurchaseInvoice ConfirmObject(PurchaseInvoice purchaseInvoice);
        PurchaseInvoice UnconfirmObject(PurchaseInvoice purchaseInvoice);
        string SetObjectCode();
    }
}