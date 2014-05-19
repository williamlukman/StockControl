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
            HasKey(x => x.Id);
            HasMany(x => x.StockMutations)
                .WithRequired(y => y.Item)
                .Map(x => x.MapKey("Id"));
            HasMany(x => x.PurchaseOrderDetails)
                .WithRequired(y => y.Item)
                .Map(x => x.MapKey("Id"));
            Ignore(x => x.Errors);
        }
    }
}
