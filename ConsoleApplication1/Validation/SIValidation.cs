
using Core.DomainModel;
using Core.Interface.Service;
using Core.Interface.Validation;
using Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validation.Validation;

namespace ConsoleApp.Validation
{
    public class SIValidation
    {
        private SalesInvoiceValidator siv;
        private SalesInvoiceDetailValidator sidv;
        private IContactService _c;
        private IItemService _i;
        private IPurchaseOrderService _po;
        private IPurchaseOrderDetailService _pod;
        private IPurchaseReceivalService _pr;
        private IPurchaseReceivalDetailService _prd;
        private ISalesOrderService _so;
        private ISalesOrderDetailService _sod;
        private IDeliveryOrderService _do;
        private IDeliveryOrderDetailService _dod;
        private IStockMutationService _sm;
        private ISalesInvoiceService _si;
        private ISalesInvoiceDetailService _sid;
        private IReceivableService _receivable;
        private IPaymentVoucherService _pv;
        private IPaymentVoucherDetailService _pvd;

        public SIValidation(SalesInvoiceValidator _siv, SalesInvoiceDetailValidator _sidv,
                                 ISalesInvoiceService si, ISalesInvoiceDetailService sid,
                                 IReceivableService receivable,
                                 IContactService c, IItemService i, IStockMutationService sm,
                                 IPurchaseOrderService po, IPurchaseReceivalService pr,
                                 ISalesOrderService so, IDeliveryOrderService d,
                                 IPurchaseOrderDetailService pod, IPurchaseReceivalDetailService prd,
                                 ISalesOrderDetailService sod, IDeliveryOrderDetailService dod)
        {
            siv = _siv;
            sidv = _sidv;
            _c = c;
            _i = i;
            _sm = sm;
            _po = po;
            _pr = pr;
            _so = so;
            _do = d;
            _pod = pod;
            _prd = prd;
            _sod = sod;
            _dod = dod;
            _si = si;
            _sid = sid;
            _receivable = receivable;
        }

        public int SIValidation1()
        {
            Console.WriteLine("     [SI 1] Create valid Sales Invoice for Michaelangelo");
            Contact c = _c.GetObjectByName("Michaelangelo Buanorotti");
            SalesInvoice si = _si.CreateObject(c.Id, "Trial", 10, _c);
            si = _si.CreateObject(si, _c);
            if (si.Errors.Any()) { Console.WriteLine("        >> " + _si.GetValidator().PrintError(si)); return 0; }
            return si.Id;
        }

        public int SIValidation2(int piId, int deliveryOrderDetailId)
        {
            Console.WriteLine("     [SI 2] Create valid Sales Invoice Detail for Michaelangelo");
            SalesInvoice si = _si.GetObjectById(piId);
            SalesInvoiceDetail sid = _sid.CreateObject(si.Id, deliveryOrderDetailId, 2, 2, _si, _dod);
            if (sid.Errors.Any()) { Console.WriteLine("        >> " + _sid.GetValidator().PrintError(sid)); return 0; }
            return sid.Id;
        }

        public void SIValidation3(int salesInvoiceDetailId)
        {
            Console.WriteLine("     [SI 3] Create Sales Invoice Detail");
            SalesInvoiceDetail sid = _sid.GetObjectById(salesInvoiceDetailId);
            SalesInvoice si = _si.GetObjectById(sid.SalesInvoiceId);
            si = _si.ConfirmObject(si, _sid, _dod, _receivable);
            if (sid.Errors.Any()) { Console.WriteLine("        >> " + _sid.GetValidator().PrintError(sid)); }
        }

        public void SIValidation4()
        {
            Console.WriteLine("     [SI 4] Create valid Purchase Order");
        }
    }
}
