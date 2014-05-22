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
        Program()
        {
            _c = new ContactService(new ContactRepository());
            _i = new ItemService(new ItemRepository());
            _po = new PurchaseOrderService(new PurchaseOrderRepository());
            _pod = new PurchaseOrderDetailService(new PurchaseOrderDetailRepository());
            _pr = new PurchaseReceivalService(new PurchaseReceivalRepository());
            _prd = new PurchaseReceivalDetailService(new PurchaseReceivalDetailRepository());
            _so = new SalesOrderService(new SalesOrderRepository());
            _sod = new SalesOrderDetailService(new SalesOrderDetailRepository());
            _do = new DeliveryOrderService(new DeliveryOrderRepository());
            _dod = new DeliveryOrderDetailService(new DeliveryOrderDetailRepository());
            _sm = new StockMutationService(new StockMutationRepository());
        }

        public static void Main(string[] args)
        {
            //Database.SetInitializer<StockControlEntities>(new StockControlInit());
            var db = new StockControlEntities();

            using (db)
            {
                Program p = new Program();
                p.flushdb(db);
                p.scratch2purchasereceivals(db);
                //p.scratch2deliveryorder(db);

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }

        public void scratch2purchasereceivals(StockControlEntities db)
        {
            // Initialize Contact
            Contact person1 = _c.CreateObject("Andy Robinson", "CEO of Gotong Royong");
            Contact person2 = _c.CreateObject("Baby Marshanda", "CEO of Boutique Kencana");
            Contact person3 = _c.CreateObject("Candy Barbara", "Freelancer Designer");
            ContactDb.Display(db, _c);

            // Initialize Item
            Item item1 = _i.CreateObject("Buku Tulis Kiky", "20x30cm garis-garis lurus Kiky", "BTKIKY001");
            Item item2 = _i.CreateObject("Buku Gambar Kiky", "20x30cm buku gambar polos Kiky", "BGKIKY002");
            Item item3 = _i.CreateObject("Buku Kotak-Kotak Kiky", "20x30cm buku kotak-kotak Kiky", "BKKIKY003");
            ItemDb.Display(db, _i);

            // Initialize Purchase Order & Details
            PurchaseOrder po1 = _po.CreateObject(_c.GetObjectByName("Andy Robinson").Id, DateTime.Today);
            PurchaseOrderDetail pod1 = _pod.CreateObject(po1.Id, _i.GetObjectBySku("BTKIKY001").Id, 800);
            PurchaseOrderDetail pod2 = _pod.CreateObject(po1.Id, _i.GetObjectByName("Buku Gambar Kiky").Id, 15);
            PurchaseOrderDetail pod3 = _pod.CreateObject(po1.Id, item3.Id, 500);
            PurchaseOrderDb.Display(db, _po);
            PurchaseOrderDetailDb.Display(db, _pod, po1.Id);
            
            // Confirm Purchase Order & Details
            po1 = _po.ConfirmObject(po1);
            pod1 = _pod.ConfirmObject(pod1);
            pod2 = _pod.ConfirmObject(pod2);
            pod3 = _pod.ConfirmObject(pod3);
            PurchaseOrderDb.Display(db, _po);
            PurchaseOrderDetailDb.Display(db, _pod, po1.Id);

            // Create Stock Mutations
            StockMutation sm1 = _sm.CreateStockMutationForPurchaseOrder(pod1, _i.GetObjectBySku("BTKIKY001"));
            StockMutation sm2 = _sm.CreateStockMutationForPurchaseOrder(pod2, _i.GetObjectByName("Buku Gambar Kiky"));
            StockMutation sm3 = _sm.CreateStockMutationForPurchaseOrder(pod2, item3);
            StockMutationDb.Display(db, _sm);

            // Initialize Purchase Receival & Details
            PurchaseReceival pr1 = _pr.CreateObject(_c.GetObjectByName("Andy Robinson").Id, DateTime.Today);
            PurchaseReceivalDetail prd1 = _prd.CreateObject(pr1.Id, _i.GetObjectBySku("BTKIKY001").Id, 800, pod1.Id);
            PurchaseReceivalDetail prd2 = _prd.CreateObject(pr1.Id, _i.GetObjectByName("Buku Gambar Kiky").Id, 15, pod2.Id);
            PurchaseReceivalDetail prd3 = _prd.CreateObject(pr1.Id, item3.Id, 500, pod3.Id);
            PurchaseReceivalDb.Display(db, _pr);
            PurchaseReceivalDetailDb.Display(db, _prd, pr1.Id);

            // Confirm Purchase Receival PO1
            pr1 = _pr.ConfirmObject(pr1);
            prd1 = _prd.ConfirmObject(prd1);
            prd2 = _prd.ConfirmObject(prd2);
            prd3 = _prd.ConfirmObject(prd3);
            PurchaseReceivalDb.Display(db, _pr);
            PurchaseReceivalDetailDb.Display(db, _prd, pr1.Id);

            IList<StockMutation> sm4 = _sm.CreateStockMutationForPurchaseReceival(prd1, item1);
            IList<StockMutation> sm5 = _sm.CreateStockMutationForPurchaseReceival(prd2, item2);
            IList<StockMutation> sm6 = _sm.CreateStockMutationForPurchaseReceival(prd3, item3);
            StockMutationDb.Display(db, _sm);
            Console.WriteLine("Scratch to PRDS success");
        }

        public void scratch2deliveryorder(StockControlEntities db)
        {
            Console.WriteLine("Scratch to DODS success");

        }

        public void flushdb(StockControlEntities db)
        {
            ContactDb.Delete(db, _c);
            ItemDb.Delete(db, _i);
            PurchaseOrderDb.Delete(db, _po);
            PurchaseOrderDetailDb.Delete(db, _pod);
            PurchaseReceivalDb.Delete(db, _pr);
            PurchaseReceivalDetailDb.Delete(db, _prd);
            StockMutationDb.Delete(db, _sm);
            Console.WriteLine("Database is clean");
        }
    }
}
