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
        SalesInvoice CreateObject(SalesInvoice salesOrder);
        SalesInvoice UpdateObject(SalesInvoice salesOrder);
        SalesInvoice SoftDeleteObject(SalesInvoice salesOrder);
        bool DeleteObject(int Id);
        SalesInvoice ConfirmObject(SalesInvoice salesOrder);
        SalesInvoice UnconfirmObject(SalesInvoice salesOrder);
    }
}