using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Core.Interface.Validation;
using Core.DomainModel;
using Core.Interface.Service;
using Core.Constant;

namespace Validation.Validation
{
    public class PurchaseReceivalValidator : IPurchaseReceivalValidator
    {

        public PurchaseReceival VCustomer(PurchaseReceival pr, IContactService _cs)
        {
            Contact c = _cs.GetObjectById(pr.CustomerId);
            if (c == null)
            {
                pr.Errors.Add("Error. Customer does not exist");
            }
            return pr;
        }

        public PurchaseReceival VReceivalDate(PurchaseReceival pr)
        {
            /* receivalDate is never null
            if (pr.ReceivalDate == null)
            {
                pr.Errors.Add("Error. Receival Date does not exist");
            }
            */
            return pr;
        }

        public PurchaseReceival VIsConfirmed(PurchaseReceival pr)
        {
            if (pr.IsConfirmed)
            {
                pr.Errors.Add("Error. Purchase Receival is confirmed already");
            }
            return pr;
        }

        public PurchaseReceival VHasPurchaseReceivalDetails(PurchaseReceival pr, IPurchaseReceivalDetailService _prds)
        {
            IList<PurchaseReceivalDetail> details = _prds.GetObjectsByPurchaseReceivalId(pr.Id);
            if (!details.Any())
            {
                pr.Errors.Add("Error. Purchase Receival does not have purchase receival details");
            }
            return pr;
        }

        public PurchaseReceival VCreateObject(PurchaseReceival pr, IContactService _cs)
        {
            VCustomer(pr, _cs);
            VReceivalDate(pr);
            return pr;
        }

        public PurchaseReceival VUpdateObject(PurchaseReceival pr, IContactService _cs)
        {
            VCustomer(pr, _cs);
            VReceivalDate(pr);
            VIsConfirmed(pr);
            return pr;
        }

        public PurchaseReceival VDeleteObject(PurchaseReceival pr, IPurchaseReceivalDetailService _prds)
        {
            VConfirmObject(pr, _prds);
            return pr;
        }

        public PurchaseReceival VConfirmObject(PurchaseReceival pr, IPurchaseReceivalDetailService _prds)
        {
            VIsConfirmed(pr);
            VHasPurchaseReceivalDetails(pr, _prds);
            if (isValid(pr))
            {
                IList<PurchaseReceivalDetail> details = _prds.GetObjectsByPurchaseReceivalId(pr.Id);
                foreach (var detail in details)
                {
                    _prds.GetValidator().VConfirmObject(detail);
                    pr.Errors.UnionWith(detail.Errors);
                }
            }
            return pr;
        }

        public PurchaseReceival VUnconfirmObject(PurchaseReceival pr, IPurchaseReceivalDetailService _prds, IItemService _is)
        {            
            if (isValid(pr))
            {
                IList<PurchaseReceivalDetail> details = _prds.GetObjectsByPurchaseReceivalId(pr.Id);
                foreach (var detail in details)
                {
                    _prds.GetValidator().VUnconfirmObject(detail, _prds, _is);
                    pr.Errors.UnionWith(detail.Errors);
                }
            }

            return pr;
        }

        public bool ValidCreateObject(PurchaseReceival pr, IContactService _cs)
        {
            VCreateObject(pr, _cs);
            return isValid(pr);
        }

        public bool ValidUpdateObject(PurchaseReceival pr, IContactService _cs)
        {
            VUpdateObject(pr, _cs);
            return isValid(pr);
        }

        public bool ValidDeleteObject(PurchaseReceival pr, IPurchaseReceivalDetailService _prds)
        {
            VDeleteObject(pr, _prds);
            return isValid(pr);
        }

        public bool ValidConfirmObject(PurchaseReceival pr, IPurchaseReceivalDetailService _prds)
        {
            VConfirmObject(pr, _prds);
            return isValid(pr);
        }

        public bool ValidUnconfirmObject(PurchaseReceival pr, IPurchaseReceivalDetailService _prds, IItemService _is)
        {
            VUnconfirmObject(pr, _prds, _is);
            return isValid(pr);
        }

        public bool isValid(PurchaseReceival pr)
        {
            bool isValid = !pr.Errors.Any();
            return isValid;
        }

        public string PrintError(PurchaseReceival pr)
        {
            string erroroutput = "";
            foreach (var item in pr.Errors)
            {
                erroroutput += item;
            }
            return erroroutput;
        }
    }
}