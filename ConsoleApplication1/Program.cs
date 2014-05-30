using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Context;
using Core.DomainModel;
using System.Data.Entity;
using ConsoleApp.DataAccess;
using Service.Service;
using Data.Repository;
using Core.Interface.Repository;
using Core.Interface.Service;
using System.Threading;
using ConsoleApp.Validation;
using Validation.Validation;

namespace ConsoleApp
{
    public class Program
    {
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
        
        public Program()
        {
            _c = new ContactService(new ContactRepository(), new ContactValidator());
            _i = new ItemService(new ItemRepository(), new ItemValidator());
            _po = new PurchaseOrderService(new PurchaseOrderRepository(), new PurchaseOrderValidator());
            _pod = new PurchaseOrderDetailService(new PurchaseOrderDetailRepository(), new PurchaseOrderDetailValidator());
            _pr = new PurchaseReceivalService(new PurchaseReceivalRepository(), new PurchaseReceivalValidator());
            _prd = new PurchaseReceivalDetailService(new PurchaseReceivalDetailRepository(), new PurchaseReceivalDetailValidator());
            _so = new SalesOrderService(new SalesOrderRepository(), new SalesOrderValidator());
            _sod = new SalesOrderDetailService(new SalesOrderDetailRepository(), new SalesOrderDetailValidator());
            _do = new DeliveryOrderService(new DeliveryOrderRepository(), new DeliveryOrderValidator());
            _dod = new DeliveryOrderDetailService(new DeliveryOrderDetailRepository(), new DeliveryOrderDetailValidator());
            _sm = new StockMutationService(new StockMutationRepository(), new StockMutationValidator());
        }

        public static void Main(string[] args)
        {
            //Database.SetInitializer<StockControlEntities>(new StockControlInit());
            var db = new StockControlEntities();

            using (db)
            {
                Program p = new Program();
                // Warning: this function will delete all data in the DB. Use with caution!!!
                p.flushdb(db);
                p.wait(1);
                p.ValidateContactModel(p, db);
                p.wait(1);
                p.ValidateItemModel(p, db);
                p.wait(1);
                p.ValidateReceivalModel(p, db);
                p.wait(1);
                p.ValidateDeliveryModel(p, db);

                Console.WriteLine("Press any key to stop...");
                Console.ReadKey();
            }
        }

        public void ValidateContactModel(Program p, StockControlEntities db)
        {
            Console.WriteLine("[Contact Validation Test]");
            ContactValidation cv = new ContactValidation(new ContactValidator(), this._c, this._i, this._sm,
                                           this._po, this._pr, this._so, this._do,
                                           this._pod, this._prd, this._sod, this._dod);
            cv.ContactValidation1();
            cv.ContactValidation2();
            cv.ContactValidation3();
            cv.ContactValidation4();
            cv.ContactValidation5();
            cv.ContactValidation6();
        }

        public void ValidateItemModel(Program p, StockControlEntities db)
        {
            Console.WriteLine("[Item Validation Test]");
            ItemValidation iv = new ItemValidation(new ItemValidator(), this._c, this._i, this._sm,
                                           this._po, this._pr, this._so, this._do,
                                           this._pod, this._prd, this._sod, this._dod);
            iv.ItemValidation7();
            iv.ItemValidation8();
            iv.ItemValidation9();
            iv.ItemValidation10();
            iv.ItemValidation11();
        }

        public void ValidateReceivalModel(Program p, StockControlEntities db)
        {
            Console.WriteLine("[Receival Validation Test]");
            POValidation pov = new POValidation(new PurchaseOrderValidator(), new PurchaseOrderDetailValidator(), this._c, this._i, this._sm,
                                           this._po, this._pr, this._so, this._do,
                                           this._pod, this._prd, this._sod, this._dod);
            pov.POValidation12();
            int podtest1 = pov.POValidation13();
            pov.POValidation14();
            pov.POValidation15();
            int podtest2 = pov.POValidation16();
            int podtest3 = pov.POValidation17();
            int podtest4 = pov.POValidation18();

            p.wait(2);

            pov.POValidation19a();
            ItemDb.Stock(_i.GetObjectByName("Buku Tulis Kiky A5"));
            ItemDb.Stock(_i.GetObjectByName("Mini Garuda Indonesia"));
            pov.POValidation19b();
            ItemDb.Stock(_i.GetObjectByName("Buku Tulis Kiky A5"));
            ItemDb.Stock(_i.GetObjectByName("Mini Garuda Indonesia"));
            pov.POValidation19a();

            p.wait(2);

            PRValidation prv = new PRValidation(new PurchaseReceivalValidator(), new PurchaseReceivalDetailValidator(), this._c, this._i, this._sm,
                               this._po, this._pr, this._so, this._do,
                               this._pod, this._prd, this._sod, this._dod);
            prv.PRValidation20();
            prv.PRValidation21(podtest1);
            prv.PRValidation22();
            prv.PRValidation23();

            p.wait(2);

            prv.PRValidation24(podtest4);
            prv.PRValidation25(podtest1);
            prv.PRValidation26(podtest4);
            prv.PRValidation27a();
            ItemDb.Stock(_i.GetObjectByName("Buku Tulis Kiky A5"));
            ItemDb.Stock(_i.GetObjectByName("Mini Garuda Indonesia"));

            pov.POValidation28();
            
            prv.PRValidation27b();
            ItemDb.Stock(_i.GetObjectByName("Buku Tulis Kiky A5"));
            ItemDb.Stock(_i.GetObjectByName("Mini Garuda Indonesia"));

            p.wait(2);

        }

        public void ValidateDeliveryModel(Program p, StockControlEntities db)
        {
            Console.WriteLine("[Delivery Validation Test]");
            SOValidation sov = new SOValidation(new SalesOrderValidator(), new SalesOrderDetailValidator(), this._c, this._i, this._sm,
                                           this._po, this._pr, this._so, this._do,
                                           this._pod, this._prd, this._sod, this._dod);
            sov.SOValidation29();
            int sodtest1 = sov.SOValidation30();
            sov.SOValidation31();
            sov.SOValidation32();
            
            p.wait(2);
            
            int sodtest2 = sov.SOValidation33();
            int sodtest3 = sov.SOValidation34();
            int sodtest4 = sov.SOValidation35();
            sov.SOValidation36a();

            p.wait(2);

            ItemDb.Stock(_i.GetObjectByName("Buku Tulis Kiky A5"));
            ItemDb.Stock(_i.GetObjectByName("Mini Garuda Indonesia"));
            sov.SOValidation36b();
            ItemDb.Stock(_i.GetObjectByName("Buku Tulis Kiky A5"));
            ItemDb.Stock(_i.GetObjectByName("Mini Garuda Indonesia"));
            sov.SOValidation36a();

            p.wait(2);

            DOValidation dov = new DOValidation(new DeliveryOrderValidator(), new DeliveryOrderDetailValidator(), this._c, this._i, this._sm,
                               this._po, this._pr, this._so, this._do,
                               this._pod, this._prd, this._sod, this._dod);
            dov.DOValidation37();
            dov.DOValidation38(sodtest1);
            dov.DOValidation39();
            dov.DOValidation40();
            
            p.wait(2);
            
            dov.DOValidation41(sodtest4);
            dov.DOValidation42(sodtest1);
            dov.DOValidation43(sodtest4);
            dov.DOValidation44a();

            p.wait(2);

            ItemDb.Stock(_i.GetObjectByName("Buku Tulis Kiky A5"));
            ItemDb.Stock(_i.GetObjectByName("Mini Garuda Indonesia"));

            sov.SOValidation45();
            dov.DOValidation44b();
            ItemDb.Stock(_i.GetObjectByName("Buku Tulis Kiky A5"));
            ItemDb.Stock(_i.GetObjectByName("Mini Garuda Indonesia"));
            p.wait(2);
        }

        public void wait(int second)
        {
            Thread.Sleep(second * 1000);
        }

        public void flushdb(StockControlEntities db)
        {
            ContactDb.Delete(db, _c);
            ItemDb.Delete(db, _i);
            PurchaseOrderDb.Delete(db, _po, _pod);
            PurchaseReceivalDb.Delete(db, _pr, _prd);
            SalesOrderDb.Delete(db, _so, _sod);
            DeliveryOrderDb.Delete(db, _do, _dod);
            StockMutationDb.Delete(db, _sm);
            Console.WriteLine("[Clean] Database is clean");
        }
    }
}
