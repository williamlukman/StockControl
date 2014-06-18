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

    public class SpecCashBank : nspec
    {
        CashBank cashBank;
        ICashBankService _cashBankService;
        IReceiptVoucherService _receiptVoucherService;
        IReceiptVoucherDetailService _receiptVoucherDetailService;
        IReceivableService _receivableService;
        IPaymentVoucherService _paymentVoucherService;
        IPaymentVoucherDetailService _paymentVoucherDetailService;
        IPayableService _payableService;
        IContactService _contactService;
        void before_each()
        {
            var db = new StockControlEntities();
            using (db)
            {
                db.DeleteAllTables();
                _cashBankService = new CashBankService(new CashBankRepository(), new CashBankValidator());
                _receiptVoucherService = new ReceiptVoucherService(new ReceiptVoucherRepository(), new ReceiptVoucherValidator());
                _receiptVoucherDetailService = new ReceiptVoucherDetailService(new ReceiptVoucherDetailRepository(), new ReceiptVoucherDetailValidator());
                _receivableService = new ReceivableService(new ReceivableRepository(), new ReceivableValidator());
                _paymentVoucherService = new PaymentVoucherService(new PaymentVoucherRepository(), new PaymentVoucherValidator());
                _paymentVoucherDetailService = new PaymentVoucherDetailService(new PaymentVoucherDetailRepository(), new PaymentVoucherDetailValidator());
                _payableService = new PayableService(new PayableRepository(), new PayableValidator());
                _contactService = new ContactService(new ContactRepository(), new ContactValidator());
            }
        }

        void cb_validation()
        {
            it["create_cashbank"] = () =>
                {
                    cashBank = new CashBank()
                    {
                        Name = "Muamalat",
                        Description = "Bank Bersama, GIRO, Check",
                        Amount = 10000000
                    };
                    cashBank = _cashBankService.CreateObject(cashBank);
                    cashBank.Errors.Count().should_be(0);
                };

            it["create_invalid_cashbank_no_name"] = () =>
                {
                    cashBank = new CashBank()
                    {
                        Name = "         ",
                        Description = "Bank Bersama, GIRO, Check",
                        Amount = 7000000
                    };
                    cashBank = _cashBankService.CreateObject(cashBank);
                    cashBank.Errors.Count().should_not_be(0);
                };

            it["create_cashbank_with_negative_amount"] = () =>
                {
                    cashBank = new CashBank()
                    {
                        Name = "Mandiri",
                        Description = "Bank Bersama, GIRO, Check",
                        Amount = -7000000
                    };
                    cashBank = _cashBankService.CreateObject(cashBank);
                    cashBank.Errors.Count().should_be(0);
                };

            context["when deleting CashBank"] = () =>
                {
                    before = () =>
                        {
                            cashBank = new CashBank()
                            {
                                Name = "Muamalat",
                                Description = "Bank Bersama, GIRO, Check",
                                Amount = 10000000
                            };
                            cashBank = _cashBankService.CreateObject(cashBank);
                        };

                    it["deletes cashbank"] = () =>
                        {
                            cashBank = _cashBankService.SoftDeleteObject(cashBank, _receiptVoucherService, _paymentVoucherService);
                            cashBank.Errors.Count().should_be(0);
                        };

                    it["deletes cashbank with paymentvoucher"] = () =>
                        {
                            Contact contact = _contactService.CreateObject("Pak Abdul", "Punya Payment Voucher");
                            PaymentVoucher pv = _paymentVoucherService.CreateObject(cashBank.Id, contact.Id, DateTime.Now, 50000,
                                                                                    _paymentVoucherDetailService, _payableService, _contactService, _cashBankService);
                            _cashBankService.SoftDeleteObject(cashBank, _receiptVoucherService, _paymentVoucherService);
                            cashBank.Errors.Count().should_not_be(0);
                        };

                    it["deletes cashbank with receiptvoucher"] = () =>
                        {
                            Contact contact = _contactService.CreateObject("Pak Abdul", "Punya Payment Voucher");
                            ReceiptVoucher rv = _receiptVoucherService.CreateObject(cashBank.Id, contact.Id, DateTime.Now, 50000,
                                                                                    _receiptVoucherDetailService, _receivableService, _contactService, _cashBankService);
                            _cashBankService.SoftDeleteObject(cashBank, _receiptVoucherService, _paymentVoucherService);
                            cashBank.Errors.Count().should_not_be(0);
                        };
                };
        }
    }
}