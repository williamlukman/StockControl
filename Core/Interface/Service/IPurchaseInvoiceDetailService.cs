using Core.DomainModel;
using Core.Interface.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Service
{
    public interface IPurchaseInvoiceDetailService
    {
        IPurchaseInvoiceDetailValidator GetValidator();
        IList<PurchaseInvoiceDetail> GetObjectsByPurchaseInvoiceId(int purchaseInvoiceId);
        PurchaseInvoiceDetail GetObjectById(int Id);
        PurchaseInvoiceDetail CreateObject(PurchaseInvoiceDetail purchaseInvoiceDetail, IPurchaseInvoiceService _pis, IPurchaseReceivalDetailService _prds);
        PurchaseInvoiceDetail CreateObject(int purchaseInvoiceId, int purchaseReceivalDetailId, int itemId, int quantity, decimal price, IPurchaseInvoiceService _pis, IPurchaseReceivalDetailService _prds);
        PurchaseInvoiceDetail UpdateObject(PurchaseInvoiceDetail purchaseInvoiceDetail, IPurchaseReceivalDetailService _prds);
        PurchaseInvoiceDetail SoftDeleteObject(PurchaseInvoiceDetail purchaseInvoiceDetail);
        bool DeleteObject(int Id);
        PurchaseInvoiceDetail ConfirmObject(PurchaseInvoiceDetail purchaseInvoiceDetail, IPurchaseInvoiceDetailService _pids, IPurchaseReceivalDetailService _prds);
        PurchaseInvoiceDetail UnconfirmObject(PurchaseInvoiceDetail purchaseInvoiceDetail, IPaymentVoucherDetailService _pvds, IPayableService _payableService);
    }
}