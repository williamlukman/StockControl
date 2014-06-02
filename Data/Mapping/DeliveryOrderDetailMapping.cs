using System.Data.Entity.ModelConfiguration;
using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class DeliveryOrderDetailMapping : EntityTypeConfiguration<DeliveryOrderDetail>
    {
        public DeliveryOrderDetailMapping()
        {
            HasKey(prd => prd.Id);
            
            HasRequired(prd => prd.DeliveryOrder)
                .WithMany(pr => pr.DeliveryOrderDetails)
                .HasForeignKey(prd => prd.DeliveryOrderId);
            HasOptional(prd => prd.SalesOrderDetail)
                .WithOptionalDependent(pod => pod.DeliveryOrderDetail);
            Ignore(pod => pod.Errors);
        }
    }
}
