using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Repository
{
    public interface IReceivableRepository : IRepository<Receivable>
    {
        IList<Receivable> GetAll();
        IList<Receivable> GetObjectsByContactId(int contactId);
        Receivable GetObjectBySource(string ReceivableSource, int ReceivableSourceId); 
        Receivable GetObjectById(int Id);
        Receivable CreateObject(Receivable receivable);
        Receivable UpdateObject(Receivable receivable);
        Receivable SoftDeleteObject(Receivable receivable);
        bool DeleteObject(int Id);
    }
}