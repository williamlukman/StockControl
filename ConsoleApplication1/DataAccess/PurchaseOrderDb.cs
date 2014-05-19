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
        public static PurchaseOrder CreatePO(StockControlEntities db, IPurchaseOrderService _pos, IContactService _cs, int custid)
        {
            // Fill DB
            PurchaseOrder po = new PurchaseOrder
            {
                CustomerId = custid,
                PurchaseDate = DateTime.Today,
                IsConfirmed = false,
                IsDeleted = false,
                CreatedAt = DateTime.Today
            };
            po = _pos.CreateObject(po);
            po.Id = po.Id;
            return po;
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
