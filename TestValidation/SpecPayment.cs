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

    public class SpecPayment : nspec
    {
        IContactService _contactService;
        IItemService _itemService;
        IStockAdjustmentService _stockAdjustmentService;
        IStockAdjustmentDetailService _stockAdjustmentDetailService;
        ISalesOrderService _salesOrderService;
        ISalesOrderDetailService _salesOrderDetailService;
        IDeliveryOrderService _deliveryOrderService;
        IDeliveryOrderDetailService _deliveryOrderDetailService;
        IPurchaseOrderService _purchaseOrderService;
        IPurchaseOrderDetailService _purchaseOrderDetailService;
        IPurchaseReceivalService _purchaseReceivalService;
        IPurchaseReceivalDetailService _purchaseReceivalDetailService;
        IStockMutationService _stockMutationService;

        ICashBankService _cashBankService;
        IPayableService _payableService;
        IPaymentVoucherService _paymentVoucherService;
        IPaymentVoucherDetailService _paymentVoucherDetailService;
        IPurchaseInvoiceService _purchaseInvoiceService;
        IPurchaseInvoiceDetailService _purchaseInvoiceDetailService;

        Contact contact;
        Item item_batiktulis;
        Item item_busway;
        Item item_botolaqua;

        StockAdjustment stockAdjustment;
        StockAdjustmentDetail stockAdjust_batiktulis;
        StockAdjustmentDetail stockAdjust_busway;
        StockAdjustmentDetail stockAdjust_botolaqua;
        SalesOrder salesOrder;
        SalesOrderDetail salesOrderDetail1;
        SalesOrderDetail salesOrderDetail2;
        PurchaseOrder purchaseOrder1;
        PurchaseOrder purchaseOrder2;
        PurchaseOrderDetail purchaseOrderDetail_batiktulis_po1;
        PurchaseOrderDetail purchaseOrderDetail_busway_po1;
        PurchaseOrderDetail purchaseOrderDetail_botolaqua_po1;
        PurchaseOrderDetail purchaseOrderDetail_batiktulis_po2;
        PurchaseOrderDetail purchaseOrderDetail_busway_po2;
        PurchaseOrderDetail purchaseOrderDetail_botolaqua_po2;
        PurchaseReceival purchaseReceival1;
        PurchaseReceival purchaseReceival2;
        PurchaseReceivalDetail purchaseReceivalDetail_batiktulis_pr1;
        PurchaseReceivalDetail purchaseReceivalDetail_busway_pr1;
        PurchaseReceivalDetail purchaseReceivalDetail_botolaqua_pr1;
        PurchaseReceivalDetail purchaseReceivalDetail_batiktulis_pr2a;
        PurchaseReceivalDetail purchaseReceivalDetail_batiktulis_pr2b;
        PurchaseReceivalDetail purchaseReceivalDetail_busway_pr2;
        PurchaseReceivalDetail purchaseReceivalDetail_botolaqua_pr2;

        CashBank cashBank_pettycash;
        CashBank cashBank_MandiriGiro;
        PurchaseInvoice purchaseInvoicePO1;
        PurchaseInvoice purchaseInvoicePO2;
        PurchaseInvoiceDetail purchaseInvoiceDetail_batiktulis_pr1;
        PurchaseInvoiceDetail purchaseInvoiceDetail_busway_pr1;
        PurchaseInvoiceDetail purchaseInvoiceDetail_botolaqua_pr1;
        PurchaseInvoiceDetail purchaseInvoiceDetail_batiktulis_pr2a;
        PurchaseInvoiceDetail purchaseInvoiceDetail_batiktulis_pr2b;
        PurchaseInvoiceDetail purchaseInvoiceDetail_busway_pr2;
        PurchaseInvoiceDetail purchaseInvoiceDetail_botolaqua_pr2;
        Payable payablePI1;
        Payable payablePI2;
        PaymentVoucher paymentVoucher_batiktulis_cash;
        PaymentVoucher paymentVoucher_PI1_busway_cheque;
        PaymentVoucher paymentVoucher_pelunasan_sisaPI1_2_cheque;
        PaymentVoucherDetail paymentVoucherDetail_batiktulis_pi1;
        PaymentVoucherDetail paymentVoucherDetail_batiktulis_pi2;
        PaymentVoucherDetail paymentVoucherDetail_PI1_busway_pi1;
        PaymentVoucherDetail paymentVoucherDetail_pelunasan_busway_botolaqua_pi2;
        PaymentVoucherDetail paymentVoucherDetail_pelunasan_botolaqua_pi1;

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
                _purchaseOrderService = new PurchaseOrderService(new PurchaseOrderRepository(), new PurchaseOrderValidator());
                _purchaseOrderDetailService = new PurchaseOrderDetailService(new PurchaseOrderDetailRepository(), new PurchaseOrderDetailValidator());
                _purchaseReceivalService = new PurchaseReceivalService(new PurchaseReceivalRepository(), new PurchaseReceivalValidator());
                _purchaseReceivalDetailService = new PurchaseReceivalDetailService(new PurchaseReceivalDetailRepository(), new PurchaseReceivalDetailValidator());
                _stockMutationService = new StockMutationService(new StockMutationRepository(), new StockMutationValidator());
                _stockAdjustmentService = new StockAdjustmentService(new StockAdjustmentRepository(), new StockAdjustmentValidator());
                _stockAdjustmentDetailService = new StockAdjustmentDetailService(new StockAdjustmentDetailRepository(), new StockAdjustmentDetailValidator());
                _cashBankService = new CashBankService(new CashBankRepository(), new CashBankValidator());
                _payableService = new PayableService(new PayableRepository(), new PayableValidator());
                _paymentVoucherService = new PaymentVoucherService(new PaymentVoucherRepository(), new PaymentVoucherValidator());
                _paymentVoucherDetailService = new PaymentVoucherDetailService(new PaymentVoucherDetailRepository(), new PaymentVoucherDetailValidator());
                _purchaseInvoiceService = new PurchaseInvoiceService(new PurchaseInvoiceRepository(), new PurchaseInvoiceValidator());
                _purchaseInvoiceDetailService = new PurchaseInvoiceDetailService(new PurchaseInvoiceDetailRepository(), new PurchaseInvoiceDetailValidator());

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
                purchaseOrderDetail_batiktulis_po1 = _purchaseOrderDetailService.CreateObject(purchaseOrder1.Id, item_batiktulis.Id, 500, 2000000, _purchaseOrderService, _itemService);
                purchaseOrderDetail_busway_po1 = _purchaseOrderDetailService.CreateObject(purchaseOrder1.Id, item_busway.Id, 91, 800000000, _purchaseOrderService, _itemService);
                purchaseOrderDetail_botolaqua_po1 = _purchaseOrderDetailService.CreateObject(purchaseOrder1.Id, item_botolaqua.Id, 2000, 5000, _purchaseOrderService, _itemService);
                purchaseOrderDetail_batiktulis_po2 = _purchaseOrderDetailService.CreateObject(purchaseOrder2.Id, item_batiktulis.Id, 40, 2000500, _purchaseOrderService, _itemService);
                purchaseOrderDetail_busway_po2 = _purchaseOrderDetailService.CreateObject(purchaseOrder2.Id, item_busway.Id, 3, 810000000, _purchaseOrderService, _itemService);
                purchaseOrderDetail_botolaqua_po2 = _purchaseOrderDetailService.CreateObject(purchaseOrder2.Id, item_botolaqua.Id, 340, 5500, _purchaseOrderService, _itemService);
                purchaseOrder1 = _purchaseOrderService.ConfirmObject(purchaseOrder1, _purchaseOrderDetailService, _stockMutationService, _itemService);
                purchaseOrder2 = _purchaseOrderService.ConfirmObject(purchaseOrder2, _purchaseOrderDetailService, _stockMutationService, _itemService);

                purchaseReceival1 = _purchaseReceivalService.CreateObject(contact.Id, new DateTime(2000, 1, 1), _contactService);
                purchaseReceival2 = _purchaseReceivalService.CreateObject(contact.Id, new DateTime(2014, 5, 5), _contactService);
                purchaseReceivalDetail_batiktulis_pr1 = _purchaseReceivalDetailService.CreateObject(purchaseReceival1.Id, item_batiktulis.Id, 400, purchaseOrderDetail_batiktulis_po1.Id, _purchaseReceivalService,
                                                                                              _purchaseOrderDetailService, _purchaseOrderService, _itemService, _contactService);
                purchaseReceivalDetail_busway_pr1 = _purchaseReceivalDetailService.CreateObject(purchaseReceival1.Id, item_busway.Id, 91, purchaseOrderDetail_busway_po1.Id, _purchaseReceivalService,
                                                                                            _purchaseOrderDetailService, _purchaseOrderService, _itemService, _contactService);
                purchaseReceivalDetail_botolaqua_pr1 = _purchaseReceivalDetailService.CreateObject(purchaseReceival1.Id, item_botolaqua.Id, 2000, purchaseOrderDetail_botolaqua_po1.Id, _purchaseReceivalService,
                                                                                              _purchaseOrderDetailService, _purchaseOrderService, _itemService, _contactService);
                purchaseReceivalDetail_batiktulis_pr2a = _purchaseReceivalDetailService.CreateObject(purchaseReceival2.Id, item_batiktulis.Id, 100, purchaseOrderDetail_batiktulis_po1.Id, _purchaseReceivalService,
                                                                                                                      _purchaseOrderDetailService, _purchaseOrderService, _itemService, _contactService);
                purchaseReceivalDetail_batiktulis_pr2b = _purchaseReceivalDetailService.CreateObject(purchaseReceival2.Id, item_batiktulis.Id, 40, purchaseOrderDetail_batiktulis_po2.Id, _purchaseReceivalService,
                                                                                                                      _purchaseOrderDetailService, _purchaseOrderService, _itemService, _contactService);
                purchaseReceivalDetail_busway_pr2 = _purchaseReceivalDetailService.CreateObject(purchaseReceival2.Id, item_busway.Id, 3, purchaseOrderDetail_busway_po2.Id, _purchaseReceivalService,
                                                                                                                      _purchaseOrderDetailService, _purchaseOrderService, _itemService, _contactService);
                purchaseReceivalDetail_botolaqua_pr2 = _purchaseReceivalDetailService.CreateObject(purchaseReceival2.Id, item_botolaqua.Id, 340, purchaseOrderDetail_botolaqua_po2.Id, _purchaseReceivalService,
                                                                                                                      _purchaseOrderDetailService, _purchaseOrderService, _itemService, _contactService);
                purchaseReceival1 = _purchaseReceivalService.ConfirmObject(purchaseReceival1, _purchaseReceivalDetailService, _purchaseOrderDetailService, _stockMutationService, _itemService);
                purchaseReceival2 = _purchaseReceivalService.ConfirmObject(purchaseReceival2, _purchaseReceivalDetailService, _purchaseOrderDetailService, _stockMutationService, _itemService);

            }
        }

        void payment_validation()
        {
            it["validate_precondition_objects"] = () =>
            {
                contact.Errors.Count().should_be(0);
                item_batiktulis.Errors.Count().should_be(0);
                item_busway.Errors.Count().should_be(0);
                item_botolaqua.Errors.Count().should_be(0);
                purchaseOrder1.Errors.Count().should_be(0);
                purchaseOrder2.Errors.Count().should_be(0);
                purchaseReceival1.Errors.Count().should_be(0);
                purchaseReceival2.Errors.Count().should_be(0);
            };

            context["when_creating_purchaseinvoice"] = () =>
            {
                before = () =>
                {
                    cashBank_pettycash = _cashBankService.CreateObject("SSD Petty Cash", "Quick cash", false, 1080020000L);
                    cashBank_MandiriGiro = _cashBankService.CreateObject("SSD Acc. Mandiri", "GIRO, Check", true, 200000000000L);
                    decimal InvoiceAmountPO1 = (500 * 2000000) + (91 * 800000000L) + (2000 * 5000);
                    decimal InvoiceAmountPO2 = (40 * 2000500) + (3 * 810000000L) + (340 * 5500);
                    purchaseInvoicePO1 = _purchaseInvoiceService.CreateObject(contact.Id, "PO1", InvoiceAmountPO1, _contactService);
                    purchaseInvoicePO2 = _purchaseInvoiceService.CreateObject(contact.Id, "PO2", InvoiceAmountPO2, _contactService);
                    purchaseInvoiceDetail_batiktulis_pr1 = _purchaseInvoiceDetailService.CreateObject(purchaseInvoicePO1.Id, purchaseReceivalDetail_batiktulis_pr1.Id, 400, 2000000,
                                                                                                       _purchaseInvoiceService, _purchaseReceivalDetailService);
                    purchaseInvoiceDetail_busway_pr1 = _purchaseInvoiceDetailService.CreateObject(purchaseInvoicePO1.Id, purchaseReceivalDetail_busway_pr1.Id, 91, 800000000L,
                                                                                                       _purchaseInvoiceService, _purchaseReceivalDetailService);
                    purchaseInvoiceDetail_botolaqua_pr1 = _purchaseInvoiceDetailService.CreateObject(purchaseInvoicePO1.Id, purchaseReceivalDetail_botolaqua_pr1.Id, 2000,  5000,
                                                                                                       _purchaseInvoiceService, _purchaseReceivalDetailService);
                    purchaseInvoiceDetail_batiktulis_pr2a = _purchaseInvoiceDetailService.CreateObject(purchaseInvoicePO1.Id, purchaseReceivalDetail_batiktulis_pr2a.Id, 100, 2000000,
                                                                                                       _purchaseInvoiceService, _purchaseReceivalDetailService);
                    purchaseInvoiceDetail_batiktulis_pr2b = _purchaseInvoiceDetailService.CreateObject(purchaseInvoicePO1.Id, purchaseReceivalDetail_batiktulis_pr2b.Id, 40, 2000000,
                                                                                                       _purchaseInvoiceService, _purchaseReceivalDetailService);
                    purchaseInvoiceDetail_busway_pr2 = _purchaseInvoiceDetailService.CreateObject(purchaseInvoicePO2.Id, purchaseReceivalDetail_busway_pr2.Id, 3, 810000000L,
                                                                                                       _purchaseInvoiceService, _purchaseReceivalDetailService);
                    purchaseInvoiceDetail_botolaqua_pr2 = _purchaseInvoiceDetailService.CreateObject(purchaseInvoicePO2.Id, purchaseReceivalDetail_botolaqua_pr2.Id, 340, 5500,
                                                                                                       _purchaseInvoiceService, _purchaseReceivalDetailService);
                    payablePI1 = _payableService.CreateObject(contact.Id, Constant.PayableSource.PurchaseInvoice, purchaseInvoicePO1.Id, purchaseInvoicePO1.TotalAmount);
                    payablePI2 = _payableService.CreateObject(contact.Id, Constant.PayableSource.PurchaseInvoice, purchaseInvoicePO2.Id, purchaseInvoicePO2.TotalAmount);
                    purchaseInvoicePO1 = _purchaseInvoiceService.ConfirmObject(purchaseInvoicePO1, _purchaseInvoiceDetailService, _purchaseReceivalDetailService, _payableService);
                    purchaseInvoicePO2 = _purchaseInvoiceService.ConfirmObject(purchaseInvoicePO2, _purchaseInvoiceDetailService, _purchaseReceivalDetailService, _payableService);
                };

                it["validation_purchaseinvoices"] = () =>
                {
                    purchaseInvoicePO1.Errors.Count().should_be(0);
                    purchaseInvoicePO2.Errors.Count().should_be(0);
                    payablePI1.Errors.Count().should_be(0);
                    payablePI2.Errors.Count().should_be(0);
                    cashBank_MandiriGiro.Errors.Count().should_be(0);
                    cashBank_pettycash.Errors.Count().should_be(0);
                };

                it["validates_paymentvoucher"] = () =>
                {
                    paymentVoucher_batiktulis_cash = _paymentVoucherService.CreateObject(cashBank_pettycash.Id, contact.Id, DateTime.Now, (500 * 2000000) + (40 * 2000500), _paymentVoucherDetailService, _payableService, _contactService, _cashBankService);
                    paymentVoucherDetail_batiktulis_pi1 = _paymentVoucherDetailService.CreateObject(paymentVoucher_batiktulis_cash.Id, payablePI1.Id, (500 * 2000000), "Pembayaran PO1 untuk 400pcs @2000000",
                                                                                                    _paymentVoucherService, _cashBankService, _payableService, _contactService);
                    paymentVoucherDetail_batiktulis_pi2 = _paymentVoucherDetailService.CreateObject(paymentVoucher_batiktulis_cash.Id, payablePI2.Id, (40 * 2000500), "Pembayaran PO2 untuk 140pcs @2000500",
                                                                                                                                _paymentVoucherService, _cashBankService, _payableService, _contactService);

                    paymentVoucher_PI1_busway_cheque = _paymentVoucherService.CreateObject(cashBank_MandiriGiro.Id, contact.Id, DateTime.Now, (91 * 800000000L), false, _paymentVoucherDetailService, _payableService, _contactService, _cashBankService);
                    paymentVoucherDetail_PI1_busway_pi1 = _paymentVoucherDetailService.CreateObject(paymentVoucher_PI1_busway_cheque.Id, payablePI1.Id, (91 * 800000000L), "Pembayaran PO1 untuk 91 busway @800000000",
                                                                                                    _paymentVoucherService, _cashBankService, _payableService, _contactService);

                    paymentVoucher_pelunasan_sisaPI1_2_cheque = _paymentVoucherService.CreateObject(cashBank_MandiriGiro.Id, contact.Id, DateTime.Now, (3 * 810000000L) + (2000 * 5000) + (340 * 5500), _paymentVoucherDetailService, _payableService, _contactService, _cashBankService);
                    paymentVoucherDetail_pelunasan_botolaqua_pi1 = _paymentVoucherDetailService.CreateObject(paymentVoucher_pelunasan_sisaPI1_2_cheque.Id, payablePI1.Id, (2000 * 5000), "Pembayaran PO1 untuk 2000pcs @ 5000",
                                                                                                        _paymentVoucherService, _cashBankService, _payableService, _contactService);
                    paymentVoucherDetail_pelunasan_busway_botolaqua_pi2 = _paymentVoucherDetailService.CreateObject(paymentVoucher_pelunasan_sisaPI1_2_cheque.Id, payablePI2.Id, (3 * 810000000L) + (340 * 5500), "Pembayaran PO2 untuk 340pcs @ 5500 + 3 busway @810000000",
                                                                                                        _paymentVoucherService, _cashBankService, _payableService, _contactService);

                    paymentVoucherDetail_batiktulis_pi1.Errors.Count().should_be(0);
                    paymentVoucherDetail_batiktulis_pi2.Errors.Count().should_be(0);
                    paymentVoucher_PI1_busway_cheque.Errors.Count().should_be(0);
                    paymentVoucherDetail_PI1_busway_pi1.Errors.Count().should_be(0);
                    paymentVoucher_pelunasan_sisaPI1_2_cheque.Errors.Count().should_be(0);
                    paymentVoucherDetail_pelunasan_botolaqua_pi1.Errors.Count().should_be(0);
                    paymentVoucherDetail_pelunasan_busway_botolaqua_pi2.Errors.Count().should_be(0);
                };

                context["when_creating_payment"] = () =>
                {
                    before = () =>
                    {

                        paymentVoucher_batiktulis_cash = _paymentVoucherService.CreateObject(cashBank_pettycash.Id, contact.Id, DateTime.Now, (500 * 2000000) + (40 * 2000500), _paymentVoucherDetailService, _payableService, _contactService, _cashBankService);
                        paymentVoucherDetail_batiktulis_pi1 = _paymentVoucherDetailService.CreateObject(paymentVoucher_batiktulis_cash.Id, payablePI1.Id, (500 * 2000000), "Pembayaran PO1 untuk 400pcs @2000000", _paymentVoucherService, _cashBankService, _payableService, _contactService);
                        paymentVoucherDetail_batiktulis_pi2 = _paymentVoucherDetailService.CreateObject(paymentVoucher_batiktulis_cash.Id, payablePI2.Id, (40 * 2000500), "Pembayaran PO2 untuk 140pcs @2000000", _paymentVoucherService, _cashBankService, _payableService, _contactService);

                        paymentVoucher_PI1_busway_cheque = _paymentVoucherService.CreateObject(cashBank_MandiriGiro.Id, contact.Id, DateTime.Now, (91 * 800000000L), false, _paymentVoucherDetailService, _payableService, _contactService, _cashBankService);
                        paymentVoucherDetail_PI1_busway_pi1 = _paymentVoucherDetailService.CreateObject(paymentVoucher_PI1_busway_cheque.Id, payablePI1.Id, (91 * 800000000L), "Pembayaran PO1 untuk 91 busway @800000000", _paymentVoucherService, _cashBankService, _payableService, _contactService);

                        paymentVoucher_pelunasan_sisaPI1_2_cheque = _paymentVoucherService.CreateObject(cashBank_MandiriGiro.Id, contact.Id, DateTime.Now, (3 * 810000000L) + (2000 * 5000) + (340 * 5500), _paymentVoucherDetailService, _payableService, _contactService, _cashBankService);
                        paymentVoucherDetail_pelunasan_botolaqua_pi1 = _paymentVoucherDetailService.CreateObject(paymentVoucher_pelunasan_sisaPI1_2_cheque.Id, payablePI1.Id, (2000 * 5000), "Pembayaran PO1 untuk 2000pcs @ 5000", _paymentVoucherService, _cashBankService, _payableService, _contactService);
                        paymentVoucherDetail_pelunasan_busway_botolaqua_pi2 = _paymentVoucherDetailService.CreateObject(paymentVoucher_pelunasan_sisaPI1_2_cheque.Id, payablePI2.Id, (3 * 810000000L) + (340 * 5500), "Pembayaran PO2 untuk 340pcs @ 5500 + 3 busway @810000000", _paymentVoucherService, _cashBankService, _payableService, _contactService);
                    };

                    it["confirms paymentvoucher"] = () =>
                    {
                        paymentVoucher_batiktulis_cash = _paymentVoucherService.ConfirmObject(paymentVoucher_batiktulis_cash, _paymentVoucherDetailService,
                                                                                              _cashBankService, _payableService, _contactService);
                        paymentVoucher_PI1_busway_cheque = _paymentVoucherService.ConfirmObject(paymentVoucher_PI1_busway_cheque, _paymentVoucherDetailService,
                                                                                              _cashBankService, _payableService, _contactService);
                        paymentVoucher_pelunasan_sisaPI1_2_cheque = _paymentVoucherService.ConfirmObject(paymentVoucher_pelunasan_sisaPI1_2_cheque, _paymentVoucherDetailService,
                                                                                              _cashBankService, _payableService, _contactService);
                        paymentVoucher_batiktulis_cash.Errors.Count().should_be(0);
                        paymentVoucher_PI1_busway_cheque.Errors.Count().should_be(0);
                        paymentVoucher_pelunasan_sisaPI1_2_cheque.Errors.Count().should_be(0);
                    };

                    it["validates_the_amount"] = () =>
                    {
                        paymentVoucher_batiktulis_cash = _paymentVoucherService.ConfirmObject(paymentVoucher_batiktulis_cash, _paymentVoucherDetailService,
                                                                                              _cashBankService, _payableService, _contactService);
                        paymentVoucher_PI1_busway_cheque = _paymentVoucherService.ConfirmObject(paymentVoucher_PI1_busway_cheque, _paymentVoucherDetailService,
                                                                                              _cashBankService, _payableService, _contactService);
                        paymentVoucher_pelunasan_sisaPI1_2_cheque = _paymentVoucherService.ConfirmObject(paymentVoucher_pelunasan_sisaPI1_2_cheque, _paymentVoucherDetailService,
                                                                                              _cashBankService, _payableService, _contactService);
                        payablePI1.RemainingAmount.should_be(0);
                        payablePI2.RemainingAmount.should_be(0);
                        payablePI1.PendingClearanceAmount.should_be(paymentVoucherDetail_pelunasan_botolaqua_pi1.Amount + paymentVoucherDetail_PI1_busway_pi1.Amount);
                        payablePI2.PendingClearanceAmount.should_be(paymentVoucherDetail_pelunasan_busway_botolaqua_pi2.Amount);
                    };
                };
            };
        }
    }
}