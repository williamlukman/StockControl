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

    public class SpecReceipt : nspec
    {
        IContactService _contactService;
        IItemService _itemService;
        IStockAdjustmentService _stockAdjustmentService;
        IStockAdjustmentDetailService _stockAdjustmentDetailService;
        ISalesOrderService _salesOrderService;
        ISalesOrderDetailService _salesOrderDetailService;
        IDeliveryOrderService _deliveryOrderService;
        IDeliveryOrderDetailService _deliveryOrderDetailService;
        IStockMutationService _stockMutationService;

        ICashBankService _cashBankService;
        IReceivableService _receivableService;
        IReceiptVoucherService _receiptVoucherService;
        IReceiptVoucherDetailService _receiptVoucherDetailService;
        ISalesInvoiceService _salesInvoiceService;
        ISalesInvoiceDetailService _salesInvoiceDetailService;

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
        SalesOrderDetail salesOrderDetail_batiktulis_po1;
        SalesOrderDetail salesOrderDetail_busway_po1;
        SalesOrderDetail salesOrderDetail_botolaqua_po1;
        SalesOrderDetail salesOrderDetail_batiktulis_po2;
        SalesOrderDetail salesOrderDetail_busway_po2;
        SalesOrderDetail salesOrderDetail_botolaqua_po2;
        DeliveryOrder deliveryOrder1;
        DeliveryOrder deliveryOrder2;
        DeliveryOrderDetail deliveryOrderDetail_batiktulis_pr1;
        DeliveryOrderDetail deliveryOrderDetail_busway_pr1;
        DeliveryOrderDetail deliveryOrderDetail_botolaqua_pr1;
        DeliveryOrderDetail deliveryOrderDetail_batiktulis_pr2a;
        DeliveryOrderDetail deliveryOrderDetail_batiktulis_pr2b;
        DeliveryOrderDetail deliveryOrderDetail_busway_pr2;
        DeliveryOrderDetail deliveryOrderDetail_botolaqua_pr2;

        CashBank cashBank_pettycash;
        CashBank cashBank_MandiriGiro;
        SalesInvoice salesInvoiceSO1;
        SalesInvoice salesInvoiceSO2;
        SalesInvoiceDetail salesInvoiceDetail_batiktulis_pr1;
        SalesInvoiceDetail salesInvoiceDetail_busway_pr1;
        SalesInvoiceDetail salesInvoiceDetail_botolaqua_pr1;
        SalesInvoiceDetail salesInvoiceDetail_batiktulis_pr2a;
        SalesInvoiceDetail salesInvoiceDetail_batiktulis_pr2b;
        SalesInvoiceDetail salesInvoiceDetail_busway_pr2;
        SalesInvoiceDetail salesInvoiceDetail_botolaqua_pr2;
        Receivable receivableSI1;
        Receivable receivableSI2;
        ReceiptVoucher receiptVoucher_batiktulis_cash;
        ReceiptVoucher receiptVoucher_SI1_busway_cheque;
        ReceiptVoucher receiptVoucher_pelunasan_sisaSI1_2_cheque;
        ReceiptVoucherDetail receiptVoucherDetail_batiktulis_pi1;
        ReceiptVoucherDetail receiptVoucherDetail_batiktulis_pi2;
        ReceiptVoucherDetail receiptVoucherDetail_SI1_busway_pi1;
        ReceiptVoucherDetail receiptVoucherDetail_pelunasan_busway_botolaqua_pi2;
        ReceiptVoucherDetail receiptVoucherDetail_pelunasan_botolaqua_pi1;

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
                _stockAdjustmentService = new StockAdjustmentService(new StockAdjustmentRepository(), new StockAdjustmentValidator());
                _stockAdjustmentDetailService = new StockAdjustmentDetailService(new StockAdjustmentDetailRepository(), new StockAdjustmentDetailValidator());
                _cashBankService = new CashBankService(new CashBankRepository(), new CashBankValidator());
                _receivableService = new ReceivableService(new ReceivableRepository(), new ReceivableValidator());
                _receiptVoucherService = new ReceiptVoucherService(new ReceiptVoucherRepository(), new ReceiptVoucherValidator());
                _receiptVoucherDetailService = new ReceiptVoucherDetailService(new ReceiptVoucherDetailRepository(), new ReceiptVoucherDetailValidator());
                _salesInvoiceService = new SalesInvoiceService(new SalesInvoiceRepository(), new SalesInvoiceValidator());
                _salesInvoiceDetailService = new SalesInvoiceDetailService(new SalesInvoiceDetailRepository(), new SalesInvoiceDetailValidator());

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
                salesOrderDetail_batiktulis_po1 = _salesOrderDetailService.CreateObject(salesOrder1.Id, item_batiktulis.Id, 500, 2000000, _salesOrderService, _itemService);
                salesOrderDetail_busway_po1 = _salesOrderDetailService.CreateObject(salesOrder1.Id, item_busway.Id, 91, 800000000, _salesOrderService, _itemService);
                salesOrderDetail_botolaqua_po1 = _salesOrderDetailService.CreateObject(salesOrder1.Id, item_botolaqua.Id, 2000, 5000, _salesOrderService, _itemService);
                salesOrderDetail_batiktulis_po2 = _salesOrderDetailService.CreateObject(salesOrder2.Id, item_batiktulis.Id, 40, 2000500, _salesOrderService, _itemService);
                salesOrderDetail_busway_po2 = _salesOrderDetailService.CreateObject(salesOrder2.Id, item_busway.Id, 3, 810000000, _salesOrderService, _itemService);
                salesOrderDetail_botolaqua_po2 = _salesOrderDetailService.CreateObject(salesOrder2.Id, item_botolaqua.Id, 340, 5500, _salesOrderService, _itemService);
                salesOrder1 = _salesOrderService.ConfirmObject(salesOrder1, _salesOrderDetailService, _stockMutationService, _itemService);
                salesOrder2 = _salesOrderService.ConfirmObject(salesOrder2, _salesOrderDetailService, _stockMutationService, _itemService);

                deliveryOrder1 = _deliveryOrderService.CreateObject(contact.Id, new DateTime(2000, 1, 1), _contactService);
                deliveryOrder2 = _deliveryOrderService.CreateObject(contact.Id, new DateTime(2014, 5, 5), _contactService);
                deliveryOrderDetail_batiktulis_pr1 = _deliveryOrderDetailService.CreateObject(deliveryOrder1.Id, item_batiktulis.Id, 400, salesOrderDetail_batiktulis_po1.Id, _deliveryOrderService,
                                                                                              _salesOrderDetailService, _salesOrderService, _itemService, _contactService);
                deliveryOrderDetail_busway_pr1 = _deliveryOrderDetailService.CreateObject(deliveryOrder1.Id, item_busway.Id, 91, salesOrderDetail_busway_po1.Id, _deliveryOrderService,
                                                                                            _salesOrderDetailService, _salesOrderService, _itemService, _contactService);
                deliveryOrderDetail_botolaqua_pr1 = _deliveryOrderDetailService.CreateObject(deliveryOrder1.Id, item_botolaqua.Id, 2000, salesOrderDetail_botolaqua_po1.Id, _deliveryOrderService,
                                                                                              _salesOrderDetailService, _salesOrderService, _itemService, _contactService);
                deliveryOrderDetail_batiktulis_pr2a = _deliveryOrderDetailService.CreateObject(deliveryOrder2.Id, item_batiktulis.Id, 100, salesOrderDetail_batiktulis_po1.Id, _deliveryOrderService,
                                                                                                                      _salesOrderDetailService, _salesOrderService, _itemService, _contactService);
                deliveryOrderDetail_batiktulis_pr2b = _deliveryOrderDetailService.CreateObject(deliveryOrder2.Id, item_batiktulis.Id, 40, salesOrderDetail_batiktulis_po2.Id, _deliveryOrderService,
                                                                                                                      _salesOrderDetailService, _salesOrderService, _itemService, _contactService);
                deliveryOrderDetail_busway_pr2 = _deliveryOrderDetailService.CreateObject(deliveryOrder2.Id, item_busway.Id, 3, salesOrderDetail_busway_po2.Id, _deliveryOrderService,
                                                                                                                      _salesOrderDetailService, _salesOrderService, _itemService, _contactService);
                deliveryOrderDetail_botolaqua_pr2 = _deliveryOrderDetailService.CreateObject(deliveryOrder2.Id, item_botolaqua.Id, 340, salesOrderDetail_botolaqua_po2.Id, _deliveryOrderService,
                                                                                                                      _salesOrderDetailService, _salesOrderService, _itemService, _contactService);
                deliveryOrder1 = _deliveryOrderService.ConfirmObject(deliveryOrder1, _deliveryOrderDetailService, _salesOrderDetailService, _stockMutationService, _itemService);
                deliveryOrder2 = _deliveryOrderService.ConfirmObject(deliveryOrder2, _deliveryOrderDetailService, _salesOrderDetailService, _stockMutationService, _itemService);

            }
        }

        void receipt_validation()
        {
            it["validate_precondition_objects"] = () =>
            {
                contact.Errors.Count().should_be(0);
                item_batiktulis.Errors.Count().should_be(0);
                item_busway.Errors.Count().should_be(0);
                item_botolaqua.Errors.Count().should_be(0);
                salesOrder1.Errors.Count().should_be(0);
                salesOrder2.Errors.Count().should_be(0);
                deliveryOrder1.Errors.Count().should_be(0);
                deliveryOrder2.Errors.Count().should_be(0);
            };

            context["when_creating_purchaseinvoice"] = () =>
            {
                before = () =>
                {
                    cashBank_pettycash = _cashBankService.CreateObject("SSD Petty Cash", "Quick cash", false, 1080020000L);
                    cashBank_MandiriGiro = _cashBankService.CreateObject("SSD Acc. Mandiri", "GIRO, Check", true, 200000000000L);
                    decimal InvoiceAmountSO1 = (500 * 2000000) + (91 * 800000000L) + (2000 * 5000);
                    decimal InvoiceAmountSO2 = (40 * 2000500) + (3 * 810000000L) + (340 * 5500);
                    salesInvoiceSO1 = _salesInvoiceService.CreateObject(contact.Id, "SO1", InvoiceAmountSO1, _contactService);
                    salesInvoiceSO2 = _salesInvoiceService.CreateObject(contact.Id, "SO2", InvoiceAmountSO2, _contactService);
                    salesInvoiceDetail_batiktulis_pr1 = _salesInvoiceDetailService.CreateObject(salesInvoiceSO1.Id, deliveryOrderDetail_batiktulis_pr1.Id, 400, 2000000,
                                                                                                       _salesInvoiceService, _deliveryOrderDetailService);
                    salesInvoiceDetail_busway_pr1 = _salesInvoiceDetailService.CreateObject(salesInvoiceSO1.Id, deliveryOrderDetail_busway_pr1.Id, 91, 800000000L,
                                                                                                       _salesInvoiceService, _deliveryOrderDetailService);
                    salesInvoiceDetail_botolaqua_pr1 = _salesInvoiceDetailService.CreateObject(salesInvoiceSO1.Id, deliveryOrderDetail_botolaqua_pr1.Id, 2000,  5000,
                                                                                                       _salesInvoiceService, _deliveryOrderDetailService);
                    salesInvoiceDetail_batiktulis_pr2a = _salesInvoiceDetailService.CreateObject(salesInvoiceSO1.Id, deliveryOrderDetail_batiktulis_pr2a.Id, 100, 2000000,
                                                                                                       _salesInvoiceService, _deliveryOrderDetailService);
                    salesInvoiceDetail_batiktulis_pr2b = _salesInvoiceDetailService.CreateObject(salesInvoiceSO1.Id, deliveryOrderDetail_batiktulis_pr2b.Id, 40, 2000000,
                                                                                                       _salesInvoiceService, _deliveryOrderDetailService);
                    salesInvoiceDetail_busway_pr2 = _salesInvoiceDetailService.CreateObject(salesInvoiceSO2.Id, deliveryOrderDetail_busway_pr2.Id, 3, 810000000L,
                                                                                                       _salesInvoiceService, _deliveryOrderDetailService);
                    salesInvoiceDetail_botolaqua_pr2 = _salesInvoiceDetailService.CreateObject(salesInvoiceSO2.Id, deliveryOrderDetail_botolaqua_pr2.Id, 340, 5500,
                                                                                                       _salesInvoiceService, _deliveryOrderDetailService);
                    receivableSI1 = _receivableService.CreateObject(contact.Id, Constant.ReceivableSource.SalesInvoice, salesInvoiceSO1.Id, salesInvoiceSO1.TotalAmount);
                    receivableSI2 = _receivableService.CreateObject(contact.Id, Constant.ReceivableSource.SalesInvoice, salesInvoiceSO2.Id, salesInvoiceSO2.TotalAmount);
                    salesInvoiceSO1 = _salesInvoiceService.ConfirmObject(salesInvoiceSO1, _salesInvoiceDetailService, _deliveryOrderDetailService, _receivableService);
                    salesInvoiceSO2 = _salesInvoiceService.ConfirmObject(salesInvoiceSO2, _salesInvoiceDetailService, _deliveryOrderDetailService, _receivableService);
                };

                it["validation_purchaseinvoices"] = () =>
                {
                    salesInvoiceSO1.Errors.Count().should_be(0);
                    salesInvoiceSO2.Errors.Count().should_be(0);
                    receivableSI1.Errors.Count().should_be(0);
                    receivableSI2.Errors.Count().should_be(0);
                    cashBank_MandiriGiro.Errors.Count().should_be(0);
                    cashBank_pettycash.Errors.Count().should_be(0);
                };

                it["validates_receiptvoucher"] = () =>
                {
                    receiptVoucher_batiktulis_cash = _receiptVoucherService.CreateObject(cashBank_pettycash.Id, contact.Id, DateTime.Now, (500 * 2000000) + (40 * 2000500), _receiptVoucherDetailService, _receivableService, _contactService, _cashBankService);
                    receiptVoucherDetail_batiktulis_pi1 = _receiptVoucherDetailService.CreateObject(receiptVoucher_batiktulis_cash.Id, receivableSI1.Id, (500 * 2000000), "Pembayaran SO1 untuk 400pcs @2000000",
                                                                                                    _receiptVoucherService, _cashBankService, _receivableService, _contactService);
                    receiptVoucherDetail_batiktulis_pi2 = _receiptVoucherDetailService.CreateObject(receiptVoucher_batiktulis_cash.Id, receivableSI2.Id, (40 * 2000500), "Pembayaran SO2 untuk 140pcs @2000500",
                                                                                                                                _receiptVoucherService, _cashBankService, _receivableService, _contactService);

                    receiptVoucher_SI1_busway_cheque = _receiptVoucherService.CreateObject(cashBank_MandiriGiro.Id, contact.Id, DateTime.Now, (91 * 800000000L), false, _receiptVoucherDetailService, _receivableService, _contactService, _cashBankService);
                    receiptVoucherDetail_SI1_busway_pi1 = _receiptVoucherDetailService.CreateObject(receiptVoucher_SI1_busway_cheque.Id, receivableSI1.Id, (91 * 800000000L), "Pembayaran SO1 untuk 91 busway @800000000",
                                                                                                    _receiptVoucherService, _cashBankService, _receivableService, _contactService);

                    receiptVoucher_pelunasan_sisaSI1_2_cheque = _receiptVoucherService.CreateObject(cashBank_MandiriGiro.Id, contact.Id, DateTime.Now, (3 * 810000000L) + (2000 * 5000) + (340 * 5500), _receiptVoucherDetailService, _receivableService, _contactService, _cashBankService);
                    receiptVoucherDetail_pelunasan_botolaqua_pi1 = _receiptVoucherDetailService.CreateObject(receiptVoucher_pelunasan_sisaSI1_2_cheque.Id, receivableSI1.Id, (2000 * 5000), "Pembayaran SO1 untuk 2000pcs @ 5000",
                                                                                                        _receiptVoucherService, _cashBankService, _receivableService, _contactService);
                    receiptVoucherDetail_pelunasan_busway_botolaqua_pi2 = _receiptVoucherDetailService.CreateObject(receiptVoucher_pelunasan_sisaSI1_2_cheque.Id, receivableSI2.Id, (3 * 810000000L) + (340 * 5500), "Pembayaran SO2 untuk 340pcs @ 5500 + 3 busway @810000000",
                                                                                                        _receiptVoucherService, _cashBankService, _receivableService, _contactService);

                    receiptVoucherDetail_batiktulis_pi1.Errors.Count().should_be(0);
                    receiptVoucherDetail_batiktulis_pi2.Errors.Count().should_be(0);
                    receiptVoucher_SI1_busway_cheque.Errors.Count().should_be(0);
                    receiptVoucherDetail_SI1_busway_pi1.Errors.Count().should_be(0);
                    receiptVoucher_pelunasan_sisaSI1_2_cheque.Errors.Count().should_be(0);
                    receiptVoucherDetail_pelunasan_botolaqua_pi1.Errors.Count().should_be(0);
                    receiptVoucherDetail_pelunasan_busway_botolaqua_pi2.Errors.Count().should_be(0);
                };

                context["when_creating_receipt"] = () =>
                {
                    before = () =>
                    {

                        receiptVoucher_batiktulis_cash = _receiptVoucherService.CreateObject(cashBank_pettycash.Id, contact.Id, DateTime.Now, (500 * 2000000) + (40 * 2000500), _receiptVoucherDetailService, _receivableService, _contactService, _cashBankService);
                        receiptVoucherDetail_batiktulis_pi1 = _receiptVoucherDetailService.CreateObject(receiptVoucher_batiktulis_cash.Id, receivableSI1.Id, (500 * 2000000), "Pembayaran SO1 untuk 400pcs @2000000", _receiptVoucherService, _cashBankService, _receivableService, _contactService);
                        receiptVoucherDetail_batiktulis_pi2 = _receiptVoucherDetailService.CreateObject(receiptVoucher_batiktulis_cash.Id, receivableSI2.Id, (40 * 2000500), "Pembayaran SO2 untuk 140pcs @2000000", _receiptVoucherService, _cashBankService, _receivableService, _contactService);

                        receiptVoucher_SI1_busway_cheque = _receiptVoucherService.CreateObject(cashBank_MandiriGiro.Id, contact.Id, DateTime.Now, (91 * 800000000L), false, _receiptVoucherDetailService, _receivableService, _contactService, _cashBankService);
                        receiptVoucherDetail_SI1_busway_pi1 = _receiptVoucherDetailService.CreateObject(receiptVoucher_SI1_busway_cheque.Id, receivableSI1.Id, (91 * 800000000L), "Pembayaran SO1 untuk 91 busway @800000000", _receiptVoucherService, _cashBankService, _receivableService, _contactService);

                        receiptVoucher_pelunasan_sisaSI1_2_cheque = _receiptVoucherService.CreateObject(cashBank_MandiriGiro.Id, contact.Id, DateTime.Now, (3 * 810000000L) + (2000 * 5000) + (340 * 5500), _receiptVoucherDetailService, _receivableService, _contactService, _cashBankService);
                        receiptVoucherDetail_pelunasan_botolaqua_pi1 = _receiptVoucherDetailService.CreateObject(receiptVoucher_pelunasan_sisaSI1_2_cheque.Id, receivableSI1.Id, (2000 * 5000), "Pembayaran SO1 untuk 2000pcs @ 5000", _receiptVoucherService, _cashBankService, _receivableService, _contactService);
                        receiptVoucherDetail_pelunasan_busway_botolaqua_pi2 = _receiptVoucherDetailService.CreateObject(receiptVoucher_pelunasan_sisaSI1_2_cheque.Id, receivableSI2.Id, (3 * 810000000L) + (340 * 5500), "Pembayaran SO2 untuk 340pcs @ 5500 + 3 busway @810000000", _receiptVoucherService, _cashBankService, _receivableService, _contactService);
                    };

                    it["confirms receiptvoucher"] = () =>
                    {
                        receiptVoucher_batiktulis_cash = _receiptVoucherService.ConfirmObject(receiptVoucher_batiktulis_cash, _receiptVoucherDetailService,
                                                                                              _cashBankService, _receivableService, _contactService);
                        receiptVoucher_SI1_busway_cheque = _receiptVoucherService.ConfirmObject(receiptVoucher_SI1_busway_cheque, _receiptVoucherDetailService,
                                                                                              _cashBankService, _receivableService, _contactService);
                        receiptVoucher_pelunasan_sisaSI1_2_cheque = _receiptVoucherService.ConfirmObject(receiptVoucher_pelunasan_sisaSI1_2_cheque, _receiptVoucherDetailService,
                                                                                              _cashBankService, _receivableService, _contactService);
                        receiptVoucher_batiktulis_cash.Errors.Count().should_be(0);
                        receiptVoucher_SI1_busway_cheque.Errors.Count().should_be(0);
                        receiptVoucher_pelunasan_sisaSI1_2_cheque.Errors.Count().should_be(0);
                    };

                    it["validates_the_amount"] = () =>
                    {
                        receiptVoucher_batiktulis_cash = _receiptVoucherService.ConfirmObject(receiptVoucher_batiktulis_cash, _receiptVoucherDetailService,
                                                                                              _cashBankService, _receivableService, _contactService);
                        receiptVoucher_SI1_busway_cheque = _receiptVoucherService.ConfirmObject(receiptVoucher_SI1_busway_cheque, _receiptVoucherDetailService,
                                                                                              _cashBankService, _receivableService, _contactService);
                        receiptVoucher_pelunasan_sisaSI1_2_cheque = _receiptVoucherService.ConfirmObject(receiptVoucher_pelunasan_sisaSI1_2_cheque, _receiptVoucherDetailService,
                                                                                              _cashBankService, _receivableService, _contactService);
                        receivableSI1.RemainingAmount.should_be(0);
                        receivableSI2.RemainingAmount.should_be(0);
                        receivableSI1.PendingClearanceAmount.should_be(receiptVoucherDetail_pelunasan_botolaqua_pi1.Amount + receiptVoucherDetail_SI1_busway_pi1.Amount);
                        receivableSI2.PendingClearanceAmount.should_be(receiptVoucherDetail_pelunasan_busway_botolaqua_pi2.Amount);
                    };
                };
            };
        }
    }
}