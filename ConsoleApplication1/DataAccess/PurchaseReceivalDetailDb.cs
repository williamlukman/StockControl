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
    class PurchaseReceivalDetailDb
    {
        public static void Delete(StockControlEntities db, IPurchaseReceivalDetailService _prdc)
        {
            IPurchaseReceivalDetailRepository _prdrepo = new PurchaseReceivalDetailRepository();
            var purchaseReceivals = _prdrepo.FindAll();
            Console.WriteLine("Delete all " + purchaseReceivals.Count() + " previous purchaseReceivals");

            foreach (var item in purchaseReceivals)
            {
                _prdc.DeleteObject(item.Id);
            }
        }

        public static void Display(StockControlEntities db, IPurchaseReceivalDetailService _prd, int purchaseReceivalId)
        {
            var purchaseReceivals = _prd.GetObjectsByPurchaseReceivalId(purchaseReceivalId);

            Console.WriteLine("All purchaseReceivals in the database:");
            foreach (var item in purchaseReceivals)
            {
                Console.WriteLine("PRD ID: " + item.Id + ", Item: " + item.ItemId + ", Quantity: " + item.Quantity);
            }
            Console.WriteLine();
        }
    }
}
