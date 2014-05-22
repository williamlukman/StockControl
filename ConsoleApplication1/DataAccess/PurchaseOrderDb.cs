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
    class PurchaseOrderDb
    {
        public static void Delete(StockControlEntities db, IPurchaseOrderService _po, IPurchaseOrderDetailService _pod)
        {
            var purchaseOrders = _po.GetAll();
            Console.WriteLine("Delete all " + purchaseOrders.Count() + " previous purchaseOrders and its purchaseOrderDetails");

            foreach (var item in purchaseOrders)
            {
                _po.DeleteObject(item.Id);
                var purchaseOrderDetails = _pod.GetObjectsByPurchaseOrderId(item.Id);
                foreach (var detailitem in purchaseOrderDetails)
                {
                    _pod.DeleteObject(detailitem.Id);
                }
            }
        }

        public static void Display(StockControlEntities db, IPurchaseOrderService _po)
        {
            var purchaseOrders = _po.GetAll();

            Console.WriteLine("All purchaseOrders in the database:");
            foreach (var item in purchaseOrders)
            {
                Console.WriteLine("PO ID: " + item.Id + ", PO Purchase Date: " + item.PurchaseDate);
            }
        }
    }
}
