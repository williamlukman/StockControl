using Core.DomainModel;
using Core.Interface.Repository;
using Core.Interface.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Service
{
    public interface IContactService
    {
        IContactValidator GetValidator();
        IList<Contact> GetAll();
        Contact GetObjectById(int Id);
        Contact GetObjectByName(string name);
        Contact CreateObject(string name, string address);
        Contact CreateObject(Contact contact);
        Contact UpdateObject(Contact contact);
        Contact SoftDeleteObject(Contact contact, IPurchaseOrderService _pos, IPurchaseReceivalService _prs, ISalesOrderService _sos, IDeliveryOrderService _dos);
        bool DeleteObject(int Id);	
    }
}