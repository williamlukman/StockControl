using System.Data.Entity.ModelConfiguration;
using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class StockMutationMapping : EntityTypeConfiguration<StockMutation>
    {

        public StockMutationMapping()
        {
            HasKey(x => x.Id);
            HasRequired(x => x.Item)
                .WithMany (y => y.StockMutations)
                .Map(x => x.MapKey("Id"));
            Ignore(x => x.Errors);
        }
    }
}
