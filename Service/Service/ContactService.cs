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
        private IContactRepository _c;
        private IContactValidator _validator;
        public ContactService(IContactRepository _contactRepository, IContactValidator _contactValidator)
        {
            _c = _contactRepository;
            _validator = _contactValidator;
        }

        public IContactValidator GetValidator()
        {
            return _validator;
        }

        public IList<Contact> GetAll()
        {
            return _c.GetAll();
        }

        public Contact GetObjectById(int Id)
        {
            return _c.GetObjectById(Id);
        }

        public Contact GetObjectByName(string name)
        {
            return _c.FindAll(c => c.Name == name && !c.IsDeleted).FirstOrDefault();
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
            return (_validator.ValidCreateObject(contact) ? _c.CreateObject(contact) : contact);
        }

        public Contact UpdateObject(Contact contact)
        {
            return (_validator.ValidUpdateObject(contact) ? _c.UpdateObject(contact) : contact);
        }

        public Contact SoftDeleteObject(Contact contact, IPurchaseOrderService _pos, IPurchaseReceivalService _prs, ISalesOrderService _sos, IDeliveryOrderService _dos)
        {
            return (_validator.ValidDeleteObject(contact, _pos, _prs, _sos, _dos) ? _c.SoftDeleteObject(contact) : contact);
        }

        public bool DeleteObject(int Id)
        {
            return _c.DeleteObject(Id);
        }
    }
}