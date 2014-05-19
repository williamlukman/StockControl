using Core.DomainModel;
using Core.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Context;

namespace Core.Interface.Repository
{
    class ItemRepository : IItemRepository
    {
        IList<Item> GetAll()
        {
            StockControl stocks = new StockControl();

            var items = (from i in stocks.items
                            select i).ToList();

            return items;
        }

        Item GetObjectById(int Id);
        Item GetObjectBySku(string Sku);
        Item CreateObject (Item item);
        Item UpdateObject(Item item);
        Item SoftDeleteObject(Item item);
        bool DeleteObject(int Id);

    }
}