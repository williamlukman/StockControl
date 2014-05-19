using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Service
{
    public interface IItemService
    {
            public IList<Item> GetAll();	
            public Item GetObjectById(int Id);
            public Item GetObjectBySku(string Sku);
            public Item CreateObject (Item item);
            public Item UpdateObject(Item item);

            public Item SoftDeleteObject(Item item);
            public Item ConfirmObject( Item item);
            public bool DeleteObject(int Id);	
    }
}