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
    class PurchaseReceivalDetailDb
    {
        public static PurchaseReceivalDetail CreatePRD(StockControlEntities db, IPurchaseReceivalDetailService _prds, int PurchaseReceivalId, int ItemId, int quantity, int purchaseOrderDetailId)
        {

            // Fill DB
            PurchaseReceivalDetail prd = new PurchaseReceivalDetail
            {
                PurchaseReceivalId = PurchaseReceivalId,
                ItemId = ItemId,
                Quantity = quantity,
                PurchaseOrderDetailId = purchaseOrderDetailId
            };
            prd = _prds.CreateObject(prd);
            prd.Id = prd.Id;
            return prd;
        }

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
                Console.WriteLine(item.Id);
            }


        }
    }
}
