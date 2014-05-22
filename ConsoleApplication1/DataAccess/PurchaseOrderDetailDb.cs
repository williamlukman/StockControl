using Core.DomainModel;
using Core.Interface.Service;
using Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.DataAccess
{
    class PurchaseOrderDetailDb
    {
        public static PurchaseOrderDetail CreatePOD(StockControlEntities db, IPurchaseOrderDetailService _pods, int PurchaseOrderId, int ItemId, int quantity)
        {

            // Fill DB
            PurchaseOrderDetail pod = new PurchaseOrderDetail
            {
                PurchaseOrderId = PurchaseOrderId,
                ItemId = ItemId,
                Quantity = quantity,
            };
            pod = _pods.CreateObject(pod);
            pod.Id = pod.Id;
            return pod;
        }

        public static void Delete(StockControlEntities db, IPurchaseOrderService _c)
        {
            var purchaseOrders = _c.GetAll();
            Console.WriteLine("Delete all " + purchaseOrders.Count() + " previous purchaseOrders");

            foreach (var item in purchaseOrders)
            {
                _c.DeleteObject(item.Id);
            }
        }

        public static void Display(StockControlEntities db, IPurchaseOrderService _c)
        {
            var purchaseOrders = _c.GetAll();

            Console.WriteLine("All purchaseOrders in the database:");
            foreach (var item in purchaseOrders)
            {
                Console.WriteLine(item.Id);
            }


        }
    }
}
