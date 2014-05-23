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
        private IContactValidator _cvalidator;
        public ContactService(IContactRepository _contactRepository, IContactValidator _contactValidator)
        {
            _c = _contactRepository;
            _cvalidator = _contactValidator;
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
            return _c.CreateObject(contact);
        }

        public Contact UpdateObject(Contact contact)
        {
            return _c.UpdateObject(contact);
        }

        public Contact SoftDeleteObject(Contact contact)
        {
            return _c.SoftDeleteObject(contact);
        }

        public bool DeleteObject(int Id)
        {
            return _c.DeleteObject(Id);
        }
    }
}