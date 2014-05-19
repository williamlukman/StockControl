using Core.DomainModel;
using Core.Interface.Repository;
using Core.Interface.Service;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class ItemService : IItemService
    {
        public IList<Item> GetAll(IItemRepository _i)
        {
            return _i.GetAll();
        }

        public Item GetObjectById(int Id, IItemRepository _i)
        {
            return _i.GetObjectById(Id);
        }

        public Item GetObjectBySku(string Sku, IItemRepository _i)
        {
            return _i.GetObjectBySku(Sku);
        }

        public Item CreateObject(Item item, IItemRepository _i)
        {
            return _i.CreateObject(item);
        }

        public Item UpdateObject(Item item, IItemRepository _i)
        {
            return _i.UpdateObject(item);
        }

        public Item SoftDeleteObject(Item item, IItemRepository _i)
        {
            return _i.SoftDeleteObject(item);
        }

        public bool DeleteObject(int Id, IItemRepository _i)
        {
            return _i.DeleteObject(Id);
        }
    }
}