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
    public class DOValidation
    {
        private DeliveryOrderValidator dov;
        private DeliveryOrderDetailValidator dodv;
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

        public DOValidation(DeliveryOrderValidator _dov, DeliveryOrderDetailValidator _dodv, IContactService c, IItemService i, IStockMutationService sm,
                                 IPurchaseOrderService po, IPurchaseReceivalService pr,
                                 ISalesOrderService so, IDeliveryOrderService d,
                                 IPurchaseOrderDetailService pod, IPurchaseReceivalDetailService prd,
                                 ISalesOrderDetailService sod, IDeliveryOrderDetailService dod)
        {
            dov = _dov;
            dodv = _dodv;
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

        public void DOValidation37()
        {
            Console.WriteLine("[Test 37] Create valid Delivery Order for Michaelangelo");
            DeliveryOrder d = _do.CreateObject(_c.GetObjectByName("Michaelangelo Buanorotti").Id, DateTime.Now,_c);
            if (d.Errors.Any()) { Console.WriteLine(_do.GetValidator().PrintError(d)); }
        }

        public void DOValidation38(int salesOrderDetailId)
        {
            Console.WriteLine("[Test 38] Create valid Delivery Order Detail for Michaelangelo");
            int doid = _do.GetObjectsByContactId(_c.GetObjectByName("Michaelangelo Buanorotti").Id).FirstOrDefault().Id;
            DeliveryOrderDetail dod1 = _dod.CreateObject(doid, _i.GetObjectByName("Buku Tulis Kiky A5").Id, 100, salesOrderDetailId, _do, _sod, _so, _i, _c);
            if (dod1.Errors.Any()) { Console.WriteLine(_dod.GetValidator().PrintError(dod1)); }
        }

        public void DOValidation39()
        {
            Console.WriteLine("[Test 39] Create invalid Delivery Order (wrong contact id)");
            DeliveryOrder d = _do.CreateObject(0, DateTime.Now, _c);
            if (d.Errors.Any()) { Console.WriteLine(_do.GetValidator().PrintError(d)); }
        }

        public void DOValidation40()
        {
            Console.WriteLine("[Test 40] Create valid Delivery Order");
            DeliveryOrder d = _do.CreateObject(_c.GetObjectByName("Andy Robinson").Id, new DateTime(2000, 2, 28), _c);
            if (d.Errors.Any()) { Console.WriteLine(_do.GetValidator().PrintError(d)); }
        }

        public void DOValidation41(int salesOrderDetailId)
        {
            Console.WriteLine("[Test 41] Create invalid Delivery Order Detail for Michaelangelo with wrong contact");
            DeliveryOrderDetail dod1 = _dod.CreateObject(0, _i.GetObjectByName("Mini Garuda Indonesia").Id, 100, salesOrderDetailId, _do, _sod, _so, _i, _c);
            if (dod1.Errors.Any()) { Console.WriteLine(_dod.GetValidator().PrintError(dod1)); }
        }

        public void DOValidation42(int salesOrderDetailId)
        {
            Console.WriteLine("[Test 42] Create invalid Delivery Order Detail for Michaelangelo with exact same item");
            DeliveryOrderDetail dod1 = _dod.CreateObject(_do.GetObjectsByContactId(_c.GetObjectByName("Michaelangelo Buanorotti").Id).FirstOrDefault().Id, _i.GetObjectByName("Buku Tulis Kiky A5").Id, 50, salesOrderDetailId, _do, _sod, _so, _i, _c);
            if (dod1.Errors.Any()) { Console.WriteLine(_dod.GetValidator().PrintError(dod1)); }
        }

        public void DOValidation43(int salesOrderDetailId)
        {
            Console.WriteLine("[Test 43] Create valid Delivery Order Detail for Michaelangelo");
            DeliveryOrderDetail dod1 = _dod.CreateObject(_do.GetObjectsByContactId(_c.GetObjectByName("Michaelangelo Buanorotti").Id).FirstOrDefault().Id, _i.GetObjectByName("Mini Garuda Indonesia").Id, 50, salesOrderDetailId, _do, _sod, _so, _i, _c);
            if (dod1.Errors.Any()) { Console.WriteLine(_dod.GetValidator().PrintError(dod1)); }
        }

        public void DOValidation44()
        {
            Console.WriteLine("[Test 44] Confirm DO and DOD for Michaelangelo");
            DeliveryOrder d = _do.ConfirmObject(_do.GetObjectsByContactId(_c.GetObjectByName("Michaelangelo Buanorotti").Id).FirstOrDefault(), _dod, _sod, _sm, _i);
            if (d.Errors.Any()) { Console.WriteLine(_do.GetValidator().PrintError(d)); }
        }

    }
}
