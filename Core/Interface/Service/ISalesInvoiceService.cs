using Core.DomainModel;
using Core.Interface.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Service
{
    public interface ISalesInvoiceService
    {
        ISalesInvoiceValidator GetValidator();
        IList<SalesInvoice> GetAll();
        SalesInvoice GetObjectById(int Id);
        IList<SalesInvoice> GetObjectsByContactId(int contactId);
        SalesInvoice CreateObject(SalesInvoice salesInvoice, IContactService _contactService);
        SalesInvoice CreateObject(int contactId, string description, decimal totalAmount, IContactService _contactService);
        SalesInvoice UpdateObject(SalesInvoice salesInvoice, ISalesInvoiceDetailService _salesInvoiceDetailService, IContactService _contactService);
        SalesInvoice SoftDeleteObject(SalesInvoice salesInvoice);
        bool DeleteObject(int Id);
        SalesInvoice ConfirmObject(SalesInvoice salesInvoice, ISalesInvoiceDetailService _salesInvoiceDetailService, IDeliveryOrderDetailService _deliveryOrderDetailService, IReceivableService _receivableService);
        SalesInvoice UnconfirmObject(SalesInvoice salesInvoice, ISalesInvoiceDetailService _salesInvoiceDetailService,
                                        IReceiptVoucherDetailService _receiptVoucherDetailService, IReceivableService _receivableService);
    }
}