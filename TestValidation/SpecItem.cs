using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DomainModel;
using NSpec;
using Service.Service;
using Core.Interface.Service;
using Data.Context;
using System.Data.Entity;
using Data.Repository;
using Validation.Validation;

namespace TestValidation
{

    public class SpecItem: nspec
    {
        Item item;
        IItemService _itemService;
        IContactService _contactService;
        IPurchaseOrderService _purchaseOrderService;
        IPurchaseOrderDetailService _purchaseOrderDetailService;
        IStockMutationService _stockMutationService;
        
        void before_each()
        {
            var db = new StockControlEntities();
            using (db)
            {
                db.DeleteAllTables();
                _itemService = new ItemService(new ItemRepository(), new ItemValidator());
                _contactService = new ContactService(new ContactRepository(), new ContactValidator());
                _purchaseOrderService = new PurchaseOrderService(new PurchaseOrderRepository(), new PurchaseOrderValidator());
                _purchaseOrderDetailService = new PurchaseOrderDetailService(new PurchaseOrderDetailRepository(), new PurchaseOrderDetailValidator());
                _stockMutationService = new StockMutationService(new StockMutationRepository(), new StockMutationValidator());
                item = _itemService.CreateObject("Beras_Katul", "Kotor", "BKT001");
            }
        }

        /*
         * STEPS:
         * 1. Create valid item
         * 2. Create invalid item with no name
         * 3. Create invalid items with same SKU
         * 4a. Delete item
         * 4b. Delete item with stock mutations
         */
        void contact_validation()
        {
        
            it["validates_item"] = () =>
            {
                item.Errors.Count().should_be(0);
            };


            it["create_invalid_item_no_name"] = () =>
            {
                Item item2 = _itemService.CreateObject("   ", "No name", "NN001");
                item2.Errors.Count().should_not_be(0);
            };

            it["create_invalid_item_same_skus"] = () =>
            {
                Item item2 = _itemService.CreateObject("Babibukataku", "Tirutiru Sku", "BKT001");
                item2.Errors.Count().should_not_be(0);
            };

            it["delete_item"] = () =>
            {
                item = _itemService.SoftDeleteObject(item, _stockMutationService);
                item.Errors.Count().should_be(0);
            };

            it["delete_item_with_stockmutation"] = () =>
            {
                Contact contact = _contactService.CreateObject("Bpk. Presiden", "Istana Negara");
                PurchaseOrder purchaseOrder = _purchaseOrderService.CreateObject(contact.Id, DateTime.Now, _contactService);
                PurchaseOrderDetail purchaseOrderDetail = _purchaseOrderDetailService.CreateObject(purchaseOrder.Id, item.Id, 50, 500000, _purchaseOrderService, _itemService);
                purchaseOrder = _purchaseOrderService.ConfirmObject(purchaseOrder, _purchaseOrderDetailService, _stockMutationService, _itemService);
                item = _itemService.SoftDeleteObject(item, _stockMutationService);
                item.Errors.Count().should_not_be(0);
            };
        }
    }
}