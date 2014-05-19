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
        public static Contact CreatePerson1(StockControlEntities db, IContactService _cs)
        {
            // Fill DB
            Contact Person1 = new Contact
            {
                Name = "Andrew Baby Chan",
                Description = "New person",
                IsDeleted = false,
                CreatedAt = DateTime.Now
            };
            Person1 = _cs.CreateObject(Person1);
            Person1.Id = Person1.Id;
            return Person1;
        }

        public static Contact CreatePerson2 (StockControlEntities db, IContactService _cs)
        {
            Contact Person2 = new Contact
            {
                Name = "Dandy Enough Facial",
                Description = "I am new to the system too",
                IsDeleted = false,
                CreatedAt = DateTime.Now
            };
            Person2 = _cs.CreateObject(Person2);
            Person2.Id = Person2.Id;
            return Person2;
        }

        public static void Delete(StockControlEntities db, IContactService _c)
        {
            var contacts = _c.GetAll();
            Console.WriteLine("Delete all " + contacts.Count() + " previous contacts");

            foreach (var item in contacts)
            {
                _c.DeleteObject(item.Id);
            }
        }

        public static void Display(StockControlEntities db, IContactService _c)
        {
            var contacts = _c.GetAll();

            Console.WriteLine("All contacts in the database:");
            foreach (var item in contacts)
            {
                Console.WriteLine(item.Name);
            }


        }
    }
}
