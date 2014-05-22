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
            Console.WriteLine();
        }
    }
}
