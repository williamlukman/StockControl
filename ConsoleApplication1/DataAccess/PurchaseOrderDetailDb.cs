using Core.DomainModel;
using Core.Interface.Repository;
using Core.Interface.Service;
using Data.Context;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.DataAccess
{
    class PurchaseOrderDetailDb
    {

        public static void Display(StockControlEntities db, IPurchaseOrderDetailService _pod, int purchaseOrderId)
        {
            var purchaseOrderDetails = _pod.GetObjectsByPurchaseOrderId(purchaseOrderId);
            Console.WriteLine("All purchaseOrderDetails in the database:");

            foreach (var item in purchaseOrderDetails)
            {
                Console.WriteLine("POD ID: " + item.Id + ", Item Id: " + item.ItemId + ", Quantity:" + item.Quantity );
            }
            Console.WriteLine();
        }
    }
}
