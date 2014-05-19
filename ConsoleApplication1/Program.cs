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
    class Program
    {

            public static void Main(string[] args)
            {
                //Database.SetInitializer<StockControlEntities>(new StockControlInit());
                var db = new StockControlEntities();

                using (db)
                {
                    // Initialize Contact
                    IContactService cs = new ContactService(new ContactRepository());
                    ContactDb.Delete(db, cs);
                    Contact person1 = ContactDb.CreatePerson1(db, cs);
                    Contact person2 = ContactDb.CreatePerson2(db, cs);
                    
                    Console.WriteLine("2 Contacts were created");
                    System.Threading.Thread.Sleep(2000);
                    
                    // Initialize Item
                    IItemService i = new ItemService(new ItemRepository());
                    ItemDb.Delete(db, i);
                    Item item1 = ItemDb.CreateItem1(db, i);
                    Item item2 = ItemDb.CreateItem2(db, i);
                    ItemDb.Display(db, i);

                    Console.WriteLine("2 Items were created");
                    System.Threading.Thread.Sleep(2000);
                   
                    // Initialize Purchase Order & Details
                    IPurchaseOrderService pos = new PurchaseOrderService(new PurchaseOrderRepository());
                    PurchaseOrder po1 = PurchaseOrderDb.CreatePO(db, pos, cs, person1.Id);
                    PurchaseOrder po2 = PurchaseOrderDb.CreatePO(db, pos, cs, person2.Id);

                    Console.WriteLine("2 POs were created");
                    System.Threading.Thread.Sleep(2000);
                    
                    IPurchaseOrderDetailService pods = new PurchaseOrderDetailService(new PurchaseOrderDetailRepository());
                    PurchaseOrderDetail pod1a = PurchaseOrderDetailDb.CreatePOD(db, pods, po1.Id, item1.Id, 80);
                    PurchaseOrderDetail pod1b = PurchaseOrderDetailDb.CreatePOD(db, pods, po1.Id, item2.Id, 100);
                    PurchaseOrderDetail pod2a = PurchaseOrderDetailDb.CreatePOD(db, pods, po2.Id, item1.Id, 20);
                    PurchaseOrderDetail pod2b = PurchaseOrderDetailDb.CreatePOD(db, pods, po2.Id, item2.Id, 12);

                    Console.WriteLine("4 PODs were created");
                    System.Threading.Thread.Sleep(2000);
                   
                    // Confirm Purchase Order PO1
                    po1 = pos.ConfirmObject(po1);
                    pod1a = pods.ConfirmObject(pod1a);
                    pod1b = pods.ConfirmObject(pod1b);
                    IStockMutationService sms = new StockMutationService(new StockMutationRepository());
                    sms.CreateStockMutationForPurchaseOrder(pod1a, item1);
                    sms.CreateStockMutationForPurchaseOrder(pod1b, item2);

                    Console.WriteLine("PO1 Confirmation = " + po1.IsConfirmed);
                    Console.WriteLine("POD1a Confirmation = " + pod1a.IsConfirmed);
                    Console.WriteLine("POD1b Confirmation = " + pod1b.IsConfirmed);
                    Console.WriteLine("PO2 Confirmation = " + po2.IsConfirmed);
                    Console.WriteLine("POD2a Confirmation = " + pod2a.IsConfirmed);
                    Console.WriteLine("POD2b Confirmation = " + pod2b.IsConfirmed);

                    Console.WriteLine("Press any key to exit...");
                    Console.ReadKey();
            }
        }
    }
}
