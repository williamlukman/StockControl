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
            HasKey(po => po.Id);
            HasRequired(po => po.Contact)
                .WithMany(c => c.PurchaseOrders)
                .HasForeignKey(po => po.ContactId);
            HasOptional(po => po.PurchaseOrderDetails)
                .WithOptionalPrincipal()
                .Map(pod => pod.MapKey("Id"));
            Ignore(po => po.Errors);
        }
    }
}
