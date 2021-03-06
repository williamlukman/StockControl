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

    public class SpecContact : nspec
    {
        Contact contact;
        IContactService _contactService;
        IPurchaseOrderService _purchaseOrderService;
        IPurchaseOrderDetailService _purchaseOrderDetailService;
        IPurchaseReceivalService _purchaseReceivalService;
        ISalesOrderService _salesOrderService;
        ISalesOrderDetailService _salesOrderDetailService;
        IDeliveryOrderService _deliveryOrderService;
        IItemService _itemService;
        
        void before_each()
        {
            var db = new StockControlEntities();
            using (db)
            {
                db.DeleteAllTables();
                _contactService = new ContactService(new ContactRepository(), new ContactValidator());
                _purchaseOrderService = new PurchaseOrderService(new PurchaseOrderRepository(), new PurchaseOrderValidator());
                _purchaseOrderDetailService = new PurchaseOrderDetailService(new PurchaseOrderDetailRepository(), new PurchaseOrderDetailValidator());
                _purchaseReceivalService = new PurchaseReceivalService(new PurchaseReceivalRepository(), new PurchaseReceivalValidator());
                _salesOrderService = new SalesOrderService(new SalesOrderRepository(), new SalesOrderValidator());
                _salesOrderDetailService = new SalesOrderDetailService(new SalesOrderDetailRepository(), new SalesOrderDetailValidator());
                _deliveryOrderService = new DeliveryOrderService(new DeliveryOrderRepository(), new DeliveryOrderValidator());
                _itemService = new ItemService(new ItemRepository(), new ItemValidator());
            }
        }

        /*
         * STEPS:
         * 1. Create valid contact
         * 2. Create invalid contact with no name
         * 3. Create valid contact with no address
         * 4. Create contact with no elements
         * 5a. Delete contact
         * 5b. Delete contact with purchase order
         * 5c. Delete contact with deleted purchase order
         * 5d. Delete contact with sales order
         * 5e. Delete contact with deleted sales order & details
         */
        void contact_validation()
        {
            it["create_contact"] = () =>
                {
                    contact = _contactService.CreateObject("Harijadi", "Jl. Pahlawan 1 Bojonegoro" );
                    contact.Errors.Count().should_be(0);
                };


            it["create_invalid_contact_no_name"] = () =>
                {
                    contact = _contactService.CreateObject("               ", "Jl. Tanpa Nama 321 Gang Buntu");
                    contact.Errors.Count().should_not_be(0);
                };

            it["create_contact_no_address"] = () =>
                {
                    contact = _contactService.CreateObject("Suramadu", "Jl. P.B.Sudirman 114 Suramadu");
                    contact.Errors.Count().should_be(0);
                };

            it["create_contact_with_no_element"] = () =>
                {
                    contact = new Contact();
                    _contactService.CreateObject(contact);
                    contact.Errors.Count().should_not_be(0);
                };

            context["when deleting Contact"] = () =>
                {
                    before = () =>
                        {
                            contact = _contactService.CreateObject("Harijadi", "Jl. Pahlawan 1 Bojonegoro");
                        };

                    it["deletes contact"] = () =>
                        {
                            contact = _contactService.SoftDeleteObject(contact, _purchaseOrderService, _purchaseReceivalService, _salesOrderService, _deliveryOrderService);
                            contact.Errors.Count().should_be(0);
                        };

                    it["deletes contact with purchaseorder"] = () =>
                        {
                            PurchaseOrder purchaseOrder = _purchaseOrderService.CreateObject(contact.Id, DateTime.Now, _contactService);
                            contact = _contactService.SoftDeleteObject(contact, _purchaseOrderService, _purchaseReceivalService, _salesOrderService, _deliveryOrderService);
                            contact.Errors.Count().should_not_be(0);
                        };

                    it["deletes cashbank with deleted purchaseorder"] = () =>
                        {
                            PurchaseOrder purchaseOrder = _purchaseOrderService.CreateObject(contact.Id, DateTime.Now, _contactService);
                            _purchaseOrderService.SoftDeleteObject(purchaseOrder, _purchaseOrderDetailService);
                            contact = _contactService.SoftDeleteObject(contact, _purchaseOrderService, _purchaseReceivalService, _salesOrderService, _deliveryOrderService);
                            contact.Errors.Count().should_be(0);
                        };

                    it["deletes contact with salesorder"] = () =>
                    {
                        SalesOrder salesOrder = _salesOrderService.CreateObject(contact.Id, DateTime.Now, _contactService);
                        contact = _contactService.SoftDeleteObject(contact, _purchaseOrderService, _purchaseReceivalService, _salesOrderService, _deliveryOrderService);
                        contact.Errors.Count().should_not_be(0);
                    };

                    it["deletes contact with deleted salesorder"] = () =>
                    {
                        SalesOrder salesOrder = _salesOrderService.CreateObject(contact.Id, DateTime.Now, _contactService);
                        Item item = _itemService.CreateObject("Teh Botol", "Ukuran 200ml", "TBTL200M");
                        SalesOrderDetail salesOrderDetail = _salesOrderDetailService.CreateObject(salesOrder.Id, item.Id, 1, 100000, _salesOrderService, _itemService);
                        _salesOrderService.SoftDeleteObject(salesOrder, _salesOrderDetailService);
                        contact = _contactService.SoftDeleteObject(contact, _purchaseOrderService, _purchaseReceivalService, _salesOrderService, _deliveryOrderService);
                        contact.Errors.Count().should_be(0);
                    };
                };
        }
    }
}