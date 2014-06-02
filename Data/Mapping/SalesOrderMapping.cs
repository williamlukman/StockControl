using System.Data.Entity.ModelConfiguration;
using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class SalesOrderMapping : EntityTypeConfiguration<SalesOrder>
    {
        public SalesOrderMapping()
        {
            HasKey(so => so.Id);
            HasRequired(so => so.Contact)
                .WithMany(c => c.SalesOrders)
                .HasForeignKey(so => so.ContactId);
            HasOptional(so => so.SalesOrderDetails);
            Ignore(so => so.Errors);
        }
    }
}
