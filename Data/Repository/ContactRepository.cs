using Core.DomainModel;
using Core.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Context;
using Data.Repository;

namespace Data.Repository
{
    public class ContactRepository : EfRepository<Contact>, IContactRepository
    {

        private StockControlEntities stocks;
        public ContactRepository()
        {
            stocks = new StockControlEntities();
        }

        public IList<Contact> GetAll()
        {
            List<Contact> contacts = (from i in stocks.Contacts
                                select i).ToList();

            return contacts;
        }

        public Contact GetObjectById(int Id)
        {
            Contact contact = (from i in stocks.Contacts
                         where i.Id == Id
                         select i).FirstOrDefault();
            return contact;
        }


        public Contact CreateObject(Contact contact)
        {
            Contact newcontact = new Contact();
            newcontact.Name = contact.Name;
            newcontact.Description = contact.Description;
            newcontact.IsDeleted = false;
            newcontact.CreatedAt = DateTime.Now;

            return Create(newcontact);
        }

        public Contact UpdateObject(Contact contact)
        {
            Contact updatecontact = new Contact();
            updatecontact.Name = contact.Name;
            updatecontact.Description = contact.Description;
            updatecontact.IsDeleted = contact.IsDeleted;
            updatecontact.ModifiedAt = DateTime.Now;
            Update(updatecontact);
            return updatecontact;
        }

        public Contact SoftDeleteObject(Contact contact)
        {
            contact.IsDeleted = true;
            contact.DeletedAt = DateTime.Now;
            Update(contact);
            return contact;
        }

        public bool DeleteObject(int Id)
        {
            Contact contact = Find(x => x.Id == Id);
            return (Delete(contact) == 1) ? true : false;
        }
        
    }
}