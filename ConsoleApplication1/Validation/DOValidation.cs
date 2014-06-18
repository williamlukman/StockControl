
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
        private IStockAdjustmentService _sa;
        private IStockAdjustmentDetailService _sad;

        public DOValidation(DeliveryOrderValidator _dov, DeliveryOrderDetailValidator _dodv, IContactService c, IItemService i, IStockMutationService sm,
                                 IPurchaseOrderService po, IPurchaseReceivalService pr,
                                 ISalesOrderService so, IDeliveryOrderService d,
                                 IPurchaseOrderDetailService pod, IPurchaseReceivalDetailService prd,
                                 ISalesOrderDetailService sod, IDeliveryOrderDetailService dod,
                                 IStockAdjustmentService sa, IStockAdjustmentDetailService sad)
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
            _sa = sa;
            _sad = sad;
        }

        public void DOValidation1()
        {
            Console.WriteLine("     [DO 1] Create valid DO for Michaelangelo");
            DeliveryOrder d = _do.CreateObject(_c.GetObjectByName("Michaelangelo Buanorotti").Id, DateTime.Now,_c);
            if (d.Errors.Any()) { Console.WriteLine("        >> " + _do.GetValidator().PrintError(d)); }
        }

        public int DOValidation2(int salesOrderDetailId)
        {
            Console.WriteLine("     [DO 2] Create valid DOD for Michaelangelo");
            int contactId = _c.GetObjectByName("Michaelangelo Buanorotti").Id;
            int doid = _do.GetObjectsByContactId(contactId).FirstOrDefault().Id;
            DeliveryOrderDetail dod1 = _dod.CreateObject(doid, _i.GetObjectByName("Buku Tulis Kiky A5").Id, 100, salesOrderDetailId, _do, _sod, _so, _i, _c);
            if (dod1.Errors.Any()) { Console.WriteLine("        >> " + _dod.GetValidator().PrintError(dod1)); return 0; }
            return dod1.Id;
        }

        public void DOValidation3()
        {
            Console.WriteLine("     [DO 3] Create invalid DO (wrong contact id)");
            DeliveryOrder d = _do.CreateObject(0, DateTime.Now, _c);
            if (d.Errors.Any()) { Console.WriteLine("        >> " + _do.GetValidator().PrintError(d)); }
        }

        public void DOValidation4()
        {
            Console.WriteLine("     [DO 4] Create valid Delivery Order");
            DeliveryOrder d = _do.CreateObject(_c.GetObjectByName("Andy Robinson").Id, new DateTime(2000, 2, 28), _c);
            if (d.Errors.Any()) { Console.WriteLine("        >> " + _do.GetValidator().PrintError(d)); }
        }

        public void DOValidation5(int salesOrderDetailId)
        {
            Console.WriteLine("     [DO 5] Create invalid DOD for Michaelangelo with wrong contact");
            int contactId = _c.GetObjectByName("Michaelangelo Buanorotti").Id;
            DeliveryOrderDetail dod1 = _dod.CreateObject(0, _i.GetObjectByName("Mini Garuda Indonesia").Id, 100, salesOrderDetailId, _do, _sod, _so, _i, _c);
            if (dod1.Errors.Any()) { Console.WriteLine("        >> " + _dod.GetValidator().PrintError(dod1)); }
        }

        public void DOValidation6(int salesOrderDetailId)
        {
            Console.WriteLine("     [DO 6] Create invalid DOD for Michaelangelo with exact same item");
            int contactId = _c.GetObjectByName("Michaelangelo Buanorotti").Id;
            DeliveryOrderDetail dod1 = _dod.CreateObject(_do.GetObjectsByContactId(contactId).FirstOrDefault().Id, _i.GetObjectByName("Buku Tulis Kiky A5").Id, 50, salesOrderDetailId, _do, _sod, _so, _i, _c);
            if (dod1.Errors.Any()) { Console.WriteLine("        >> " + _dod.GetValidator().PrintError(dod1)); }
        }

        public void DOValidation7(int salesOrderDetailId)
        {
            Console.WriteLine("     [DO 7] Create valid DOD for Michaelangelo");
            int contactId = _c.GetObjectByName("Michaelangelo Buanorotti").Id;
            DeliveryOrderDetail dod1 = _dod.CreateObject(_do.GetObjectsByContactId(_c.GetObjectByName("Michaelangelo Buanorotti").Id).FirstOrDefault().Id, _i.GetObjectByName("Mini Garuda Indonesia").Id, 50, salesOrderDetailId, _do, _sod, _so, _i, _c);
            if (dod1.Errors.Any()) { Console.WriteLine("        >> " + _dod.GetValidator().PrintError(dod1)); }
        }

        public void DOValidation8()
        {
            Console.WriteLine("     [DO 8] Confirm DO and DOD for Michaelangelo");
            DeliveryOrder d = _do.GetObjectsByContactId(_c.GetObjectByName("Michaelangelo Buanorotti").Id).FirstOrDefault();
            d.ConfirmedAt = new DateTime(2014, 5,6); 
            d = _do.ConfirmObject(d, _dod, _sod, _sm, _i);
            if (d.Errors.Any()) { Console.WriteLine("        >> " + _do.GetValidator().PrintError(d)); }
        }

        public void DOValidation9()
        {
            Console.WriteLine("     [DO 9] Unconfirm DO and DOD for Michaelangelo");
            DeliveryOrder d = _do.UnconfirmObject(_do.GetObjectsByContactId(_c.GetObjectByName("Michaelangelo Buanorotti").Id).FirstOrDefault(), _dod, _sm, _i);
            if (d.Errors.Any()) { Console.WriteLine("        >> " + _do.GetValidator().PrintError(d)); }
        }

        public void DOValidation10()
        {

            Contact contact = _c.CreateObject("Bpk. Presiden", "Istana Negara");
            Item item_batiktulis = _i.CreateObject("Batik Tulis", "Untuk Para Menteri Negara", "RI001");
            Item item_busway = _i.CreateObject("Busway", "Untuk disumbangkan bagi kebutuhan DKI Jakarta", "RI002");
            Item item_botolaqua = _i.CreateObject("Botol Aqua", "Minuman untuk pekerja di Istana Negara", "RI003");
            StockAdjustment stockAdjustment = _sa.CreateObject(new DateTime(2014, 1, 1));
            StockAdjustmentDetail stockAdjust_batiktulis = _sad.CreateObject(stockAdjustment.Id, item_batiktulis.Id, 990, 1400000, _sa, _i);
            StockAdjustmentDetail stockAdjust_busway = _sad.CreateObject(stockAdjustment.Id, item_busway.Id, 120, 725000000, _sa, _i);
            StockAdjustmentDetail stockAdjust_botolaqua = _sad.CreateObject(stockAdjustment.Id, item_botolaqua.Id, 5000, 4000, _sa, _i);
            SalesOrder salesOrder1 = _so.CreateObject(contact.Id, new DateTime(2014, 07, 09), _c);
            SalesOrder salesOrder2 = _so.CreateObject(contact.Id, new DateTime(2014, 04, 09), _c);
            SalesOrderDetail salesOrderDetail_batiktulis_so1 = _sod.CreateObject(salesOrder1.Id, item_batiktulis.Id, 500, 2000000, _so, _i);
            SalesOrderDetail salesOrderDetail_busway_so1 = _sod.CreateObject(salesOrder1.Id, item_busway.Id, 91, 800000000, _so, _i);
            SalesOrderDetail salesOrderDetail_botolaqua_so1 = _sod.CreateObject(salesOrder1.Id, item_botolaqua.Id, 2000, 5000, _so, _i);
            SalesOrderDetail salesOrderDetail_batiktulis_so2 = _sod.CreateObject(salesOrder2.Id, item_batiktulis.Id, 40, 2000500, _so, _i);
            SalesOrderDetail salesOrderDetail_busway_so2 = _sod.CreateObject(salesOrder2.Id, item_busway.Id, 3, 810000000, _so, _i);
            SalesOrderDetail salesOrderDetail_botolaqua_so2 = _sod.CreateObject(salesOrder2.Id, item_botolaqua.Id, 340, 5500, _so, _i);
            salesOrder1 = _so.ConfirmObject(salesOrder1, _sod, _sm, _i);
            salesOrder2 = _so.ConfirmObject(salesOrder2, _sod, _sm, _i);

        }
    }
}
