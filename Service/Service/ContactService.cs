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
    public class ContactService : IContactService
    {
        private IContactRepository _repository;
        private IContactValidator _validator;
        public ContactService(IContactRepository _contactRepository, IContactValidator _contactValidator)
        {
            _repository = _contactRepository;
            _validator = _contactValidator;
        }

        public IContactValidator GetValidator()
        {
            return _validator;
        }

        public IList<Contact> GetAll()
        {
            return _repository.GetAll();
        }

        public Contact GetObjectById(int Id)
        {
            return _repository.GetObjectById(Id);
        }

        public Contact GetObjectByName(string name)
        {
            return _repository.FindAll(c => c.Name == name && !c.IsDeleted).FirstOrDefault();
        }

        public Contact CreateObject(string name, string address)
        {
            Contact c = new Contact
            {
                Name = name,
                Address = address
            };
            return this.CreateObject(c);
        }

        public Contact CreateObject(Contact contact)
        {
            contact.Errors = new HashSet<string>();
            return (_validator.ValidCreateObject(contact) ? _repository.CreateObject(contact) : contact);
        }

        public Contact UpdateObject(Contact contact)
        {
            return (contact = _validator.ValidUpdateObject(contact) ? _repository.UpdateObject(contact) : contact);
        }

        public Contact SoftDeleteObject(Contact contact, IPurchaseOrderService _pos, IPurchaseReceivalService _prs, ISalesOrderService _sos, IDeliveryOrderService _dos)
        {
            return (contact = _validator.ValidDeleteObject(contact, _pos, _prs, _sos, _dos) ? _repository.SoftDeleteObject(contact) : contact);
        }

        public bool DeleteObject(int Id)
        {
            return _repository.DeleteObject(Id);
        }
    }
}