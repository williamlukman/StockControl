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
            List<StockMutation> sms = (from s in stocks.stockMutations
                                       where !s.IsDeleted
                                       select s).ToList();
            return sms;
        }
        IList<StockMutation> GetObjectsByItemId(int itemId);
        StockMutation GetObjectById(int Id);
        StockMutation CreateObject(StockMutation stockMutation);
        StockMutation UpdateObject(StockMutation stockMutation);
        StockMutation SoftDeleteObject(StockMutation stockMutation);
        bool DeleteObject(int Id);

    }
}