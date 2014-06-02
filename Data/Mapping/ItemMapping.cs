using System.Data.Entity.ModelConfiguration;
using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class ItemMapping : EntityTypeConfiguration<Item>
    {

        public ItemMapping()
        {
            HasKey(i => i.Id);
            HasOptional(i => i.StockMutations);
            HasOptional(i => i.StockAdjustmentDetails);
            Ignore(i => i.Errors);
        }
    }
}
