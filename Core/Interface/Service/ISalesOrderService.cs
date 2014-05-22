using Core.DomainModel;
using Core.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Service
{
    public interface ISalesOrderService
    {
        IList<SalesOrder> GetAll();
        SalesOrder GetObjectById(int Id);
        SalesOrder CreateObject(SalesOrder salesOrder);
        SalesOrder UpdateObject(SalesOrder salesOrder);
        SalesOrder SoftDeleteObject(SalesOrder salesOrder);
        bool DeleteObject(int Id);
        SalesOrder ConfirmObject(SalesOrder salesOrder);
        SalesOrder UnconfirmObject(SalesOrder salesOrder);
    }
}