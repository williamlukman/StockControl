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
    class StockAdjustmentDb
    {

        public static void Delete(StockControlEntities db, IStockAdjustmentService _sa, IStockAdjustmentDetailService _sad)
        {
            var stockAdjustments = _sa.GetAll();

            foreach (var item in stockAdjustments)
            {
                _sa.DeleteObject(item.Id);
                var stockAdjustmentDetails = _sad.GetObjectsByStockAdjustmentId(item.Id);
                foreach (var detailitem in stockAdjustmentDetails)
                {
                    _sad.DeleteObject(detailitem.Id);
                }
            }
        }

        public static void Display(StockControlEntities db, IStockAdjustmentService _sa)
        {
            var purchaseReceivals = _sa.GetAll();

            Console.WriteLine("All stockAdjustments in the database:");
            foreach (var item in purchaseReceivals)
            {
                Console.WriteLine("SA ID: " + item.Id + ", Date:" + item.AdjustmentDate);
            }
            Console.WriteLine();
        }
    }
}
