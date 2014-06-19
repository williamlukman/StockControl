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
using Core.Constant;

namespace TestValidation
{

    public class SpecSalesOrder : nspec
    {
        Contact contact;
        Item item_sepatubola;
        Item item_batiktulis;
        SalesOrder salesOrder;
        SalesOrderDetail salesOrderDetail1;
        SalesOrderDetail salesOrderDetail2;
        IContactService _contactService;
        IItemService _itemService;
        ISalesOrderService _salesOrderService;
        ISalesOrderDetailService _salesOrderDetailService;
        IDeliveryOrderService _deliveryOrderService;
        IDeliveryOrderDetailService _deliveryOrderDetailService;
        IStockMutationService _stockMutationService;
        int Quantity1;
        int Quantity2;
        void before_each()
        {
            var db = new StockControlEntities();
            using (db)
            {
                db.DeleteAllTables();
                _contactService = new ContactService(new ContactRepository(), new ContactValidator());
                _itemService = new ItemService(new ItemRepository(), new ItemValidator());
                _salesOrderService = new SalesOrderService(new SalesOrderRepository(), new SalesOrderValidator());
                _salesOrderDetailService = new SalesOrderDetailService(new SalesOrderDetailRepository(), new SalesOrderDetailValidator());
                _deliveryOrderService = new DeliveryOrderService(new DeliveryOrderRepository(), new DeliveryOrderValidator());
                _deliveryOrderDetailService = new DeliveryOrderDetailService(new DeliveryOrderDetailRepository(), new DeliveryOrderDetailValidator());
                _stockMutationService = new StockMutationService(new StockMutationRepository(), new StockMutationValidator());

                contact = _contactService.CreateObject("Harijadi", "Jl. Pahlawan 1 Bojonegoro");
                item_batiktulis = _itemService.CreateObject("Batik Tulis", "Tenunan Halus dari Surakarta", "BTL001");
                item_sepatubola = _itemService.CreateObject("Sepatu Bola Neymar 2015", "Special Edition Signed by Neymar", "NEY001");
            }
        }

        void salesorder_validation()
        {
            it["validate_contact_and_items"] = () =>
            {
                contact.Errors.Count().should_be(0);
                item_batiktulis.Errors.Count().should_be(0);
                item_sepatubola.Errors.Count().should_be(0);
            };

            it["create_salesorder"] = () =>
            {
                salesOrder = _salesOrderService.CreateObject(contact.Id, new DateTime(2000, 1, 1), _contactService);
                salesOrder.Errors.Count().should_be(0);
            };

            it["create_salesorder_with_no_contactid"] = () =>
            {
                salesOrder = new SalesOrder()
                {
                    SalesDate = DateTime.Now
                };
                _salesOrderService.CreateObject(salesOrder, _contactService);
                salesOrder.Errors.Count().should_not_be(0);
            };

            it["create_salesorder_with_no_elements"] = () =>
            {
                salesOrder = new SalesOrder();
                _salesOrderService.CreateObject(salesOrder, _contactService);
                salesOrder.Errors.Count().should_not_be(0);
            };

            context["validating_salesorder"] = () =>
            {
                before = () =>
                {
                    salesOrder = new SalesOrder
                    {
                        ContactId = contact.Id,
                        SalesDate = DateTime.Now
                    };
                    salesOrder = _salesOrderService.CreateObject(salesOrder, _contactService);
                };

                it["delete_salesorder"] = () =>
                {
                    salesOrder = _salesOrderService.SoftDeleteObject(salesOrder, _salesOrderDetailService);
                    salesOrder.Errors.Count().should_be(0);
                };

                it["delete_salesorder_and_details"] = () =>
                {
                    salesOrderDetail1 = _salesOrderDetailService.CreateObject(salesOrder.Id, item_batiktulis.Id, 5, 100000, _salesOrderService, _itemService);
                    salesOrderDetail2 = _salesOrderDetailService.CreateObject(salesOrder.Id, item_sepatubola.Id, 12, 850000, _salesOrderService, _itemService);
                    salesOrder = _salesOrderService.SoftDeleteObject(salesOrder, _salesOrderDetailService);
                    salesOrder.Errors.Count().should_be(0);
                };

                it["delete_salesorderdetail"] = () =>
                {
                    salesOrderDetail1 = _salesOrderDetailService.CreateObject(salesOrder.Id, item_batiktulis.Id, 5, 100000, _salesOrderService, _itemService);
                    salesOrderDetail2 = _salesOrderDetailService.CreateObject(salesOrder.Id, item_sepatubola.Id, 12, 850000, _salesOrderService, _itemService);
                    salesOrderDetail2 = _salesOrderDetailService.SoftDeleteObject(salesOrderDetail2);
                    salesOrderDetail2.Errors.Count().should_be(0);
                };

                it["create_salesorderdetails_with_same_item"] = () =>
                {
                    salesOrderDetail1 = _salesOrderDetailService.CreateObject(salesOrder.Id, item_batiktulis.Id, 5, 100000, _salesOrderService, _itemService);
                    salesOrderDetail2 = _salesOrderDetailService.CreateObject(salesOrder.Id, item_batiktulis.Id, 12, 850000, _salesOrderService, _itemService);
                    salesOrderDetail2.Errors.Count().should_not_be(0);
                };

                context["when confirming SO"] = () =>
                {
                    before = () =>
                    {
                        salesOrderDetail1 = _salesOrderDetailService.CreateObject(salesOrder.Id, item_batiktulis.Id, 5, 100000, _salesOrderService, _itemService);
                        salesOrderDetail2 = _salesOrderDetailService.CreateObject(salesOrder.Id, item_sepatubola.Id, 12, 850000, _salesOrderService, _itemService);
                        Quantity1 = item_batiktulis.PendingDelivery;
                        Quantity2 = item_sepatubola.PendingDelivery;
                        salesOrder = _salesOrderService.ConfirmObject(salesOrder, _salesOrderDetailService, _stockMutationService, _itemService);
                    };

                    it["confirmed_salesorder_and_details"] = () =>
                    {
                        IList<StockMutation> stockMutations = _stockMutationService.GetObjectsBySourceDocumentDetail(item_batiktulis.Id, Constant.SourceDocumentDetailType.SalesOrderDetail, salesOrderDetail1.Id);
                        stockMutations.Count().should_be(1);
                        salesOrder.Errors.Count().should_be(0);
                    };

                    it["delete_confirmed_salesorder_and_details"] = () =>
                    {
                        salesOrder = _salesOrderService.SoftDeleteObject(salesOrder, _salesOrderDetailService);
                        salesOrder.Errors.Count().should_not_be(0);
                    };

                    it["unconfirm_salesorderdetail"] = () =>
                    {
                        salesOrderDetail2 = _salesOrderDetailService.UnconfirmObject(salesOrderDetail2, _deliveryOrderDetailService, _stockMutationService, _itemService);
                        salesOrderDetail2.Errors.Count().should_be(0);
                        IList<StockMutation> stockMutations = _stockMutationService.GetObjectsBySourceDocumentDetail(item_sepatubola.Id, Constant.SourceDocumentDetailType.SalesOrderDetail, salesOrderDetail2.Id);
                        stockMutations.Count().should_be(0);
                    };

                    it["delete_unconfirm_salesorderdetail"] = () =>
                    {
                        salesOrderDetail2 = _salesOrderDetailService.UnconfirmObject(salesOrderDetail2, _deliveryOrderDetailService, _stockMutationService, _itemService);
                        salesOrderDetail2 = _salesOrderDetailService.SoftDeleteObject(salesOrderDetail2);
                        salesOrderDetail2.Errors.Count().should_be(0);
                    };

                    it["should increase pending delivery in item"] = () =>
                    {
                        Item NewItem1 = _itemService.GetObjectById(item_batiktulis.Id);
                        Item NewItem2 = _itemService.GetObjectById(item_sepatubola.Id);

                        int diff_1 = NewItem1.PendingDelivery - Quantity1;
                        diff_1.should_be(salesOrderDetail1.Quantity);

                        int diff_2 = NewItem2.PendingDelivery - Quantity2;
                        diff_2.should_be(salesOrderDetail2.Quantity);
                    };
                };
            };
        }
    }
}