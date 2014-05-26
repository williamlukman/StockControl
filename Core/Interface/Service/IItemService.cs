using Core.DomainModel;
using Core.Interface.Repository;
using Core.Interface.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Service
{
    public interface IItemService
    {
        IItemValidator GetValidator();
        IList<Item> GetAll();
        Item GetObjectById(int Id);
        Item GetObjectBySku(string Sku);
        Item GetObjectByName(string Name);
        Item CreateObject(Item item);
        Item CreateObject(string Name, string Description, string Sku, int Ready);
        Item UpdateObject(Item item);

        Item SoftDeleteObject(Item item, IStockMutationService _stockMutationService);
        bool DeleteObject(int Id);
        bool IsSkuDuplicated(String sku);
    }
}