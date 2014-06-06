using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Repository
{
    public interface IPayableRepository : IRepository<Payable>
    {
        IList<Payable> GetAll();
        IList<Payable> GetObjectsByContactId(int contactId);
        Payable GetObjectBySource(string PayableSource, int PayableSourceId); 
        Payable GetObjectById(int Id);
        Payable CreateObject(Payable payable);
        Payable UpdateObject(Payable payable);
        Payable SoftDeleteObject(Payable payable);
        bool DeleteObject(int Id);
    }
}