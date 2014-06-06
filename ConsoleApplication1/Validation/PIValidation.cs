using ConsoleApp.DataAccess;
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
    public class PIValidation
    {
        private PurchaseInvoiceValidator piv;
        private PurchaseInvoiceDetailValidator pidv;
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
        private IPurchaseInvoiceService _pi;
        private IPurchaseInvoiceDetailService _pid;
        private IPayableService _payable;
        private IPaymentVoucherService _pv;
        private IPaymentVoucherDetailService _pvd;

        public PIValidation(PurchaseInvoiceValidator _piv, PurchaseInvoiceDetailValidator _pidv,
                                 IPurchaseInvoiceService pi, IPurchaseInvoiceDetailService pid,
                                 IPayableService payable,
                                 IContactService c, IItemService i, IStockMutationService sm,
                                 IPurchaseOrderService po, IPurchaseReceivalService pr,
                                 ISalesOrderService so, IDeliveryOrderService d,
                                 IPurchaseOrderDetailService pod, IPurchaseReceivalDetailService prd,
                                 ISalesOrderDetailService sod, IDeliveryOrderDetailService dod)
        {
            piv = _piv;
            pidv = _pidv;
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
            _pi = pi;
            _pid = pid;
            //_pv = pv;
            //_pvd = pvd;
            _payable = payable;
        }

        public int PIValidation1()
        {
            Console.WriteLine("     [PI 1] Create valid Purchase Invoice for Michaelangelo");
            Contact c = _c.GetObjectByName("Michaelangelo Buanorotti");
            PurchaseInvoice pi = _pi.CreateObject(c.Id, "Trial", 10, _c);
            pi = _pi.CreateObject(pi, _c);
            if (pi.Errors.Any()) { Console.WriteLine("        >> " + _pi.GetValidator().PrintError(pi)); return 0; }
            return pi.Id;
        }

        public int PIValidation2(int piId, int purchaseReceivalDetailId)
        {
            Console.WriteLine("     [PI 2] Create valid Purchase Invoice Detail for Michaelangelo");
            PurchaseInvoice pi = _pi.GetObjectById(piId);
            PurchaseInvoiceDetail pid = _pid.CreateObject(pi.Id, purchaseReceivalDetailId, 2, 2, _pi, _prd);
            if (pid.Errors.Any()) { Console.WriteLine("        >> " + _pid.GetValidator().PrintError(pid)); return 0; }
            return pid.Id;
        }

        public void PIValidation3(int purchaseInvoiceDetailId)
        {
            Console.WriteLine("     [PI 3] Create invalid Purchase Order (wrong contact id)");
            PurchaseInvoiceDetail pid = _pid.GetObjectById(purchaseInvoiceDetailId);
            PurchaseInvoice pi = _pi.GetObjectById(pid.PurchaseInvoiceId);
            pi = _pi.ConfirmObject(pi, _pid, _prd, _payable);
            if (pid.Errors.Any()) { Console.WriteLine("        >> " + _pid.GetValidator().PrintError(pid)); }
        }

        public void PIValidation4()
        {
            Console.WriteLine("     [PI 4] Create valid Purchase Order");
        }
    }
}
