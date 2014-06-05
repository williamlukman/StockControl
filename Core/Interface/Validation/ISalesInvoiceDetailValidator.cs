using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DomainModel;
using Core.Interface.Service;

namespace Core.Interface.Validation
{
    public interface ISalesInvoiceDetailValidator
    {
        SalesInvoiceDetail VHasDeliveryOrderDetail(SalesInvoiceDetail sid, IDeliveryOrderDetailService _dod);
        SalesInvoiceDetail VQuantity(SalesInvoiceDetail sid);
        SalesInvoiceDetail VPrice(SalesInvoiceDetail sid);
        SalesInvoiceDetail VIsUniqueDeliveryOrderDetail(SalesInvoice sid, IDeliveryOrderDetailService _dod);
        SalesInvoiceDetail VHasReceipt(SalesInvoice sid, ISalesInvoiceDetailService _sid, IReceivableService _r);
        SalesInvoiceDetail VIsBank(SalesInvoiceDetail sid);
        SalesInvoiceDetail VCreateObject(SalesInvoiceDetail sid, ISalesInvoiceDetailService _sid);
        SalesInvoiceDetail VUpdateObject(SalesInvoiceDetail sid, ISalesInvoiceDetailService _sid);
        SalesInvoiceDetail VDeleteObject(SalesInvoiceDetail sid, ISalesInvoiceDetailService _sid);
        bool ValidCreateObject(SalesInvoiceDetail sid, ISalesInvoiceDetailService _sid);
        bool ValidUpdateObject(SalesInvoiceDetail sid, ISalesInvoiceDetailService _sid);
        bool ValidDeleteObject(SalesInvoiceDetail sid, ISalesInvoiceDetailService _sid);
        bool isValid(SalesInvoiceDetail sid);
        string PrintError(SalesInvoiceDetail sid);
    }
}
