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
        IList<SalesOrder> GetObjectsByContactId(int contactId);
        SalesOrder CreateObject(SalesOrder salesOrder);
        SalesOrder CreateObject(int contactId, DateTime salesDate);
        SalesOrder UpdateObject(SalesOrder salesOrder);
        SalesOrder SoftDeleteObject(SalesOrder salesOrder);
        bool DeleteObject(int Id);
        SalesOrder ConfirmObject(SalesOrder salesOrder, ISalesOrderDetailService _sods,
                                    IStockMutationService _stockMutationService, IItemService _itemService);
        SalesOrder UnconfirmObject(SalesOrder salesOrder, ISalesOrderDetailService _sods,
                                    IStockMutationService _stockMutationService, IItemService _itemService);
    }
}