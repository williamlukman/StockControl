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
    public class StockMutationRepository : EfRepository<StockMutation>, IStockMutationRepository
    {
        private StockControlEntities stocks;
        public StockMutationRepository()
        {
            stocks = new StockControlEntities();
        }

        public IList<StockMutation> GetAll()
        {
            return FindAll(sm => !sm.IsDeleted).ToList();
        }

        public IList<StockMutation> GetObjectsByItemId(int itemId)
        {
            return FindAll(sm => sm.ItemId == itemId && !sm.IsDeleted).ToList();
        }

        public StockMutation GetObjectById(int Id)
        {
            return Find(sm => sm.Id == Id && !sm.IsDeleted);
        }

        public IList<StockMutation> GetObjectsBySourceDocumentDetail(int itemId, string SourceDocumentDetailType, int SourceDocumentDetailId)
        {
            return FindAll(sm => sm.ItemId == itemId && sm.SourceDocumentDetailType == SourceDocumentDetailType
                                && sm.SourceDocumentDetailId == SourceDocumentDetailId && !sm.IsDeleted).ToList();
        }

        public StockMutation CreateObject(StockMutation stockMutation)
        {
            stockMutation.IsDeleted = false;
            stockMutation.CreatedAt = DateTime.Now;
            stockMutation.Errors = new HashSet<string>();
            return Create(stockMutation);
        }

        public StockMutation UpdateObject(StockMutation stockMutation)
        {
            stockMutation.ModifiedAt = DateTime.Now;
            Update(stockMutation);
            return stockMutation;
        }

        public StockMutation SoftDeleteObject(StockMutation stockMutation)
        {
            stockMutation.IsDeleted = true;
            stockMutation.DeletedAt = DateTime.Now;
            Update(stockMutation);
            return stockMutation;
        }

        public bool DeleteObject(int Id)
        {
            StockMutation sm = Find(x => x.Id == Id);
            return (Delete(sm) == 1) ? true : false;
        }

    }
}