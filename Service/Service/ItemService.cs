using Core.DomainModel;
using Core.Interface.Repository;
using Core.Interface.Service;
using Core.Interface.Validation;
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
        private IItemRepository _i;
        private IItemValidator _validator;

        public ItemService(IItemRepository _itemRepository, IItemValidator _itemValidator)
        {
            _i = _itemRepository;
            _validator = _itemValidator;
        }

        public IItemValidator GetValidator()
        {
            return _validator;
        }

        public IList<Item> GetAll()
        {
            return _i.GetAll();
        }

        public Item GetObjectById(int Id)
        {
            return _i.GetObjectById(Id);
        }

        public Item GetObjectBySku(string Sku)
        {
            return _i.GetObjectBySku(Sku);
        }

        public Item GetObjectByName(string Name)
        {
            return _i.Find(i => i.Name == Name && !i.IsDeleted);
        }

        public Item CreateObject(Item item)
        {
            return (_validator.ValidCreateObject(item, this) ? _i.CreateObject(item) : item);
        }

        public Item CreateObject(string name, string description, string Sku, int Ready)
        {
            Item item = new Item
            {
                Name = name,
                Description = description,
                Sku = Sku,
                Ready = Ready
            };
            return _i.CreateObject(item);
        }

        public Item UpdateObject(Item item)
        {
            return (_validator.ValidUpdateObject(item, this) ? _i.UpdateObject(item) : item);
        }

        public Item SoftDeleteObject(Item item, IStockMutationService _stockMutationService)
        {
            return (_validator.ValidDeleteObject(item, _stockMutationService) ? _i.SoftDeleteObject(item) : item);
        }

        public bool DeleteObject(int Id)
        {
            return _i.DeleteObject(Id);
        }

        public bool IsSkuDuplicated(String sku)
        {
            IQueryable<Item> item = _i.FindAll(i => i.Sku == sku && !i.IsDeleted);
            return (item.Count() > 1 ? true : false);
        }
    }
}