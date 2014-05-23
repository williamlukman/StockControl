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
    class DeliveryOrderDb
    {

        public static void Delete(StockControlEntities db, IDeliveryOrderService _do, IDeliveryOrderDetailService _dod)
        {
            var deliveryOrders = _do.GetAll();

            foreach (var item in deliveryOrders)
            {
                _do.DeleteObject(item.Id);
                var deliveryOrderDetails = _dod.GetObjectsByDeliveryOrderId(item.Id);
                foreach (var detailitem in deliveryOrderDetails)
                {
                    _dod.DeleteObject(detailitem.Id);
                }

            }
        }

        public static void Display(StockControlEntities db, IDeliveryOrderService _do)
        {
            var deliveryOrders = _do.GetAll();

            Console.WriteLine("All deliveryOrders in the database:");
            foreach (var item in deliveryOrders)
            {
                Console.WriteLine("DO ID: " + item.Id + ", Customer: " + item.CustomerId + ", Date:" + item.DeliveryDate);
            }
            Console.WriteLine();
        }
    }
}
