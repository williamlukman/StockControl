using Core.DomainModel;
using Core.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interface.Service;
using Core.Interface.Validation;
using Data.Repository;
using Validation.Validation;

namespace Service.Service
{
    public class CashBankService : ICashBankService
    {
        private ICashBankRepository _repository;
        private ICashBankValidator _validator;
        public CashBankService(ICashBankRepository _cashBankRepository, ICashBankValidator _cashBankValidator)
        {
            _repository = _cashBankRepository;
            _validator = _cashBankValidator;
        }

        public ICashBankValidator GetValidator()
        {
            return _validator;
        }

        public IList<CashBank> GetAll()
        {
            return _repository.GetAll();
        }

        public CashBank GetObjectById(int Id)
        {
            return _repository.GetObjectById(Id);
        }

        public CashBank GetObjectByName(string name)
        {
            return _repository.FindAll(c => c.Name == name && !c.IsDeleted).FirstOrDefault();
        }

        public CashBank CreateObject(string name, string description, bool isBank, decimal amount)
        {
            CashBank c = new CashBank
            {
                Name = name,
                Description = description,
                IsBank = isBank,
                Amount = amount
            };
            return this.CreateObject(c);
        }

        public CashBank CreateObject(CashBank cashBank)
        {
            cashBank.Errors = new Dictionary<String, String>();
            return (_validator.ValidCreateObject(cashBank, this) ? _repository.CreateObject(cashBank) : cashBank);
        }

        public CashBank UpdateObject(CashBank cashBank)
        {
            return (cashBank = _validator.ValidUpdateObject(cashBank, this) ? _repository.UpdateObject(cashBank) : cashBank);
        }

        public CashBank SoftDeleteObject(CashBank cashBank, IReceiptVoucherService _rvs, IPaymentVoucherService _pvs)
        {
            return (cashBank = _validator.ValidDeleteObject(cashBank, this, _rvs, _pvs) ? _repository.SoftDeleteObject(cashBank) : cashBank);
        }

        public bool DeleteObject(int Id)
        {
            return _repository.DeleteObject(Id);
        }

        public bool IsNameDuplicated(String name)
        {
            IQueryable<CashBank> cashbank = _repository.FindAll(cb => cb.Name == name && !cb.IsDeleted);
            return (cashbank.Count() > 1 ? true : false);
        }

    }
}