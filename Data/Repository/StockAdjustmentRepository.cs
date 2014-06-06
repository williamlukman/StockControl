using Core.DomainModel;
using Core.Interface.Repository;
using Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class StockAdjustmentRepository : EfRepository<StockAdjustment>, IStockAdjustmentRepository
    {
        private StockControlEntities stocks;
        public StockAdjustmentRepository()
        {
            stocks = new StockControlEntities();
        }

        public IList<StockAdjustment> GetAll()
        {
            return FindAll(sa => !sa.IsDeleted).ToList();
        }

        public StockAdjustment GetObjectById(int Id)
        {
            StockAdjustment stockAdjustment = Find(sa => sa.Id == Id && !sa.IsDeleted);
            if (stockAdjustment != null) { stockAdjustment.Errors = new Dictionary<string, string>(); }
            return stockAdjustment;
        }

        public StockAdjustment CreateObject(StockAdjustment stockAdjustment)
        {
            stockAdjustment.IsDeleted = false;
            stockAdjustment.IsConfirmed = false;
            stockAdjustment.CreatedAt = DateTime.Now;
            return Create(stockAdjustment);
        }

        public StockAdjustment UpdateObject(StockAdjustment stockAdjustment)
        {
            stockAdjustment.ModifiedAt = DateTime.Now;
            Update(stockAdjustment);
            return stockAdjustment;
        }

        public StockAdjustment SoftDeleteObject(StockAdjustment stockAdjustment)
        {
            stockAdjustment.IsDeleted = true;
            stockAdjustment.DeletedAt = DateTime.Now;
            Update(stockAdjustment);
            return stockAdjustment;
        }

        public bool DeleteObject(int Id)
        {
            StockAdjustment sa = Find(x => x.Id == Id);
            return (Delete(sa) == 1) ? true : false;
        }

        public StockAdjustment ConfirmObject(StockAdjustment stockAdjustment)
        {
            stockAdjustment.IsConfirmed = true;
            Update(stockAdjustment);
            return stockAdjustment;
        }

        public StockAdjustment UnconfirmObject(StockAdjustment stockAdjustment)
        {
            stockAdjustment.IsConfirmed = false;
            Update(stockAdjustment);
            return stockAdjustment;
        }
    }
}