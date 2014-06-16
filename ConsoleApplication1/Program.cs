using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Context;
using Core.DomainModel;
using System.Data.Entity;
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
        private ICashBankService _cb;
        private IPurchaseInvoiceService _pi;
        private IPurchaseInvoiceDetailService _pid;
        private IPayableService _payable;
        private IPaymentVoucherService _pv;
        private IPaymentVoucherDetailService _pvd;
        private ISalesInvoiceService _si;
        private ISalesInvoiceDetailService _sid;
        private IReceivableService _receivable;
        private IReceiptVoucherService _rv;
        private IReceiptVoucherDetailService _rvd;

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
            _cb = new CashBankService(new CashBankRepository(), new CashBankValidator());
            _pi = new PurchaseInvoiceService(new PurchaseInvoiceRepository(), new PurchaseInvoiceValidator());
            _pid = new PurchaseInvoiceDetailService(new PurchaseInvoiceDetailRepository(), new PurchaseInvoiceDetailValidator());
            _payable = new PayableService(new PayableRepository(), new PayableValidator());
            _pv = new PaymentVoucherService(new PaymentVoucherRepository(), new PaymentVoucherValidator());
            _pvd = new PaymentVoucherDetailService(new PaymentVoucherDetailRepository(), new PaymentVoucherDetailValidator());
            _si = new SalesInvoiceService(new SalesInvoiceRepository(), new SalesInvoiceValidator());
            _sid = new SalesInvoiceDetailService(new SalesInvoiceDetailRepository(), new SalesInvoiceDetailValidator());
            _receivable = new ReceivableService(new ReceivableRepository(), new ReceivableValidator());
            _rv = new ReceiptVoucherService(new ReceiptVoucherRepository(), new ReceiptVoucherValidator());
            _rvd = new ReceiptVoucherDetailService(new ReceiptVoucherDetailRepository(), new ReceiptVoucherDetailValidator());
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
                
                // Operational Test
                p.ValidateContactModel(p, db);
                p.ValidateItemModel(p, db);
                p.ValidateStockAdjustmentModel(p, db);
                int sampleprdid = p.ValidateReceivalModel(p, db);
                int sampledodid = p.ValidateDeliveryModel(p, db);
                
                // Finance Test
                p.ValidateCashBankModel(p, db);
                int samplepiid = p.ValidatePurchaseInvoiceModel(p, db, sampleprdid);
                p.ValidatePaymentVoucherModel(p, db, samplepiid);
                int samplesiid = p.ValidateSalesInvoiceModel(p, db, sampledodid);
                p.ValidateReceiptVoucherModel(p, db, samplesiid);
 
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
            sa.SAValidation9(stockAdjustmentId2);

            // Reconfirm item to next reduce the item in SAValidation8
            sa.SAValidation3(stockAdjustmentId);
            sa.SAValidation10(stockAdjustmentId2);

        }

        public int ValidateReceivalModel(Program p, StockControlEntities db)
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
            int prdid = prv.PRValidation7(podtest4);
            prv.PRValidation8();

            pov.POValidation9();
            
            prv.PRValidation9();
            prv.PRValidation8();
            return prdid;
        }

        public int ValidateDeliveryModel(Program p, StockControlEntities db)
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
            int sampledodid = dov.DOValidation2(sodtest1);
            dov.DOValidation3();
            dov.DOValidation4();
            dov.DOValidation5(sodtest4);
            dov.DOValidation6(sodtest1);
            dov.DOValidation7(sodtest4);
            dov.DOValidation8();

            sov.SOValidation10();
            dov.DOValidation9();

            return sampledodid;
        }

        public void ValidateCashBankModel(Program p, StockControlEntities db)
        {
            Console.WriteLine("[CashBank Validation Test]");
            CashBankValidation cb = new CashBankValidation(new CashBankValidator(), this._cb);
            cb.CashBankValidation1();
            cb.CashBankValidation2();
            cb.CashBankValidation3();
        }

        public int ValidatePurchaseInvoiceModel(Program p, StockControlEntities db, int sampleprdid)
        {
            Console.WriteLine("[Purchase Invoice Validation Test]");
            PIValidation piv = new PIValidation(new PurchaseInvoiceValidator(), new PurchaseInvoiceDetailValidator(), 
                               this._pi, this._pid, 
                               this._payable,
                               this._c, this._i, this._sm,
                               this._po, this._pr, this._so, this._do,
                               this._pod, this._prd, this._sod, this._dod);
            int piid = piv.PIValidation1();
            int pidid = piv.PIValidation2(piid, sampleprdid);
            piv.PIValidation3(pidid);
            piv.PIValidation4();
            return piid;
        }

        public void ValidatePaymentVoucherModel(Program p, StockControlEntities db, int samplepiid)
        {
            Console.WriteLine("[Payment Voucher Validation Test]");
            PVValidation pvv = new PVValidation(
                               this._pi, this._pid,
                               this._payable, this._pv, this._pvd,
                               this._cb,
                               this._c, this._i, this._sm,
                               this._po, this._pr, this._so, this._do,
                               this._pod, this._prd, this._sod, this._dod);

            int payable1 = pvv.PayableValidation1(samplepiid); // 10jt
            int payable2 = pvv.PayableValidation2(samplepiid); // 5jt
            int pvId1 = pvv.PVValidation1(); // 20jt
            int pvdId1a = pvv.PVValidation2a(pvId1, payable1); // 3jt, Payable1
            int pvdId1b = pvv.PVValidation2b(pvId1, payable2); // 4jt, Payable2
            int pvdId1c = pvv.PVValidation2c(pvId1, payable1); // 4jt, Payable1
            int pvId2 = pvv.PVValidation4(); // 7.5jt
            int pvdId2a = pvv.PVValidation5(pvId2, payable1); // 4jt, Payable 1
            int pvdId2b = pvv.PVValidation6(pvId2, payable2); // 3.5jt, Payable 2
            pvv.PVValidation7(pvId1);
            pvv.PVValidation8(pvId2); // invalid
            pvv.PVValidation9(pvdId1a);
            //pvv.PVValidation10(pvdId2a);
        }

        public int ValidateSalesInvoiceModel(Program p, StockControlEntities db, int sampledodid)
        {
            Console.WriteLine("[Sales Invoice Validation Test]");
            SIValidation siv = new SIValidation(new SalesInvoiceValidator(), new SalesInvoiceDetailValidator(),
                               this._si, this._sid,
                               this._receivable,
                               this._c, this._i, this._sm,
                               this._po, this._pr, this._so, this._do,
                               this._pod, this._prd, this._sod, this._dod);
            int siid = siv.SIValidation1();
            int sidid = siv.SIValidation2(siid, sampledodid);
            siv.SIValidation3(sidid);
            siv.SIValidation4();
            return siid;
        }

        public void ValidateReceiptVoucherModel(Program p, StockControlEntities db, int samplesiid)
        {
            Console.WriteLine("[Receipt Voucher Validation Test]");
            RVValidation rvv = new RVValidation(
                               this._si, this._sid,
                               this._receivable, this._rv, this._rvd,
                               this._cb,
                               this._c, this._i, this._sm,
                               this._po, this._pr, this._so, this._do,
                               this._pod, this._prd, this._sod, this._dod);

            int receivable1 = rvv.ReceivableValidation1(samplesiid); // 10jt
            int receivable2 = rvv.ReceivableValidation2(samplesiid); // 5jt
            int rvId1 = rvv.RVValidation1(); // 20jt
            int rvdId1a = rvv.RVValidation2a(rvId1, receivable1); // 3jt, Payable1
            int rvdId1b = rvv.RVValidation2b(rvId1, receivable2); // 4jt, Payable2
            int rvdId1c = rvv.RVValidation2c(rvId1, receivable1); // 4jt, Payable1
            int rvId2 = rvv.RVValidation4(); // 7.5jt
            int rvdId2a = rvv.RVValidation5(rvId2, receivable1); // 4jt, Payable 1
            int pvdId2b = rvv.RVValidation6(rvId2, receivable2); // 3.5jt, Payable 2
            rvv.RVValidation7(rvId1);
            rvv.RVValidation8(rvId2); // invalid
            rvv.RVValidation9(rvdId1a);
            //pvv.PVValidation10(pvdId2a);
        }

        public void wait(int second)
        {
            Thread.Sleep(second * 1000);
        }

        public void flushdb(StockControlEntities db)
        {
            db.DeleteAllTables();
            Console.WriteLine("[Clean] Database is clean");
        }
    }
}
