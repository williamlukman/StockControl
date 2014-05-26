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
                // Warning: each function will delete all data in the DB. Use with caution!!!
                // p.CreateDummyData(p, db)
                //p.ValidateContactModel(p, db);
                //p.ValidateItemModel(p, db);

                Console.WriteLine("Press any key to stop...");
                Console.ReadKey();
            }
        }

        /*
        public void CreateDummyData(Program p, StockControlEntities db)
        {
            //cleandb
            p.flushdb(db);
            p.wait(2);
            //createcustomers
            p.initializeContact(db);
            p.wait(2);
            //createproducts
            p.initializeItem(db);
            p.wait(2);
            //po -> pr procedure
            p.scratch2purchasereceivals(db);
            p.wait(2);
            //so -> do procedure
            p.scratch2deliveryorder(db);
        }

        public void ValidateContactModel(Program p, StockControlEntities db)
        {
            p.flushdb(db);
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
            p.flushdb(db);
            Console.WriteLine("[Item Validation Test]");
            ItemValidation iv = new ItemValidation(new ItemValidator(), this._c, this._i, this._sm,
                                           this._po, this._pr, this._so, this._do,
                                           this._pod, this._prd, this._sod, this._dod);
            iv.ItemValidation1();
            iv.ItemValidation2();
            iv.ItemValidation3();
            iv.ItemValidation4();
            iv.ItemValidation5();
        }

        public void wait(int second)
        {
            Thread.Sleep(second * 1000);
        }

        public void initializeContact(StockControlEntities db)
        {
            // Initialize Contact

            Contact person1 = _c.CreateObject("Andy Robinson", "CEO of Gotong Royong");
            Contact person2 = _c.CreateObject("Baby Marshanda", "CEO of Boutique Kencana");
            Contact person3 = _c.CreateObject("Candy Barbara", "Freelancer Designer");
            ContactDb.Display(db, _c);
        }

        public void initializeItem(StockControlEntities db)
        {
            // Initialize Item
            Item item1 = _i.CreateObject("Buku Tulis Kiky", "20x30cm garis-garis lurus Kiky", "BTKIKY001", 300);
            Item item2 = _i.CreateObject("Buku Gambar Kiky", "20x30cm buku gambar polos Kiky", "BGKIKY002", 200);
            Item item3 = _i.CreateObject("Buku Kotak-Kotak Kiky", "20x30cm buku kotak-kotak Kiky", "BKKIKY003", 0);
            ItemDb.Display(db, _i);
        }

        public void scratch2purchasereceivals(StockControlEntities db)
        {
            // Initialize Purchase Order & Details
            PurchaseOrder po1 = _po.CreateObject(_c.GetObjectByName("Andy Robinson").Id, DateTime.Today);
            PurchaseOrderDetail pod1 = _pod.CreateObject(po1.Id, _i.GetObjectBySku("BTKIKY001").Id, 800);
            PurchaseOrderDetail pod2 = _pod.CreateObject(po1.Id, _i.GetObjectByName("Buku Gambar Kiky").Id, 15);
            PurchaseOrderDetail pod3 = _pod.CreateObject(po1.Id, _i.GetObjectByName("Buku Kotak-Kotak Kiky").Id, 500);
            PurchaseOrderDb.Display(db, _po);
            PurchaseOrderDetailDb.Display(db, _pod, po1.Id);
            
            // Confirm Purchase Order & Details & Create Stock Mutations
            po1 = _po.ConfirmObject(po1, _pod, _sm, _i);
            PurchaseOrderDb.Display(db, _po);
            PurchaseOrderDetailDb.Display(db, _pod, po1.Id);
            StockMutationDb.Display(db, _sm);

            // Initialize Purchase Receival & Details
            PurchaseReceival pr1 = _pr.CreateObject(_c.GetObjectByName("Andy Robinson").Id, DateTime.Today);
            PurchaseReceivalDetail prd1 = _prd.CreateObject(pr1.Id, _i.GetObjectBySku("BTKIKY001").Id, 800, pod1.Id);
            PurchaseReceivalDetail prd2 = _prd.CreateObject(pr1.Id, _i.GetObjectByName("Buku Gambar Kiky").Id, 15, pod2.Id);
            PurchaseReceivalDetail prd3 = _prd.CreateObject(pr1.Id, _i.GetObjectByName("Buku Kotak-Kotak Kiky").Id, 500, pod3.Id);
            PurchaseReceivalDb.Display(db, _pr);
            PurchaseReceivalDetailDb.Display(db, _prd, pr1.Id);

            // Confirm Purchase Receival PO1
            pr1 = _pr.ConfirmObject(pr1, _prd, _pod, _sm, _i);
            PurchaseReceivalDb.Display(db, _pr);
            PurchaseReceivalDetailDb.Display(db, _prd, pr1.Id);
            StockMutationDb.Display(db, _sm);
            Console.WriteLine("Scratch to PRDS success");
        }

        public void scratch2deliveryorder(StockControlEntities db)
        {
            // Initialize Sales Order & Details
            SalesOrder so1 = _so.CreateObject(_c.GetObjectByName("Andy Robinson").Id, DateTime.Today);
            SalesOrderDetail sod1 = _sod.CreateObject(so1.Id, _i.GetObjectBySku("BTKIKY001").Id, 800);
            SalesOrderDetail sod2 = _sod.CreateObject(so1.Id, _i.GetObjectByName("Buku Gambar Kiky").Id, 15);
            SalesOrderDetail sod3 = _sod.CreateObject(so1.Id, _i.GetObjectByName("Buku Kotak-Kotak Kiky").Id, 500);
            SalesOrderDb.Display(db, _so);
            SalesOrderDetailDb.Display(db, _sod, so1.Id);

            // Confirm Sales Order & Details
            so1 = _so.ConfirmObject(so1, _sod, _sm, _i);
            SalesOrderDb.Display(db, _so);
            SalesOrderDetailDb.Display(db, _sod, so1.Id);
            StockMutationDb.Display(db, _sm);

            // Initialize Delivery Order & Details
            DeliveryOrder do1 = _do.CreateObject(_c.GetObjectByName("Andy Robinson").Id, DateTime.Today);
            DeliveryOrderDetail dod1 = _dod.CreateObject(do1.Id, _i.GetObjectBySku("BTKIKY001").Id, 800, sod1.Id);
            DeliveryOrderDetail dod2 = _dod.CreateObject(do1.Id, _i.GetObjectByName("Buku Gambar Kiky").Id, 15, sod2.Id);
            DeliveryOrderDetail dod3 = _dod.CreateObject(do1.Id, _i.GetObjectByName("Buku Kotak-Kotak Kiky").Id, 500, sod3.Id);
            DeliveryOrderDb.Display(db, _do);
            DeliveryOrderDetailDb.Display(db, _dod, do1.Id);

            // Confirm Purchase Receival PO1 & Details & Create Stock Mutations
            do1 = _do.ConfirmObject(do1, _dod, _sod, _sm, _i);
            DeliveryOrderDb.Display(db, _do);
            DeliveryOrderDetailDb.Display(db, _dod, do1.Id);
            StockMutationDb.Display(db, _sm);

            Console.WriteLine("Scratch to DODS success");

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
            Console.WriteLine("Database is clean");
        }
         * */
    }
}
