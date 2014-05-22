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

        public static void Delete(StockControlEntities db, IPurchaseReceivalService _pr, IPurchaseReceivalDetailService _prd)
        {
            var purchaseReceivals = _pr.GetAll();
            Console.WriteLine("Delete all " + purchaseReceivals.Count() + " previous purchaseReceivals and its purchaseReceivalDetails");

            foreach (var item in purchaseReceivals)
            {
                _pr.DeleteObject(item.Id);
                var purchaseReceivalDetails = _prd.GetObjectsByPurchaseReceivalId(item.Id);
                foreach (var detailitem in purchaseReceivalDetails)
                {
                    _prd.DeleteObject(detailitem.Id);
                }
            }
        }

        public static void Display(StockControlEntities db, IPurchaseReceivalService _pr)
        {
            var purchaseReceivals = _pr.GetAll();

            Console.WriteLine("All purchaseReceivals in the database:");
            foreach (var item in purchaseReceivals)
            {
                Console.WriteLine("PR ID: " + item.Id + ", Customer: " + item.CustomerId + ", Date:" + item.ReceivalDate);
            }
            Console.WriteLine();
        }
    }
}
