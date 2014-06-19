﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Core.Interface.Validation;
using Core.DomainModel;
using Core.Interface.Service;

namespace Validation.Validation
{
    public class PayableValidator : IPayableValidator
    {
        public Payable VCreateObject(Payable payable, IPayableService _payableService)
        {
            return payable;
        }

        public Payable VUpdateObject(Payable payable, IPayableService _payableService)
        {
            return payable;
        }

        public Payable VDeleteObject(Payable payable)
        {
            return payable;
        }

        public bool ValidCreateObject(Payable payable, IPayableService _payableService)
        {
            VCreateObject(payable, _payableService);
            return isValid(payable);
        }

        public bool ValidUpdateObject(Payable payable, IPayableService _payableService)
        {
            payable.Errors.Clear();
            VUpdateObject(payable, _payableService);
            return isValid(payable);
        }

        public bool ValidDeleteObject(Payable payable)
        {
            payable.Errors.Clear();
            VDeleteObject(payable);
            return isValid(payable);
        }

        public bool isValid(Payable obj)
        {
            bool isValid = !obj.Errors.Any();
            return isValid;
        }

        public string PrintError(Payable obj)
        {
            string erroroutput = "";
            KeyValuePair<string, string> first = obj.Errors.ElementAt(0);
            erroroutput += first.Key + "," + first.Value;
            foreach (KeyValuePair<string, string> pair in obj.Errors.Skip(1))
            {
                erroroutput += Environment.NewLine;
                erroroutput += pair.Key + "," + pair.Value;
            }
            return erroroutput;
        }

    }
}
