using Core.DomainModel;
using Core.Interface.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Service
{
    public interface ISalesInvoiceDetailService
    {
        ISalesInvoiceDetailValidator GetValidator();
        IList<SalesInvoiceDetail> GetObjectsBySalesInvoiceId(int salesInvoiceId);
        SalesInvoiceDetail GetObjectById(int Id);
        SalesInvoiceDetail CreateObject(SalesInvoiceDetail salesInvoiceDetail);
        SalesInvoiceDetail UpdateObject(SalesInvoiceDetail salesInvoiceDetail);
        SalesInvoiceDetail SoftDeleteObject(SalesInvoiceDetail salesInvoiceDetail);
        bool DeleteObject(int Id);
        SalesInvoiceDetail ConfirmObject(SalesInvoiceDetail salesInvoiceDetail);
        SalesInvoiceDetail UnconfirmObject(SalesInvoiceDetail salesInvoiceDetail);
        SalesInvoiceDetail FulfilObject(SalesInvoiceDetail salesInvoiceDetail);

    }
}