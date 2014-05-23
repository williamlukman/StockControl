using Core.DomainModel;
using Core.Interface.Repository;
using Core.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class SalesOrderService : ISalesOrderService
    {
        private ISalesOrderRepository _s;
        public SalesOrderService(ISalesOrderRepository _salesOrderRepository)
        {
            _s = _salesOrderRepository;
        }

        public IList<SalesOrder> GetAll()
        {
            return _s.GetAll();
        }

        public SalesOrder GetObjectById(int Id)
        {
            return _s.GetObjectById(Id);
        }

        public IList<SalesOrder> GetObjectsByContactId(int contactId)
        {
            return _s.GetObjectsByContactId(contactId);
        }
        
        public SalesOrder CreateObject(SalesOrder salesOrder)
        {
            return _s.CreateObject(salesOrder);
        }

        public SalesOrder CreateObject(int contactId, DateTime salesDate)
        {
            SalesOrder so = new SalesOrder
            {
                CustomerId = contactId,
                SalesDate = salesDate
            };
            return _s.CreateObject(so);
        }

        public SalesOrder UpdateObject(SalesOrder salesOrder)
        {
            return _s.UpdateObject(salesOrder);
        }

        public SalesOrder SoftDeleteObject(SalesOrder salesOrder)
        {
            return _s.SoftDeleteObject(salesOrder);
        }

        public bool DeleteObject(int Id)
        {
            return _s.DeleteObject(Id);
        }

        public SalesOrder ConfirmObject(SalesOrder salesOrder)
        {
            return _s.ConfirmObject(salesOrder);
        }

        public SalesOrder UnconfirmObject(SalesOrder salesOrder)
        {
            return _s.UnconfirmObject(salesOrder);
        }
    }
}