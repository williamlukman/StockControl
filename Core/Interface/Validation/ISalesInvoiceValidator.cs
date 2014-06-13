using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DomainModel;
using Core.Interface.Service;

namespace Core.Interface.Validation
{
    public interface ISalesInvoiceValidator
    {
        SalesInvoice VContact(SalesInvoice si, IContactService _contactService);
        SalesInvoice VHasSalesInvoiceDetails(SalesInvoice si, ISalesInvoiceDetailService _sid);
        SalesInvoice VHasReceipt(SalesInvoice si, IReceivableService _receivableService, IReceiptVoucherDetailService _receiptVoucherDetailService);
        SalesInvoice VIsConfirmed(SalesInvoice si);
        SalesInvoice VCreateObject(SalesInvoice si, IContactService _cs);
        SalesInvoice VUpdateObject(SalesInvoice si, ISalesInvoiceDetailService _sid, IContactService _cs);
        SalesInvoice VDeleteObject(SalesInvoice si);
        SalesInvoice VConfirmObject(SalesInvoice si, ISalesInvoiceDetailService _sids, IDeliveryOrderDetailService _dods);
        SalesInvoice VUnconfirmObject(SalesInvoice si, ISalesInvoiceDetailService _sids, IReceiptVoucherDetailService _rvds, IReceivableService _receivableService);
        bool ValidCreateObject(SalesInvoice si, IContactService _cs);
        bool ValidUpdateObject(SalesInvoice si, ISalesInvoiceDetailService _sid, IContactService _c);
        bool ValidDeleteObject(SalesInvoice si);
        bool ValidConfirmObject(SalesInvoice si, ISalesInvoiceDetailService _sids, IDeliveryOrderDetailService _dods);
        bool ValidUnconfirmObject(SalesInvoice si, ISalesInvoiceDetailService _sids, IReceiptVoucherDetailService _rvds, IReceivableService _receivableService);
        bool isValid(SalesInvoice si);
        string PrintError(SalesInvoice si);
    }
}
