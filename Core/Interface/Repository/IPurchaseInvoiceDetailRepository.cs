using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Repository
{
    public interface IPurchaseInvoiceDetailRepository : IRepository<PurchaseInvoiceDetail>
    {
        IList<PurchaseInvoiceDetail> GetObjectsByPurchaseInvoiceId(int purchaseInvoiceId);
        PurchaseInvoiceDetail GetObjectById(int Id);
        PurchaseInvoiceDetail CreateObject(PurchaseInvoiceDetail purchaseInvoiceDetail);
        PurchaseInvoiceDetail UpdateObject(PurchaseInvoiceDetail purchaseInvoiceDetail);
        PurchaseInvoiceDetail SoftDeleteObject(PurchaseInvoiceDetail purchaseInvoiceDetail);
        bool DeleteObject(int Id);
        PurchaseInvoiceDetail ConfirmObject(PurchaseInvoiceDetail purchaseInvoiceDetail);
        PurchaseInvoiceDetail UnconfirmObject(PurchaseInvoiceDetail purchaseInvoiceDetail);
    }
}