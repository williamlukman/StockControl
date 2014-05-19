using Core.DomainModel;
using Core.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Service
{
    public interface IContactService
    {
        IList<Contact> GetAll();
        Contact GetObjectById(int Id);
        Contact CreateObject(Contact contact);
        Contact UpdateObject(Contact contact);

        Contact SoftDeleteObject(Contact contact);
        bool DeleteObject(int Id);	
    }
}