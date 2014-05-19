using Core.DomainModel;
using Core.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Service
{
    public interface IItemService
    {
        public IList<Item> GetAll(IItemRepository _i);
        public Item GetObjectById(int Id, IItemRepository _i);
        public Item GetObjectBySku(string Sku, IItemRepository _i);
        public Item CreateObject(Item item, IItemRepository _i);
        public Item UpdateObject(Item item, IItemRepository _i);

        public Item SoftDeleteObject(Item item, IItemRepository _i);
        public bool DeleteObject(int Id, IItemRepository _i);	
    }
}