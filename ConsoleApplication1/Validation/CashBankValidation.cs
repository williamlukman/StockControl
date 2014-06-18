using Core.DomainModel;
using Core.Interface.Service;
using Core.Interface.Validation;
using Data.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validation.Validation;

namespace ConsoleApp.Validation
{
    public class CashBankValidation
    {
        private CashBankValidator _validator;
        private ICashBankService _cb;
        private IPaymentVoucherService _pvs;
        private IReceiptVoucherService _rvs;

        public CashBankValidation(CashBankValidator validator, ICashBankService cb,
                                  IPaymentVoucherService pvs, IReceiptVoucherService rvs)
        {
            _validator = validator;
            _cb = cb;
            _pvs = pvs;
            _rvs = rvs;
        }

        public void CashBankValidation1()
        {
            Console.WriteLine("     [CB 1] Create valid CashBank Mandiri");
            CashBank cb = new CashBank
            {
                Name = "Mandiri",
                Description = "Rekening No. 123456789",
                IsBank = true,
                Amount = 100000000
            };
            _cb.CreateObject(cb);
            if (cb.Errors.Any()) { Console.WriteLine("        >> " + _cb.GetValidator().PrintError(cb)); }
        }

        public void CashBankValidation2()
        {
            Console.WriteLine("     [CB 2] Create duplicate CashBank Mandiri");
            CashBank cb = new CashBank
            {
                Name = "Mandiri",
                Description = "Rekening No. 222222222",
                IsBank = true,
                Amount = 200000000
            };
            _cb.CreateObject(cb);
            if (cb.Errors.Any()) { Console.WriteLine("        >> " + _cb.GetValidator().PrintError(cb)); }
        }

        public void CashBankValidation3()
        {
            Console.WriteLine("     [CB 3] Create valid CashBank Pettycash");
            CashBank cb = new CashBank
            {
                Name = "SSD PettyCash",
                Description = "Laci William",
                IsBank = false,
                Amount = 5000000
            };
            _cb.CreateObject(cb);
            if (cb.Errors.Any()) { Console.WriteLine("        >> " + _cb.GetValidator().PrintError(cb)); }
        }

        public void CashBankValidation4()
        {
            Console.WriteLine("     [CB 3] Create valid CashBank Pettycash");
            CashBank cashBank = new CashBank()
            {
                Name = "Muamalat",
                Description = "Bank Bersama, GIRO, Check",
                Amount = 10000000
            };
            cashBank = _cb.CreateObject(cashBank);
            cashBank = _cb.SoftDeleteObject(cashBank, _rvs, _pvs);
        }
    }
}
