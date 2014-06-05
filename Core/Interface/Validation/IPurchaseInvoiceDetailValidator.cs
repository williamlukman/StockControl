using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DomainModel;
using Core.Interface.Service;

namespace Core.Interface.Validation
{
    public interface IPurchaseInvoiceDetailValidator
    {
        PurchaseInvoiceDetail VHasPurchaseReceivalDetail(PurchaseInvoiceDetail pid, IPurchaseReceivalDetailService _prds);
        PurchaseInvoiceDetail VQuantity(PurchaseInvoiceDetail pid, IPurchaseReceivalDetailService _prds);
        PurchaseInvoiceDetail VPrice(PurchaseInvoiceDetail pid);
        PurchaseInvoiceDetail VIsUniquePurchaseReceivalDetail(PurchaseInvoiceDetail pid, IPurchaseInvoiceDetailService _pids, IPurchaseReceivalDetailService _prds);
        PurchaseInvoiceDetail VHasPayment(PurchaseInvoiceDetail pid, IPaymentVoucherDetailService _pvds, IPayableService _payableService);
        PurchaseInvoiceDetail VIsConfirmed(PurchaseInvoiceDetail pid);
        PurchaseInvoiceDetail VCreateObject(PurchaseInvoiceDetail pid, IPurchaseInvoiceDetailService _pids, IPurchaseReceivalDetailService _prds);
        PurchaseInvoiceDetail VUpdateObject(PurchaseInvoiceDetail pid, IPurchaseInvoiceDetailService _pids, IPurchaseReceivalDetailService _prds);
        PurchaseInvoiceDetail VDeleteObject(PurchaseInvoiceDetail pid);
        PurchaseInvoiceDetail VConfirmObject(PurchaseInvoiceDetail pid, IPurchaseInvoiceDetailService _pids, IPurchaseReceivalDetailService _prds);
        PurchaseInvoiceDetail VUnconfirmObject(PurchaseInvoiceDetail pid, IPaymentVoucherDetailService _pvds, IPayableService _payableService);
        bool ValidCreateObject(PurchaseInvoiceDetail pid, IPurchaseInvoiceDetailService _pids, IPurchaseReceivalDetailService _prds);
        bool ValidUpdateObject(PurchaseInvoiceDetail pid, IPurchaseInvoiceDetailService _pids, IPurchaseReceivalDetailService _prds);
        bool ValidDeleteObject(PurchaseInvoiceDetail pid);
        bool ValidConfirmObject(PurchaseInvoiceDetail pid, IPurchaseInvoiceDetailService _pids, IPurchaseReceivalDetailService _prds);
        bool ValidUnconfirmObject(PurchaseInvoiceDetail pid, IPaymentVoucherDetailService _pvds, IPayableService _payableService);
        bool isValid(PurchaseInvoiceDetail pid);
        string PrintError(PurchaseInvoiceDetail pid);
    }
}
