using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Repository
{
    public interface ICashBankRepository : IRepository<CashBank>
    {
        IList<CashBank> GetAll();
        CashBank GetObjectById(int Id);
        CashBank CreateObject(CashBank cashBank);
        CashBank UpdateObject(CashBank cashBank);
        CashBank SoftDeleteObject(CashBank cashBank);
        bool DeleteObject(int Id);
    }
}