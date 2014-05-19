using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Repository
{
    public interface IItemRepository : IRepository<Item>
    {
        public IList<Item> GetAll();
        public Item GetObjectById(int Id);
        public Item GetObjectBySku(string Sku);
        public Item CreateObject (Item item);
        public Item UpdateObject(Item item);
        public Item SoftDeleteObject(Item item);
        public bool DeleteObject(int Id);

    }
}