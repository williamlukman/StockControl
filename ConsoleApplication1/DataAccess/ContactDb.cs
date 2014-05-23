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
    class ContactDb
    {

        public static void Delete(StockControlEntities db, IContactService _c)
        {
            var contacts = _c.GetAll();

            foreach (var item in contacts)
            {
                _c.DeleteObject(item.Id);
            }
        }

        public static void Display(StockControlEntities db, IContactService _c)
        {
            var contacts = _c.GetAll();

            Console.WriteLine("All contacts in the database:");
            int i = 0;
            foreach (var item in contacts)
            {
                Console.WriteLine(i++ + " " + item.Name);
            }
            Console.WriteLine();
        }
    }
}
