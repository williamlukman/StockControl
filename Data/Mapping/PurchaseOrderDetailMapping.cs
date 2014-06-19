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
            HasKey(pod => pod.Id);
            HasRequired(pod => pod.Contact)
                .WithMany(c => c.PurchaseOrderDetails)
                .WillCascadeOnDelete(false);
            HasRequired(pod => pod.PurchaseOrder)
                .WithMany(po => po.PurchaseOrderDetails)
                .HasForeignKey(pod => pod.PurchaseOrderId);
            HasOptional(pod => pod.PurchaseReceivalDetail)
                .WithOptionalPrincipal(prd => prd.PurchaseOrderDetail)
                .Map(prd => prd.MapKey("PurchaseReceivalDetailId"));
            Ignore(pod => pod.Errors);
        }
    }
}
