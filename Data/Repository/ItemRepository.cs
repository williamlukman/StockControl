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
    public class ItemRepository : EfRepository<Item>, IItemRepository
    {

        private StockControlEntities stocks;
        public ItemRepository()
        {
            stocks = new StockControlEntities();
        }

        public IList<Item> GetAll()
        {
            List<Item> items = (from i in stocks.items
                                select i).ToList();

            return items;
        }

        public Item GetObjectById(int Id)
        {
            Item item = (from i in stocks.items
                         where i.Id == Id
                         select i).FirstOrDefault();
            return item;
        }

        public Item GetObjectBySku(string Sku)
        {
            Item item = (from i in stocks.items
                         where i.Sku == Sku
                         select i).FirstOrDefault();
            return item;
        }

        public Item CreateObject(Item item)
        {
            Item newitem = new Item();
            newitem.Sku = item.Sku;
            newitem.Name = item.Name;
            newitem.Description = item.Description;
            newitem.IsDeleted = false;
            newitem.CreatedAt = DateTime.Now;

            return Create(newitem);
        }

        public Item UpdateObject(Item item)
        {
            Item updateitem = new Item();
            updateitem.Sku = item.Sku;
            updateitem.Name = item.Name;
            updateitem.Description = item.Description;
            updateitem.IsDeleted = item.IsDeleted;
            updateitem.ModifiedAt = DateTime.Now;
            Update(updateitem);
            return updateitem;
        }

        public Item SoftDeleteObject(Item item)
        {
            item.IsDeleted = true;
            item.DeletedAt = DateTime.Now;
            Update(item);
            return item;
        }

        public bool DeleteObject(int Id)
        {
            Item item = Find(x => x.Id == Id);
            return (Delete(item) == 1) ? true : false;
        }
    }
}