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
    class CashBankDb
    {

        public static void Delete(StockControlEntities db, ICashBankService _cb)
        {
            var cashbanks = _cb.GetAll();

            foreach (var item in cashbanks)
            {
                _cb.DeleteObject(item.Id);
            }
        }

        public static void Display(StockControlEntities db, ICashBankService _cb)
        {
            var cashbanks = _cb.GetAll();

            Console.WriteLine("All cashbanks in the database:");
            int i = 0;
            foreach (var item in cashbanks)
            {
                Console.WriteLine(i++ + " " + item.Name);
            }
            Console.WriteLine();
        }
    }
}
