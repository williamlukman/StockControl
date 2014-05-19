using Core.DomainModel;
using Core.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Context;
using Data.Repository;

namespace Data.Repository
{
    class ItemRepository : EfRepository<Item>, IItemRepository
    {

        private StockControlEntities stocks;
        public ItemRepository()
        {
            stocks = new StockControlEntities();
        }

        IList<Item> GetAll()
        {
            List<Item> items = (from i in stocks.items
                                select i).ToList();

            return items;
        }

        Item GetObjectById(int Id)
        {
            Item item = (from i in stocks.items
                         where i.Id == Id
                         select i).FirstOrDefault();
            return item;
        }

        Item GetObjectBySku(string Sku)
        {
            Item item = (from i in stocks.items
                         where i.Sku == Sku
                         select i).FirstOrDefault();
            return item;
        }

        Item CreateObject(Item item)
        {
            Item newitem = new Item();
            newitem.Sku = item.Sku;
            newitem.Name = item.Name;
            newitem.Description = item.Description;
            newitem.IsDeleted = false;
            newitem.CreatedAt = DateTime.Now;

            return Create(newitem);
        }
        Item UpdateObject(Item item);
        Item SoftDeleteObject(Item item);
        bool DeleteObject(int Id);

    }
}