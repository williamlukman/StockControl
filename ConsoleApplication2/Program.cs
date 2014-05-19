using Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new StockControlEntities())
            {

                // Display all Blogs from the database 
                var contacts = from c in db.Contacts
                               orderby c.Name
                               select c;

                Console.WriteLine("All contacts in the database:");
                foreach (var item in contacts)
                {
                    Console.WriteLine(item.Name);
                }

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
