using System.Data.Entity.ModelConfiguration;
using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class PurchaseOrderDetailMapping : EntityTypeConfiguration<PurchaseOrderDetail>
    {
        public PurchaseOrderDetailMapping()
        {
            HasKey(x => x.Id);
            HasRequired(x => x.Item)
                .WithMany(y => y.PurchaseOrderDetails)
                .Map(x => x.MapKey("Id"));
            HasRequired(x => x.PurchaseOrder)
                .WithMany(y => y.PurchaseOrderDetails)
                .Map(m => m.MapKey("Id"));
            Ignore(x => x.Errors);
        }
    }
}
