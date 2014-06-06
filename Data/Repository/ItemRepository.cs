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
            return FindAll().ToList();
        }

        public Item GetObjectById(int Id)
        {
            Item item = Find(i => i.Id == Id && !i.IsDeleted);
            if (item != null) { item.Errors = new Dictionary<string, string>(); }
            return item;
        }

        public Item GetObjectBySku(string Sku)
        {
            Item item = Find(i => i.Sku == Sku);
            if (item != null) { item.Errors = new Dictionary<string, string>(); }
            return item;
        }

        public Item CreateObject(Item item)
        {
            item.PendingDelivery = 0;
            item.PendingReceival = 0;
            item.Ready = 0;
            item.AvgCost = 0;
            item.IsDeleted = false;
            item.CreatedAt = DateTime.Now;
            return Create(item);
        }

        public Item UpdateObject(Item item)
        {
            item.ModifiedAt = DateTime.Now;
            Update(item);
            return item;
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

        public decimal CalculateAvgCost(Item item, int addedQuantity, decimal addedAvgCost)
        {
            int originalQuantity = item.Ready;
            decimal originalAvgCost = item.AvgCost;
            decimal avgCost = (originalQuantity + addedQuantity == 0) ? 0 :            
                ((originalQuantity * originalAvgCost) + (addedQuantity * addedAvgCost)) / (originalQuantity + addedQuantity);
            return avgCost;
        }
        
    }
}