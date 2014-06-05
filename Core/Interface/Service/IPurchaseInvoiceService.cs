using Core.DomainModel;
using Core.Interface.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Service
{
    public interface IPurchaseInvoiceService
    {
        IPurchaseInvoiceValidator GetValidator();
        IList<PurchaseInvoice> GetAll();
        PurchaseInvoice GetObjectById(int Id);
        IList<PurchaseInvoice> GetObjectsByContactId(int contactId);
        PurchaseInvoice CreateObject(PurchaseInvoice purchaseInvoice, IContactService _contactService);
        PurchaseInvoice UpdateObject(PurchaseInvoice purchaseInvoice, IPurchaseInvoiceDetailService _pid, IContactService _c);
        PurchaseInvoice SoftDeleteObject(PurchaseInvoice purchaseInvoice);
        bool DeleteObject(int Id);
        PurchaseInvoice ConfirmObject(PurchaseInvoice purchaseInvoice, IPurchaseInvoiceDetailService _purchaseInvoiceDetailService, IPurchaseReceivalDetailService _prds, IPayableService _payableService);
        PurchaseInvoice UnconfirmObject(PurchaseInvoice purchaseInvoice, IPurchaseInvoiceDetailService _purchaseInvoiceDetailService,
                                        IPaymentVoucherDetailService _pvds, IPayableService _payableService);
    }
}