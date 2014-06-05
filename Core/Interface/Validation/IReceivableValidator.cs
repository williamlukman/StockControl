using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DomainModel;
using Core.Interface.Service;

namespace Core.Interface.Validation
{
    public interface IReceivableValidator
    {
        Receivable VCreateObject(Receivable r, IReceivableService _r);
        Receivable VUpdateObject(Receivable r, IReceivableService _r);
        Receivable VDeleteObject(Receivable r, IReceivableService _r);
        bool ValidCreateObject(Receivable r, IReceivableService _r);
        bool ValidUpdateObject(Receivable r, IReceivableService _r);
        bool ValidDeleteObject(Receivable r, IReceivableService _r);
        bool isValid(Receivable r);
        string PrintError(Receivable r);
    }
}
