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

    public class SpecDeliveryOrder : nspec
    {
        Contact contact;
        Item item_batiktulis;
        Item item_busway;
        Item item_botolaqua;
        StockAdjustment stockAdjustment;
        StockAdjustmentDetail stockAdjust_batiktulis;
        StockAdjustmentDetail stockAdjust_busway;
        StockAdjustmentDetail stockAdjust_botolaqua;
        SalesOrder salesOrder1;
        SalesOrder salesOrder2;
        SalesOrderDetail salesOrderDetail_batiktulis_so1;
        SalesOrderDetail salesOrderDetail_busway_so1;
        SalesOrderDetail salesOrderDetail_botolaqua_so1;
        SalesOrderDetail salesOrderDetail_batiktulis_so2;
        SalesOrderDetail salesOrderDetail_busway_so2;
        SalesOrderDetail salesOrderDetail_botolaqua_so2;
        DeliveryOrder deliveryOrder1;
        DeliveryOrder deliveryOrder2;
        DeliveryOrderDetail deliveryOrderDetail_batiktulis_do1;
        DeliveryOrderDetail deliveryOrderDetail_busway_do1;
        DeliveryOrderDetail deliveryOrderDetail_botolaqua_do1;
        DeliveryOrderDetail deliveryOrderDetail_batiktulis_do2a;
        DeliveryOrderDetail deliveryOrderDetail_batiktulis_do2b;
        DeliveryOrderDetail deliveryOrderDetail_busway_do2;
        DeliveryOrderDetail deliveryOrderDetail_botolaqua_do2;
        IContactService _contactService;
        IItemService _itemService;
        IStockMutationService _stockMutationService;
        IStockAdjustmentService _stockAdjustmentService;
        IStockAdjustmentDetailService _stockAdjustmentDetailService;
        ISalesOrderService _salesOrderService;
        ISalesOrderDetailService _salesOrderDetailService;
        IDeliveryOrderService _deliveryOrderService;
        IDeliveryOrderDetailService _deliveryOrderDetailService;

        void before_each()
        {
            var db = new StockControlEntities();
            using (db)
            {
                db.DeleteAllTables();
                _contactService = new ContactService(new ContactRepository(), new ContactValidator());
                _itemService = new ItemService(new ItemRepository(), new ItemValidator());
                _stockMutationService = new StockMutationService(new StockMutationRepository(), new StockMutationValidator());
                _salesOrderService = new SalesOrderService(new SalesOrderRepository(), new SalesOrderValidator());
                _salesOrderDetailService = new SalesOrderDetailService(new SalesOrderDetailRepository(), new SalesOrderDetailValidator());
                _deliveryOrderService = new DeliveryOrderService(new DeliveryOrderRepository(), new DeliveryOrderValidator());
                _deliveryOrderDetailService = new DeliveryOrderDetailService(new DeliveryOrderDetailRepository(), new DeliveryOrderDetailValidator());
                _stockAdjustmentService = new StockAdjustmentService(new StockAdjustmentRepository(), new StockAdjustmentValidator());
                _stockAdjustmentDetailService = new StockAdjustmentDetailService(new StockAdjustmentDetailRepository(), new StockAdjustmentDetailValidator());

                contact = _contactService.CreateObject("Bpk. Presiden", "Istana Negara");
                item_batiktulis = _itemService.CreateObject("Batik Tulis", "Untuk Para Menteri Negara", "RI001");
                item_busway = _itemService.CreateObject("Busway", "Untuk disumbangkan bagi kebutuhan DKI Jakarta", "RI002");
                item_botolaqua = _itemService.CreateObject("Botol Aqua", "Minuman untuk pekerja di Istana Negara", "RI003");
                stockAdjustment = _stockAdjustmentService.CreateObject(new DateTime(2014, 1, 1));
                stockAdjust_batiktulis = _stockAdjustmentDetailService.CreateObject(stockAdjustment.Id, item_batiktulis.Id, 990, 1400000, _stockAdjustmentService, _itemService);
                stockAdjust_busway = _stockAdjustmentDetailService.CreateObject(stockAdjustment.Id, item_busway.Id, 120, 725000000, _stockAdjustmentService, _itemService);
                stockAdjust_botolaqua = _stockAdjustmentDetailService.CreateObject(stockAdjustment.Id, item_botolaqua.Id, 5000, 4000, _stockAdjustmentService, _itemService);
                stockAdjustment = _stockAdjustmentService.ConfirmObject(stockAdjustment, _stockAdjustmentDetailService, _stockMutationService, _itemService);
                salesOrder1 = _salesOrderService.CreateObject(contact.Id, new DateTime(2014, 07, 09), _contactService);
                salesOrder2 = _salesOrderService.CreateObject(contact.Id, new DateTime(2014, 04, 09), _contactService);
                salesOrderDetail_batiktulis_so1 = _salesOrderDetailService.CreateObject(salesOrder1.Id, item_batiktulis.Id, 500, 2000000, _salesOrderService, _itemService);
                salesOrderDetail_busway_so1 = _salesOrderDetailService.CreateObject(salesOrder1.Id, item_busway.Id, 91, 800000000, _salesOrderService, _itemService);
                salesOrderDetail_botolaqua_so1 = _salesOrderDetailService.CreateObject(salesOrder1.Id, item_botolaqua.Id, 2000, 5000, _salesOrderService, _itemService);
                salesOrderDetail_batiktulis_so2 = _salesOrderDetailService.CreateObject(salesOrder2.Id, item_batiktulis.Id, 40, 2000500, _salesOrderService, _itemService);
                salesOrderDetail_busway_so2 = _salesOrderDetailService.CreateObject(salesOrder2.Id, item_busway.Id, 3, 810000000, _salesOrderService, _itemService);
                salesOrderDetail_botolaqua_so2 = _salesOrderDetailService.CreateObject(salesOrder2.Id, item_botolaqua.Id, 340, 5500, _salesOrderService, _itemService);
                salesOrder1 = _salesOrderService.ConfirmObject(salesOrder1, _salesOrderDetailService, _stockMutationService, _itemService);
                salesOrder2 = _salesOrderService.ConfirmObject(salesOrder2, _salesOrderDetailService, _stockMutationService, _itemService);
            }
        }

        void deliveryorder_validation()
        {
            it["validates_all_variables"] = () =>
            {
                contact.Errors.Count().should_be(0);
                item_batiktulis.Errors.Count().should_be(0);
                item_busway.Errors.Count().should_be(0);
                item_botolaqua.Errors.Count().should_be(0);
                salesOrder1.Errors.Count().should_be(0);
                salesOrder2.Errors.Count().should_be(0);
            };

            it["validates the item ready stock"] = () =>
            {
                item_batiktulis.Ready.should_be(stockAdjust_batiktulis.Quantity);
                item_busway.Ready.should_be(stockAdjust_busway.Quantity);
                item_botolaqua.Ready.should_be(stockAdjust_botolaqua.Quantity);
            };

            it["validates the item pending delivery"] = () =>
            {
                item_batiktulis.PendingDelivery.should_be(salesOrderDetail_batiktulis_so1.Quantity + salesOrderDetail_batiktulis_so2.Quantity);
                item_busway.PendingDelivery.should_be(salesOrderDetail_busway_so1.Quantity + salesOrderDetail_busway_so2.Quantity);
                item_botolaqua.PendingDelivery.should_be(salesOrderDetail_botolaqua_so1.Quantity + salesOrderDetail_botolaqua_so2.Quantity);
            };

            context["when confirming delivery order"] = () =>
            {
                before = () =>
                {
                    deliveryOrder1 = _deliveryOrderService.CreateObject(contact.Id, new DateTime(2000, 1, 1), _contactService);
                    deliveryOrder2 = _deliveryOrderService.CreateObject(contact.Id, new DateTime(2014, 5, 5), _contactService);
                    deliveryOrderDetail_batiktulis_do1 = _deliveryOrderDetailService.CreateObject(deliveryOrder1.Id, item_batiktulis.Id, 400, salesOrderDetail_batiktulis_so1.Id, _deliveryOrderService,
                                                                                                  _salesOrderDetailService, _salesOrderService, _itemService, _contactService);
                    deliveryOrderDetail_busway_do1 = _deliveryOrderDetailService.CreateObject(deliveryOrder1.Id, item_busway.Id, 91, salesOrderDetail_busway_so1.Id, _deliveryOrderService,
                                                                                                _salesOrderDetailService, _salesOrderService, _itemService, _contactService);
                    deliveryOrderDetail_botolaqua_do1 = _deliveryOrderDetailService.CreateObject(deliveryOrder1.Id, item_botolaqua.Id, 2000, salesOrderDetail_botolaqua_so1.Id,  _deliveryOrderService,
                                                                                                  _salesOrderDetailService, _salesOrderService, _itemService, _contactService);
                    deliveryOrderDetail_batiktulis_do2a = _deliveryOrderDetailService.CreateObject(deliveryOrder2.Id, item_batiktulis.Id, 100, salesOrderDetail_batiktulis_so1.Id, _deliveryOrderService,
                                                                                                                          _salesOrderDetailService, _salesOrderService, _itemService, _contactService);
                    deliveryOrderDetail_batiktulis_do2b = _deliveryOrderDetailService.CreateObject(deliveryOrder2.Id, item_batiktulis.Id, 40, salesOrderDetail_batiktulis_so2.Id, _deliveryOrderService,
                                                                                                                          _salesOrderDetailService, _salesOrderService, _itemService, _contactService);
                    deliveryOrderDetail_busway_do2 = _deliveryOrderDetailService.CreateObject(deliveryOrder2.Id, item_busway.Id, 3, salesOrderDetail_busway_so2.Id, _deliveryOrderService,
                                                                                                                          _salesOrderDetailService, _salesOrderService, _itemService, _contactService);
                    deliveryOrderDetail_botolaqua_do2 = _deliveryOrderDetailService.CreateObject(deliveryOrder2.Id, item_botolaqua.Id, 340, salesOrderDetail_botolaqua_so2.Id, _deliveryOrderService,
                                                                                                                          _salesOrderDetailService, _salesOrderService, _itemService, _contactService);
                    deliveryOrder1 = _deliveryOrderService.ConfirmObject(deliveryOrder1, _deliveryOrderDetailService, _salesOrderDetailService, _stockMutationService, _itemService);
                    deliveryOrder2 = _deliveryOrderService.ConfirmObject(deliveryOrder2, _deliveryOrderDetailService, _salesOrderDetailService, _stockMutationService, _itemService);
                };

                it["validates_deliveryorders"] = () =>
                {
                    deliveryOrder1.Errors.Count().should_be(0);
                    deliveryOrder2.Errors.Count().should_be(0);
                };

                it["deletes confirmed delivery order"] = () =>
                {
                    deliveryOrder1 = _deliveryOrderService.SoftDeleteObject(deliveryOrder1, _deliveryOrderDetailService);
                    deliveryOrder1.Errors.Count().should_not_be(0);
                };

                it["unconfirm delivery order"] = () =>
                {
                    deliveryOrder1 = _deliveryOrderService.UnconfirmObject(deliveryOrder1, _deliveryOrderDetailService, _stockMutationService, _itemService);
                    deliveryOrder1.Errors.Count().should_be(0);
                };

                it["validates item pending delivery"] = () =>
                {
                    item_batiktulis.PendingDelivery.should_be(0);
                    item_busway.PendingDelivery.should_be(0);
                    item_botolaqua.PendingDelivery.should_be(0);
                };
            };
        }
    }
}