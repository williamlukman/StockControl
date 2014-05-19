using Core.DomainModel;
using Core.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interface.Service;
using Data.Repository;

namespace Service.Service
{
    public class ContactService : IContactService
    {
        private IContactRepository _c;
        public ContactService(IContactRepository _contactRepository)
        {
            _c = _contactRepository;
        }

        public IList<Contact> GetAll()
        {
            return _c.GetAll();
        }

        public Contact GetObjectById(int Id)
        {
            return _c.GetObjectById(Id);
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