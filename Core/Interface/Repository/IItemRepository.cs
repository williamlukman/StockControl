using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Repository
{
    interface IItemRepository : IRepository<Item>
    {
        IList<Item> GetAll();
        Item GetObjectById(int Id);
        Item GetObjectBySku(string Sku);
        Item CreateObject (Item item);
        Item UpdateObject(Item item);
        Item SoftDeleteObject(Item item);
        bool DeleteObject(int Id);

    }
}