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
    public interface IPayableValidator
    {
        Payable VCreateObject(Payable p, IPayableService _p);
        Payable VUpdateObject(Payable p, IPayableService _p);
        Payable VDeleteObject(Payable p, IPayableService _p);
        bool ValidCreateObject(Payable p, IPayableService _p);
        bool ValidUpdateObject(Payable p, IPayableService _p);
        bool ValidDeleteObject(Payable p, IPayableService _p);
        bool isValid(Payable p);
        string PrintError(Payable p);
    }
}
