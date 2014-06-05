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
        SalesInvoice VContact(SalesInvoice si);
        SalesInvoice VHasSalesInvoiceDetail(SalesInvoice si, ISalesInvoiceService _si, ISalesInvoiceDetailService _sid);
        SalesInvoice VHasReceivable(SalesInvoice si, ISalesInvoiceService _si, ISalesInvoiceDetailService _sid,
                                    IReceivableService _receivableService, IReceiptVoucherService _receiptVoucherService);
        SalesInvoice VIsConfirm(SalesInvoice si, ISalesInvoiceService _si);
        SalesInvoice VCreateObject(SalesInvoice si, ISalesInvoiceService _si);
        SalesInvoice VUpdateObject(SalesInvoice si, ISalesInvoiceService _si);
        SalesInvoice VDeleteObject(SalesInvoice si, ISalesInvoiceService _si);
        SalesInvoice VConfirmObject(SalesInvoice si, ISalesInvoiceService _si, ISalesInvoiceDetailService _sid);
        SalesInvoice VUnconfirmObject(SalesInvoice si, ISalesInvoiceService _si, ISalesInvoiceDetailService _sid);
        bool ValidCreateObject(SalesInvoice si, ISalesInvoiceService _si);
        bool ValidUpdateObject(SalesInvoice si, ISalesInvoiceService _si, ISalesInvoiceDetailService _sid);
        bool ValidDeleteObject(SalesInvoice si, ISalesInvoiceService _si);
        bool ValidConfirmObject(SalesInvoice si, ISalesInvoiceService _si, ISalesInvoiceDetailService _sid);
        bool ValidUnconfirmObject(SalesInvoice si, ISalesInvoiceService _si);
        bool isValid(SalesInvoice si);
        string PrintError(SalesInvoice si);
    }
}
