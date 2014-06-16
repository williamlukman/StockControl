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

namespace NSpec.Spec
{

    public class PO : nspec
    {
        Contact contact;
        PurchaseOrderDetail poDetail1;
        PurchaseOrderDetail poDetail2;
        PurchaseOrder newPO;
        Item item1;
        Item item2;
        IItemService itemService;
        IContactService contactService;
        IPurchaseOrderService poService;
        IPurchaseOrderDetailService poDetailService;
        StockMutationService stockMutationService;

        void before_each()
        {
            var db = new StockControlEntities();
            using (db)
            {
                db.DeleteAllTables();
                itemService = new ItemService(new ItemRepository(), new ItemValidator());
                contactService = new ContactService(new ContactRepository(), new ContactValidator());
                poService = new PurchaseOrderService(new PurchaseOrderRepository(), new PurchaseOrderValidator());
                poDetailService = new PurchaseOrderDetailService(new PurchaseOrderDetailRepository(), new PurchaseOrderDetailValidator());
                stockMutationService = new StockMutationService(new StockMutationRepository(), new StockMutationValidator());
                item1 = itemService.CreateObject("Buku Tulis", "Awesome", "1132");
                item2 = itemService.CreateObject("Buku Gambar", "Blabla", "1111");
                contact = contactService.CreateObject("Willy", "baba");
            }
        }

        void po_validation()
        {
            it["prepares_valid_data"] = () =>
                {
                    item1.Errors.Count().should_be(0);
                    item2.Errors.Count().should_be(0);
                    contact.Errors.Count().should_be(0);
                };

            it["creates_po"] = () =>
                {
                    newPO = new PurchaseOrder
                    {
                        ContactId = contact.Id,
                        PurchaseDate = DateTime.Now
                    };
                    newPO = poService.CreateObject(newPO, contactService);
                    newPO.Errors.Count().should_be(0);
                    poService.GetObjectById(newPO.Id).should_not_be_null();
                };

            context["when creating PO Detail"] = () =>
                {
                    before = () =>
                    {
                        newPO = new PurchaseOrder
                        {
                            ContactId = contact.Id,
                            PurchaseDate = DateTime.Now
                        };
                        newPO = poService.CreateObject(newPO, contactService);
                    };

                    it["creates po detail"] = () =>
                    {
                        PurchaseOrderDetail poDetail = new PurchaseOrderDetail
                        {
                            PurchaseOrderId = newPO.Id,
                            ItemId = item1.Id,
                            Quantity = 5,
                            Price = 30000
                        };
                        poDetail = poDetailService.CreateObject(poDetail, poService, itemService);
                        poDetail.Errors.Count().should_be(0);
                    };

                    it["should not create po detail if there is no item id"] = () =>
                    {
                        PurchaseOrderDetail poDetail = new PurchaseOrderDetail
                        {
                            PurchaseOrderId = newPO.Id,
                            ItemId = 0,
                            Quantity = 5,
                            Price = 25000
                        };
                        poDetail = poDetailService.CreateObject(poDetail, poService, itemService);
                        poDetail.Errors.Count().should_not_be(0);
                    };

                    it["should not create po detail if there is no unique item_id"] = () =>
                    {
                        PurchaseOrderDetail poDetail1 = new PurchaseOrderDetail
                        {
                            PurchaseOrderId = newPO.Id,
                            ItemId = item1.Id,
                            Quantity = 5,
                            Price = 30000
                        };
                        poDetail1 = poDetailService.CreateObject(poDetail1, poService, itemService);

                        PurchaseOrderDetail poDetail2 = new PurchaseOrderDetail
                        {
                            PurchaseOrderId = newPO.Id,
                            ItemId = item1.Id,
                            Quantity = 5,
                            Price = 2500000
                        };
                        poDetail2 = poDetailService.CreateObject(poDetail2, poService, itemService);
                        poDetail2.Errors.Count().should_not_be(0);
                    };


                    it["must not allow PO confirmation if there is no PO Detail"] = () =>
                    {
                        newPO = poService.ConfirmObject(newPO, poDetailService, stockMutationService, itemService);
                        newPO.Errors.Count().should_not_be(0);
                    };
                };

            context["when confirming PO"] = () =>
            {
                before = () =>
                {
                    newPO = new PurchaseOrder
                    {
                        ContactId = contact.Id,
                        PurchaseDate = DateTime.Now
                    };
                    newPO = poService.CreateObject(newPO, contactService);

                    poDetail1 = new PurchaseOrderDetail
                    {
                        PurchaseOrderId = newPO.Id,
                        ItemId = item1.Id,
                        Quantity = 5,
                        Price = 30000
                    };
                    poDetail1 = poDetailService.CreateObject(poDetail1, poService, itemService);

                    poDetail2 = new PurchaseOrderDetail
                    {
                        PurchaseOrderId = newPO.Id,
                        ItemId = item2.Id,
                        Quantity = 5,
                        Price = 25000
                    };
                    poDetail2 = poDetailService.CreateObject(poDetail2, poService, itemService);
                };

                it["allows confirmation"] = () =>
                {
                    newPO = poService.ConfirmObject(newPO, poDetailService, stockMutationService, itemService);
                    newPO.IsConfirmed.should_be(true);
                };

                context["post PO confirm"] = () =>
                {
                    int Quantity1=0;
                    int Quantity2=0;
                    before = () =>
                    {
                        item1 = itemService.GetObjectById(item1.Id);
                        item2 = itemService.GetObjectById(item2.Id);
                        Quantity1 = item1.PendingReceival;
                        Quantity2 = item2.PendingReceival;
                        newPO = poService.ConfirmObject(newPO, poDetailService, stockMutationService, itemService);
                    };

                    it["should increase pending receival in item"] = () =>
                    {
                        Item NewItem1 = itemService.GetObjectById(item1.Id);
                        Item NewItem2 = itemService.GetObjectById(item2.Id);

                        int diff_1 = NewItem1.PendingReceival - Quantity1;
                        diff_1.should_be(poDetail1.Quantity);

                        int diff_2 = NewItem2.PendingReceival - Quantity2;
                        diff_2.should_be(poDetail2.Quantity);
                    };

                    it["should create StockMutation"] = () =>
                    {
                        stockMutationService.GetObjectsByItemId(item1.Id).Count().should_not_be(0);
                        stockMutationService.GetObjectsByItemId(item2.Id).Count().should_not_be(0);
                    };
                };
            };
        }
    }
}