using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DomainModel;
using Core.Interface.Service;
using Core.Interface.Repository;

namespace Core.Interface.Validation
{
    public interface IReceivableValidator
    {
        Receivable VCreateObject(Receivable p, IReceivableService _p);
        Receivable VUpdateObject(Receivable p, IReceivableService _p);
        Receivable VDeleteObject(Receivable p);
        bool ValidCreateObject(Receivable p, IReceivableService _p);
        bool ValidUpdateObject(Receivable p, IReceivableService _p);
        bool ValidDeleteObject(Receivable p);
        bool isValid(Receivable p);
        string PrintError(Receivable p);
    }
}
