
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
    public class SOValidation
    {
        private SalesOrderValidator sov;
        private SalesOrderDetailValidator sodv;
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

        public SOValidation(SalesOrderValidator _sov, SalesOrderDetailValidator _sodv, IContactService c, IItemService i, IStockMutationService sm,
                                 IPurchaseOrderService po, IPurchaseReceivalService pr,
                                 ISalesOrderService so, IDeliveryOrderService d,
                                 IPurchaseOrderDetailService pod, IPurchaseReceivalDetailService prd,
                                 ISalesOrderDetailService sod, IDeliveryOrderDetailService dod)
        {
            sov = _sov;
            sodv = _sodv;
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
        }

        public void SOValidation1()
        {
            Console.WriteLine("     [SO 1] Create valid Sales Order for Michaelangelo");
            SalesOrder so = _so.CreateObject(_c.GetObjectByName("Michaelangelo Buanorotti").Id, DateTime.Now,_c);
            if (so.Errors.Any()) { Console.WriteLine("        >> " + _so.GetValidator().PrintError(so)); }
        }

        public int SOValidation2()
        {
            Console.WriteLine("     [SO 2] Create valid Sales Order Detail for Michaelangelo");
            SalesOrderDetail sod1 = _sod.CreateObject(_so.GetObjectsByContactId(_c.GetObjectByName("Michaelangelo Buanorotti").Id).FirstOrDefault().Id, _i.GetObjectByName("Buku Tulis Kiky A5").Id, 100, (decimal) 50000.00, _so, _i);
            if (sod1.Errors.Any()) { Console.WriteLine("        >> " + _sod.GetValidator().PrintError(sod1)); return 0; }
            return sod1.Id;
        }

        public void SOValidation3()
        {
            Console.WriteLine("     [SO 3] Create invalid Sales Order (wrong contact id)");
            SalesOrder so = _so.CreateObject(0, DateTime.Now, _c);
            if (so.Errors.Any()) { Console.WriteLine("        >> " + _so.GetValidator().PrintError(so)); }
        }

        public void SOValidation4()
        {
            Console.WriteLine("     [SO 4] Create valid SO");
            SalesOrder so = _so.CreateObject(_c.GetObjectByName("Andy Robinson").Id, new DateTime(2000, 2, 28), _c);
            if (so.Errors.Any()) { Console.WriteLine("        >> " + _so.GetValidator().PrintError(so)); }
        }

        public int SOValidation5()
        {
            Console.WriteLine("     [SO 5] Create invalid SOD for Michaelangelo with wrong contact");
            SalesOrderDetail sod1 = _sod.CreateObject(0, _i.GetObjectByName("Mini Garuda Indonesia").Id, 100, (decimal) 120000.00, _so, _i);
            if (sod1.Errors.Any()) { Console.WriteLine("        >> " + _sod.GetValidator().PrintError(sod1)); return 0; }
            return sod1.Id;
        }

        public int SOValidation6()
        {
            Console.WriteLine("     [SO 6] Create invalid SOD for Michaelangelo with exact same item");
            SalesOrderDetail sod1 = _sod.CreateObject(_so.GetObjectsByContactId(_c.GetObjectByName("Michaelangelo Buanorotti").Id).FirstOrDefault().Id, _i.GetObjectByName("Buku Tulis Kiky A5").Id, 50, (decimal) 35000.00, _so, _i);
            if (sod1.Errors.Any()) { Console.WriteLine("        >> " + _sod.GetValidator().PrintError(sod1)); return 0; }
            return sod1.Id;
        }

        public int SOValidation7()
        {
            Console.WriteLine("     [SO 7] Create valid SOD for Michaelangelo");
            SalesOrderDetail sod1 = _sod.CreateObject(_so.GetObjectsByContactId(_c.GetObjectByName("Michaelangelo Buanorotti").Id).FirstOrDefault().Id, _i.GetObjectByName("Mini Garuda Indonesia").Id, 50, (decimal) 114500.00, _so, _i);
            if (sod1.Errors.Any()) { Console.WriteLine("        >> " + _sod.GetValidator().PrintError(sod1)); return 0; }
            return sod1.Id;
        }

        public void SOValidation8()
        {
            Console.WriteLine("     [SO 8] Confirm SO for Michaelangelo");
            SalesOrder so = _so.GetObjectsByContactId(_c.GetObjectByName("Michaelangelo Buanorotti").Id).FirstOrDefault();
            so.ConfirmedAt = new DateTime(2014, 5, 6);
            so = _so.ConfirmObject(so, _sod, _sm, _i);
            if (so.Errors.Any()) { Console.WriteLine("        >> " + _so.GetValidator().PrintError(so)); }
        }

        public void SOValidation9()
        {
            Console.WriteLine("     [SO 9] Unconfirm SO for Michaelangelo");
            SalesOrder so = _so.UnconfirmObject(_so.GetObjectsByContactId(_c.GetObjectByName("Michaelangelo Buanorotti").Id).FirstOrDefault(), _sod, _dod, _sm, _i);
            if (so.Errors.Any()) { Console.WriteLine("        >> " + _so.GetValidator().PrintError(so)); }
        }

        public void SOValidation10()
        {
            Console.WriteLine("     [SO 10] Unconfirm SO for Michaelangelo with confirmed PR");
            SalesOrder so = _so.UnconfirmObject(_so.GetObjectsByContactId(_c.GetObjectByName("Michaelangelo Buanorotti").Id).FirstOrDefault(), _sod, _dod, _sm, _i);
            if (so.Errors.Any()) { Console.WriteLine("        >> " + _so.GetValidator().PrintError(so)); }
        }
    }
}
