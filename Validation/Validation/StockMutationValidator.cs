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
    public class StockMutationValidator : IStockMutationValidator
    {
        
        public StockMutation VItemCase(StockMutation sm)
        {
            if (!sm.ItemCase.Equals (Constant.StockMutationItemCase.Ready) &&
                !sm.ItemCase.Equals (Constant.StockMutationItemCase.PendingDelivery) &&
                !sm.ItemCase.Equals (Constant.StockMutationItemCase.PendingReceival))
            {
                sm.Errors.Add("Error. Item Case did not meet system requirement");
            }
            return sm;
        }

        public StockMutation VStatus(StockMutation sm)
        {
            if (!sm.Status.Equals(Constant.StockMutationStatus.Addition) &&
                !sm.Status.Equals(Constant.StockMutationStatus.Deduction))
            {
                sm.Errors.Add("Error. Status did not meet system requirement");
            }
            return sm;
        }

        public StockMutation VSourceDocumentType(StockMutation sm)
        {
            if (!sm.SourceDocumentType.Equals(Constant.SourceDocumentType.PurchaseOrder) &&
                !sm.SourceDocumentType.Equals(Constant.SourceDocumentType.PurchaseReceival) &&
                !sm.SourceDocumentType.Equals(Constant.SourceDocumentType.SalesOrder) &&
                !sm.SourceDocumentType.Equals(Constant.SourceDocumentType.DeliveryOrder))
            {
                sm.Errors.Add("Error. Source Document Type did not meet system requirement");
            }
            return sm;
        }

        public StockMutation VSourceDocumentDetailType(StockMutation sm)
        {
            if (!sm.SourceDocumentDetailType.Equals(Constant.SourceDocumentDetailType.PurchaseOrderDetail) &&
                !sm.SourceDocumentDetailType.Equals(Constant.SourceDocumentDetailType.PurchaseReceivalDetail) &&
                !sm.SourceDocumentDetailType.Equals(Constant.SourceDocumentDetailType.SalesOrderDetail) &&
                !sm.SourceDocumentDetailType.Equals(Constant.SourceDocumentDetailType.DeliveryOrderDetail))
            {
                sm.Errors.Add("Error. Source Document Detail Type did not meet system requirement");
            }
            return sm;
        }

        public StockMutation VQuantity(StockMutation sm)
        {
            /*
             * value never reach null.
            if (sm.Quantity == null)
            {
                sm.Errors.Add("Error. Quantity is not set");
            }
            */
            return sm;
        }

        public StockMutation VCreateObject(StockMutation sm)
        {
            VItemCase(sm);
            VStatus(sm);
            VSourceDocumentType(sm);
            VSourceDocumentDetailType(sm);
            VQuantity(sm);
            return sm;
        }

        public StockMutation VDeleteObject(StockMutation sm)
        {
            return sm;
        }

        public bool ValidCreateObject(StockMutation sm)
        {
            VCreateObject(sm);
            return isValid(sm);
        }

        public bool ValidDeleteObject(StockMutation sm)
        {
            VDeleteObject(sm);
            return isValid(sm);
        }

        public bool isValid(StockMutation sm)
        {
            bool isValid = !sm.Errors.Any();
            return isValid;
        }

        public string PrintError(StockMutation sm)
        {
            string erroroutput = "";
            foreach (var item in sm.Errors)
            {
                erroroutput += item;
            }
            return erroroutput;
        }
    }
}
