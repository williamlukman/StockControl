using System.Data.Entity.ModelConfiguration;
using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class PurchaseOrderMapping : EntityTypeConfiguration<PurchaseOrder>
    {
        public PurchaseOrderMapping()
        {
            HasKey(x => x.Id);
            HasMany(x => x.PurchaseOrderDetails)
                .WithRequired(y => y.PurchaseOrder)
                .Map(x => x.MapKey("Id"));
            HasRequired(x => x.Customer)
                .WithMany(y => y.PurchaseOrders)
                .Map(m => m.MapKey("Id"));
            Ignore(x => x.Errors);
        }
    }
}
