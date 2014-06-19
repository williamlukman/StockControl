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

    public class SpecPurchaseReceival : nspec
    {
        Contact contact;
        Item item_batiktulis;
        Item item_busway;
        Item item_botolaqua;
        StockAdjustment stockAdjustment;
        StockAdjustmentDetail stockAdjust_batiktulis;
        StockAdjustmentDetail stockAdjust_busway;
        StockAdjustmentDetail stockAdjust_botolaqua;
        PurchaseOrder purchaseOrder1;
        PurchaseOrder purchaseOrder2;
        PurchaseOrderDetail purchaseOrderDetail_batiktulis_so1;
        PurchaseOrderDetail purchaseOrderDetail_busway_so1;
        PurchaseOrderDetail purchaseOrderDetail_botolaqua_so1;
        PurchaseOrderDetail purchaseOrderDetail_batiktulis_so2;
        PurchaseOrderDetail purchaseOrderDetail_busway_so2;
        PurchaseOrderDetail purchaseOrderDetail_botolaqua_so2;
        PurchaseReceival purchaseReceival1;
        PurchaseReceival purchaseReceival2;
        PurchaseReceivalDetail purchaseReceivalDetail_batiktulis_do1;
        PurchaseReceivalDetail purchaseReceivalDetail_busway_do1;
        PurchaseReceivalDetail purchaseReceivalDetail_botolaqua_do1;
        PurchaseReceivalDetail purchaseReceivalDetail_batiktulis_do2a;
        PurchaseReceivalDetail purchaseReceivalDetail_batiktulis_do2b;
        PurchaseReceivalDetail purchaseReceivalDetail_busway_do2;
        PurchaseReceivalDetail purchaseReceivalDetail_botolaqua_do2;
        IContactService _contactService;
        IItemService _itemService;
        IStockMutationService _stockMutationService;
        IStockAdjustmentService _stockAdjustmentService;
        IStockAdjustmentDetailService _stockAdjustmentDetailService;
        IPurchaseOrderService _purchaseOrderService;
        IPurchaseOrderDetailService _purchaseOrderDetailService;
        IPurchaseReceivalService _purchaseReceivalService;
        IPurchaseReceivalDetailService _purchaseReceivalDetailService;

        void before_each()
        {
            var db = new StockControlEntities();
            using (db)
            {
                db.DeleteAllTables();
                _contactService = new ContactService(new ContactRepository(), new ContactValidator());
                _itemService = new ItemService(new ItemRepository(), new ItemValidator());
                _stockMutationService = new StockMutationService(new StockMutationRepository(), new StockMutationValidator());
                _purchaseOrderService = new PurchaseOrderService(new PurchaseOrderRepository(), new PurchaseOrderValidator());
                _purchaseOrderDetailService = new PurchaseOrderDetailService(new PurchaseOrderDetailRepository(), new PurchaseOrderDetailValidator());
                _purchaseReceivalService = new PurchaseReceivalService(new PurchaseReceivalRepository(), new PurchaseReceivalValidator());
                _purchaseReceivalDetailService = new PurchaseReceivalDetailService(new PurchaseReceivalDetailRepository(), new PurchaseReceivalDetailValidator());
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
                purchaseOrder1 = _purchaseOrderService.CreateObject(contact.Id, new DateTime(2014, 07, 09), _contactService);
                purchaseOrder2 = _purchaseOrderService.CreateObject(contact.Id, new DateTime(2014, 04, 09), _contactService);
                purchaseOrderDetail_batiktulis_so1 = _purchaseOrderDetailService.CreateObject(purchaseOrder1.Id, item_batiktulis.Id, 500, 2000000, _purchaseOrderService, _itemService);
                purchaseOrderDetail_busway_so1 = _purchaseOrderDetailService.CreateObject(purchaseOrder1.Id, item_busway.Id, 91, 800000000, _purchaseOrderService, _itemService);
                purchaseOrderDetail_botolaqua_so1 = _purchaseOrderDetailService.CreateObject(purchaseOrder1.Id, item_botolaqua.Id, 2000, 5000, _purchaseOrderService, _itemService);
                purchaseOrderDetail_batiktulis_so2 = _purchaseOrderDetailService.CreateObject(purchaseOrder2.Id, item_batiktulis.Id, 40, 2000500, _purchaseOrderService, _itemService);
                purchaseOrderDetail_busway_so2 = _purchaseOrderDetailService.CreateObject(purchaseOrder2.Id, item_busway.Id, 3, 810000000, _purchaseOrderService, _itemService);
                purchaseOrderDetail_botolaqua_so2 = _purchaseOrderDetailService.CreateObject(purchaseOrder2.Id, item_botolaqua.Id, 340, 5500, _purchaseOrderService, _itemService);
                purchaseOrder1 = _purchaseOrderService.ConfirmObject(purchaseOrder1, _purchaseOrderDetailService, _stockMutationService, _itemService);
                purchaseOrder2 = _purchaseOrderService.ConfirmObject(purchaseOrder2, _purchaseOrderDetailService, _stockMutationService, _itemService);
            }
        }

        void purchasereceival_validation()
        {
            it["validates_all_variables"] = () =>
            {
                contact.Errors.Count().should_be(0);
                item_batiktulis.Errors.Count().should_be(0);
                item_busway.Errors.Count().should_be(0);
                item_botolaqua.Errors.Count().should_be(0);
                purchaseOrder1.Errors.Count().should_be(0);
                purchaseOrder2.Errors.Count().should_be(0);
            };

            it["validates the item ready stock"] = () =>
            {
                item_batiktulis.Ready.should_be(stockAdjust_batiktulis.Quantity);
                item_busway.Ready.should_be(stockAdjust_busway.Quantity);
                item_botolaqua.Ready.should_be(stockAdjust_botolaqua.Quantity);
            };

            it["validates the item pending receival"] = () =>
            {
                item_batiktulis.PendingReceival.should_be(purchaseOrderDetail_batiktulis_so1.Quantity + purchaseOrderDetail_batiktulis_so2.Quantity);
                item_busway.PendingReceival.should_be(purchaseOrderDetail_busway_so1.Quantity + purchaseOrderDetail_busway_so2.Quantity);
                item_botolaqua.PendingReceival.should_be(purchaseOrderDetail_botolaqua_so1.Quantity + purchaseOrderDetail_botolaqua_so2.Quantity);
            };

            context["when confirming purchase receival"] = () =>
            {
                before = () =>
                {
                    purchaseReceival1 = _purchaseReceivalService.CreateObject(contact.Id, new DateTime(2000, 1, 1), _contactService);
                    purchaseReceival2 = _purchaseReceivalService.CreateObject(contact.Id, new DateTime(2014, 5, 5), _contactService);
                    purchaseReceivalDetail_batiktulis_do1 = _purchaseReceivalDetailService.CreateObject(purchaseReceival1.Id, item_batiktulis.Id, 400, purchaseOrderDetail_batiktulis_so1.Id, _purchaseReceivalService,
                                                                                                  _purchaseOrderDetailService, _purchaseOrderService, _itemService, _contactService);
                    purchaseReceivalDetail_busway_do1 = _purchaseReceivalDetailService.CreateObject(purchaseReceival1.Id, item_busway.Id, 91, purchaseOrderDetail_busway_so1.Id, _purchaseReceivalService,
                                                                                                _purchaseOrderDetailService, _purchaseOrderService, _itemService, _contactService);
                    purchaseReceivalDetail_botolaqua_do1 = _purchaseReceivalDetailService.CreateObject(purchaseReceival1.Id, item_botolaqua.Id, 2000, purchaseOrderDetail_botolaqua_so1.Id,  _purchaseReceivalService,
                                                                                                  _purchaseOrderDetailService, _purchaseOrderService, _itemService, _contactService);
                    purchaseReceivalDetail_batiktulis_do2a = _purchaseReceivalDetailService.CreateObject(purchaseReceival2.Id, item_batiktulis.Id, 100, purchaseOrderDetail_batiktulis_so1.Id, _purchaseReceivalService,
                                                                                                                          _purchaseOrderDetailService, _purchaseOrderService, _itemService, _contactService);
                    purchaseReceivalDetail_batiktulis_do2b = _purchaseReceivalDetailService.CreateObject(purchaseReceival2.Id, item_batiktulis.Id, 40, purchaseOrderDetail_batiktulis_so2.Id, _purchaseReceivalService,
                                                                                                                          _purchaseOrderDetailService, _purchaseOrderService, _itemService, _contactService);
                    purchaseReceivalDetail_busway_do2 = _purchaseReceivalDetailService.CreateObject(purchaseReceival2.Id, item_busway.Id, 3, purchaseOrderDetail_busway_so2.Id, _purchaseReceivalService,
                                                                                                                          _purchaseOrderDetailService, _purchaseOrderService, _itemService, _contactService);
                    purchaseReceivalDetail_botolaqua_do2 = _purchaseReceivalDetailService.CreateObject(purchaseReceival2.Id, item_botolaqua.Id, 340, purchaseOrderDetail_botolaqua_so2.Id, _purchaseReceivalService,
                                                                                                                          _purchaseOrderDetailService, _purchaseOrderService, _itemService, _contactService);
                    purchaseReceival1 = _purchaseReceivalService.ConfirmObject(purchaseReceival1, _purchaseReceivalDetailService, _purchaseOrderDetailService, _stockMutationService, _itemService);
                    purchaseReceival2 = _purchaseReceivalService.ConfirmObject(purchaseReceival2, _purchaseReceivalDetailService, _purchaseOrderDetailService, _stockMutationService, _itemService);
                };

                it["validates_purchasereceivals"] = () =>
                {
                    purchaseReceival1.Errors.Count().should_be(0);
                    purchaseReceival2.Errors.Count().should_be(0);
                };

                it["deletes confirmed purchase receival"] = () =>
                {
                    purchaseReceival1 = _purchaseReceivalService.SoftDeleteObject(purchaseReceival1, _purchaseReceivalDetailService);
                    purchaseReceival1.Errors.Count().should_not_be(0);
                };

                it["unconfirm purchase receival"] = () =>
                {
                    purchaseReceival1 = _purchaseReceivalService.UnconfirmObject(purchaseReceival1, _purchaseReceivalDetailService, _purchaseOrderDetailService, _stockMutationService, _itemService);
                    purchaseReceival1.Errors.Count().should_be(0);
                };

                it["validates item pending receival"] = () =>
                {
                    item_batiktulis.PendingReceival.should_be(0);
                    item_busway.PendingReceival.should_be(0);
                    item_botolaqua.PendingReceival.should_be(0);
                };
            };
        }
    }
}