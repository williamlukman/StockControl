using Core.DomainModel;
using Core.Interface.Repository;
using Core.Interface.Service;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class SalesOrderDetailService : ISalesOrderDetailService
    {
        private ISalesOrderDetailRepository _sd;
        public SalesOrderDetailService(ISalesOrderDetailRepository _salesOrderDetailRepository)
        {
            _sd = _salesOrderDetailRepository;
        }

        public IList<SalesOrderDetail> GetObjectsBySalesOrderId(int salesOrderId)
        {
            return _sd.GetObjectsBySalesOrderId(salesOrderId);
        }

        public SalesOrderDetail GetObjectById(int Id)
        {
            return _sd.GetObjectById(Id);
        }

        public SalesOrderDetail CreateObject(SalesOrderDetail salesOrderDetail)
        {
            return _sd.CreateObject(salesOrderDetail);
        }

        public SalesOrderDetail UpdateObject(SalesOrderDetail salesOrderDetail)
        {
            return _sd.UpdateObject(salesOrderDetail);
        }

        public SalesOrderDetail SoftDeleteObject(SalesOrderDetail salesOrderDetail)
        {
            return _sd.SoftDeleteObject(salesOrderDetail);
        }

        public bool DeleteObject(int Id)
        {
            return _sd.DeleteObject(Id);
        }

        public SalesOrderDetail ConfirmObject(SalesOrderDetail salesOrderDetail)
        {
            return _sd.ConfirmObject(salesOrderDetail);
        }

        public SalesOrderDetail UnconfirmObject(SalesOrderDetail salesOrderDetail)
        {
            return _sd.UnconfirmObject(salesOrderDetail);
        }

        public SalesOrderDetail FulfilObject(SalesOrderDetail salesOrderDetail)
        {
            return _sd.FulfilObject(salesOrderDetail);
        }

    }
}