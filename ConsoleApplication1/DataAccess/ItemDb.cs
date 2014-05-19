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
    class ItemDb
    {
        public static Item CreateItem1(StockControlEntities db, IItemService _i)
        {
            // Fill DB
            Item Item1 = new Item
            {
                Name = "Buku Tulis",
                Sku = "33212",
                Description = "New Item",
                IsDeleted = false,
                CreatedAt = DateTime.Now
            };
            Item1 = _i.CreateObject(Item1);
            Item1.Id = Item1.Id;
            return Item1;
        }
        
        public static Item CreateItem2(StockControlEntities db, IItemService _i)
        {
            Item Item2 = new Item
            {
                Name = "Copy Buku Tulis",
                Sku = "33212",
                Description = "I am new item to the system too",
                IsDeleted = false,
                CreatedAt = DateTime.Now
            };
            Item2 = _i.CreateObject(Item2);
            Item2.Id = Item2.Id;
            return Item2;
        }

        public static void Delete(StockControlEntities db, IItemService _i)
        {
            var items = _i.GetAll();
            Console.WriteLine("Delete all " + items.Count() + " previous items");

            foreach (var item in items)
            {
                _i.DeleteObject(item.Id);
            }
        }

        public static void Display(StockControlEntities db, IItemService _i)
        {
            var items = _i.GetAll();

            Console.WriteLine("All items in the database:");
            foreach (var item in items)
            {
                Console.WriteLine(item.Name);
            }
        }
    }
}
