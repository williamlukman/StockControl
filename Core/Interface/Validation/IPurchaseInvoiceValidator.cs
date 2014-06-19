using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DomainModel;
using Core.Interface.Service;

namespace Core.Interface.Validation
{
    public interface IPurchaseInvoiceValidator
    {
        PurchaseInvoice VContact(PurchaseInvoice pi, IContactService _contactService);
        PurchaseInvoice VHasPurchaseInvoiceDetails(PurchaseInvoice pi, IPurchaseInvoiceDetailService _pid);
        PurchaseInvoice VHasPayment(PurchaseInvoice pi, IPayableService _payableService, IPaymentVoucherDetailService _paymentVoucherDetailService);
        PurchaseInvoice VIsConfirmed(PurchaseInvoice pi);
        PurchaseInvoice VUpdateContactWithPurchaseInvoiceDetails(PurchaseInvoice pi, IPurchaseInvoiceService _pis, IPurchaseInvoiceDetailService _pids);
        PurchaseInvoice VCreateObject(PurchaseInvoice pi, IContactService _cs);
        PurchaseInvoice VUpdateObject(PurchaseInvoice pi, IPurchaseInvoiceService _pis, IPurchaseInvoiceDetailService _pids, IContactService _cs);
        PurchaseInvoice VDeleteObject(PurchaseInvoice pi);
        PurchaseInvoice VConfirmObject(PurchaseInvoice pi, IPurchaseInvoiceDetailService _pids, IPurchaseReceivalDetailService _prds);
        PurchaseInvoice VUnconfirmObject(PurchaseInvoice pi, IPurchaseInvoiceDetailService _pids, IPaymentVoucherDetailService _pvds, IPayableService _payableService);
        bool ValidCreateObject(PurchaseInvoice pi, IContactService _cs);
        bool ValidUpdateObject(PurchaseInvoice pi, IPurchaseInvoiceService _pi, IPurchaseInvoiceDetailService _pid, IContactService _c);
        bool ValidDeleteObject(PurchaseInvoice pi);
        bool ValidConfirmObject(PurchaseInvoice pi, IPurchaseInvoiceDetailService _pids, IPurchaseReceivalDetailService _prds);
        bool ValidUnconfirmObject(PurchaseInvoice pi, IPurchaseInvoiceDetailService _pids, IPaymentVoucherDetailService _pvds, IPayableService _payableService);
        bool isValid(PurchaseInvoice pi);
        string PrintError(PurchaseInvoice pi);
    }
}
