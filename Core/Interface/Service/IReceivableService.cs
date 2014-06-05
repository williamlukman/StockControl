using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interface.Validation;

namespace Core.Interface.Service
{
    public interface IReceivableService
    {
        IReceivableValidator GetValidator();
        IList<Receivable> GetAll();
        IList<Receivable> GetObjectByContactId(int contactId);
        Receivable GetObjectById(int Id);
        Receivable CreateObject(Receivable payable);
        Receivable UpdateObject(Receivable payable);
        Receivable SoftDeleteObject(Receivable payable);
        bool DeleteObject(int Id);
    }
}