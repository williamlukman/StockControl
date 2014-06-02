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
        private IStockAdjustmentService _sa;
        private IStockAdjustmentDetailService _sad;

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
            _sa = new StockAdjustmentService(new StockAdjustmentRepository(), new StockAdjustmentValidator());
            _sad = new StockAdjustmentDetailService(new StockAdjustmentDetailRepository(), new StockAdjustmentDetailValidator());
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
                p.ValidateContactModel(p, db);
                p.ValidateItemModel(p, db);
                p.ValidateStockAdjustmentModel(p, db);
                p.ValidateReceivalModel(p, db);
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
            Console.WriteLine();
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

        public void ValidateStockAdjustmentModel(Program p, StockControlEntities db)
        {
            Console.WriteLine();
            Console.WriteLine("[Stock Adjustment Test]");
            SAValidation sa = new SAValidation(new StockAdjustmentValidator(), new StockAdjustmentDetailValidator(),
                                           this._c, this._i, this._sm,
                                           this._po, this._pr, this._so, this._do,
                                           this._pod, this._prd, this._sod, this._dod,
                                           this._sa, this._sad);
            int stockAdjustmentId = sa.SAValidation1();
            sa.SAValidation2(stockAdjustmentId);
            sa.SAValidation3(stockAdjustmentId);
            sa.SAValidation4(stockAdjustmentId);
            int stockAdjustmentId2 = sa.SAValidation5();
            sa.SAValidation6(stockAdjustmentId2);
            sa.SAValidation7(stockAdjustmentId2);
            sa.SAValidation8(stockAdjustmentId2);

            // Reconfirm item to next reduce the item in SAValidation8
            sa.SAValidation3(stockAdjustmentId);
            sa.SAValidation9(stockAdjustmentId2);

        }

        public void ValidateReceivalModel(Program p, StockControlEntities db)
        {
            Console.WriteLine();
            Console.WriteLine("[Puchase Order Validation Test]");
            POValidation pov = new POValidation(new PurchaseOrderValidator(), new PurchaseOrderDetailValidator(), this._c, this._i, this._sm,
                                           this._po, this._pr, this._so, this._do,
                                           this._pod, this._prd, this._sod, this._dod);
            pov.POValidation1();
            int podtest1 = pov.POValidation2();
            pov.POValidation3();
            pov.POValidation4();
            int podtest2 = pov.POValidation5();
            int podtest3 = pov.POValidation6();
            int podtest4 = pov.POValidation7();
            pov.POValidation8();
            pov.POValidation9();
            pov.POValidation8();

            Console.WriteLine();
            Console.WriteLine("[Purchase Receival Validation Test]");

            PRValidation prv = new PRValidation(new PurchaseReceivalValidator(), new PurchaseReceivalDetailValidator(), this._c, this._i, this._sm,
                               this._po, this._pr, this._so, this._do,
                               this._pod, this._prd, this._sod, this._dod);
            prv.PRValidation1();
            prv.PRValidation2(podtest1);
            prv.PRValidation3();
            prv.PRValidation4();
            prv.PRValidation5(podtest4);
            prv.PRValidation6(podtest1);
            prv.PRValidation7(podtest4);
            prv.PRValidation8();

            pov.POValidation9();
            
            prv.PRValidation9();
            prv.PRValidation8();
        }

        public void ValidateDeliveryModel(Program p, StockControlEntities db)
        {
            Console.WriteLine();
            Console.WriteLine("[Sales Order Validation Test]");
            SOValidation sov = new SOValidation(new SalesOrderValidator(), new SalesOrderDetailValidator(), this._c, this._i, this._sm,
                                           this._po, this._pr, this._so, this._do,
                                           this._pod, this._prd, this._sod, this._dod);
            sov.SOValidation1();
            int sodtest1 = sov.SOValidation2();
            sov.SOValidation3();
            sov.SOValidation4();
            
            int sodtest2 = sov.SOValidation5();
            int sodtest3 = sov.SOValidation6();
            int sodtest4 = sov.SOValidation7();
            sov.SOValidation8();

            sov.SOValidation9();
            sov.SOValidation10();

            Console.WriteLine();
            Console.WriteLine("[Purchase Order Validation Test]");

            DOValidation dov = new DOValidation(new DeliveryOrderValidator(), new DeliveryOrderDetailValidator(), this._c, this._i, this._sm,
                               this._po, this._pr, this._so, this._do,
                               this._pod, this._prd, this._sod, this._dod);
            dov.DOValidation1();
            dov.DOValidation2(sodtest1);
            dov.DOValidation3();
            dov.DOValidation4();
            dov.DOValidation5(sodtest4);
            dov.DOValidation6(sodtest1);
            dov.DOValidation7(sodtest4);
            dov.DOValidation8();

            sov.SOValidation10();
            dov.DOValidation9();
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
            StockAdjustmentDb.Delete(db, _sa, _sad);
            Console.WriteLine("[Clean] Database is clean");
        }
    }
}
