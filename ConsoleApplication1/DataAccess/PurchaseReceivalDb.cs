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
    class PurchaseReceivalDb
    {

        public static void Delete(StockControlEntities db, IPurchaseReceivalService _c)
        {
            var purchaseReceivals = _c.GetAll();
            Console.WriteLine("Delete all " + purchaseReceivals.Count() + " previous purchaseReceivals");

            foreach (var item in purchaseReceivals)
            {
                _c.DeleteObject(item.Id);
            }
        }

        public static void Display(StockControlEntities db, IPurchaseReceivalService _c)
        {
            var purchaseReceivals = _c.GetAll();

            Console.WriteLine("All purchaseReceivals in the database:");
            foreach (var item in purchaseReceivals)
            {
                Console.WriteLine("PR ID: " + item.Id + ", Customer: " + item.CustomerId + ", Date:" + item.ReceivalDate);
            }
            Console.WriteLine();
        }
    }
}
