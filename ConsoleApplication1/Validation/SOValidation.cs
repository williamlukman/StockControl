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

        public void SOValidation29()
        {
            Console.WriteLine("[Test 29] Create valid Sales Order for Michaelangelo");
            SalesOrder so = _so.CreateObject(_c.GetObjectByName("Michaelangelo Buanorotti").Id, DateTime.Now,_c);
            if (so.Errors.Any()) { Console.WriteLine(_so.GetValidator().PrintError(so)); }
        }

        public int SOValidation30()
        {
            Console.WriteLine("[Test 30] Create valid Sales Order Detail for Michaelangelo");
            SalesOrderDetail sod1 = _sod.CreateObject(_so.GetObjectsByContactId(_c.GetObjectByName("Michaelangelo Buanorotti").Id).FirstOrDefault().Id, _i.GetObjectByName("Buku Tulis Kiky A5").Id, 100, _so, _i);
            if (sod1.Errors.Any()) { Console.WriteLine(_sod.GetValidator().PrintError(sod1)); return 0; }
            return sod1.Id;
        }

        public void SOValidation31()
        {
            Console.WriteLine("[Test 31] Create invalid Sales Order (wrong contact id)");
            SalesOrder so = _so.CreateObject(0, DateTime.Now, _c);
            if (so.Errors.Any()) { Console.WriteLine(_so.GetValidator().PrintError(so)); }
        }

        public void SOValidation32()
        {
            Console.WriteLine("[Test 32] Create valid Sales Order");
            SalesOrder so = _so.CreateObject(_c.GetObjectByName("Andy Robinson").Id, new DateTime(2000, 2, 28), _c);
            if (so.Errors.Any()) { Console.WriteLine(_so.GetValidator().PrintError(so)); }
        }

        public int SOValidation33()
        {
            Console.WriteLine("[Test 33] Create invalid Sales Order Detail for Michaelangelo with wrong contact");
            SalesOrderDetail sod1 = _sod.CreateObject(0, _i.GetObjectByName("Mini Garuda Indonesia").Id, 100, _so, _i);
            if (sod1.Errors.Any()) { Console.WriteLine(_sod.GetValidator().PrintError(sod1)); return 0; }
            return sod1.Id;
        }

        public int SOValidation34()
        {
            Console.WriteLine("[Test 34] Create invalid Sales Order Detail for Michaelangelo with exact same item");
            SalesOrderDetail sod1 = _sod.CreateObject(_so.GetObjectsByContactId(_c.GetObjectByName("Michaelangelo Buanorotti").Id).FirstOrDefault().Id, _i.GetObjectByName("Buku Tulis Kiky A5").Id, 50, _so, _i);
            if (sod1.Errors.Any()) { Console.WriteLine(_sod.GetValidator().PrintError(sod1)); return 0; }
            return sod1.Id;
        }

        public int SOValidation35()
        {
            Console.WriteLine("[Test 35] Create valid Sales Order Detail for Michaelangelo");
            SalesOrderDetail sod1 = _sod.CreateObject(_so.GetObjectsByContactId(_c.GetObjectByName("Michaelangelo Buanorotti").Id).FirstOrDefault().Id, _i.GetObjectByName("Mini Garuda Indonesia").Id, 50, _so, _i);
            if (sod1.Errors.Any()) { Console.WriteLine(_sod.GetValidator().PrintError(sod1)); return 0; }
            return sod1.Id;
        }

        public void SOValidation36a()
        {
            Console.WriteLine("[Test 36a] Confirm SO and SOD for Michaelangelo");
            SalesOrder so = _so.ConfirmObject(_so.GetObjectsByContactId(_c.GetObjectByName("Michaelangelo Buanorotti").Id).FirstOrDefault(), _sod, _sm, _i);
            if (so.Errors.Any()) { Console.WriteLine(_so.GetValidator().PrintError(so)); }
        }

        public void SOValidation36b()
        {
            Console.WriteLine("[Test 36b] Unconfirm SO and SOD for Michaelangelo");
            SalesOrder so = _so.UnconfirmObject(_so.GetObjectsByContactId(_c.GetObjectByName("Michaelangelo Buanorotti").Id).FirstOrDefault(), _sod, _dod, _sm, _i);
            if (so.Errors.Any()) { Console.WriteLine(_so.GetValidator().PrintError(so)); }
        }

        public void SOValidation45()
        {
            Console.WriteLine("[Test 45] Unconfirm SO and SOD for Michaelangelo that already has confirmed PR");
            SalesOrder so = _so.UnconfirmObject(_so.GetObjectsByContactId(_c.GetObjectByName("Michaelangelo Buanorotti").Id).FirstOrDefault(), _sod, _dod, _sm, _i);
            if (so.Errors.Any()) { Console.WriteLine(_so.GetValidator().PrintError(so)); }
        }
    }
}
