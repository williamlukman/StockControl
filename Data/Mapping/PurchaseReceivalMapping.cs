using System.Data.Entity.ModelConfiguration;
using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class PurchaseReceivalMapping : EntityTypeConfiguration<PurchaseReceival>
    {
        public PurchaseReceivalMapping()
        {
            HasKey(pr => pr.Id);
            HasRequired(pr => pr.Contact)
                .WithMany(c => c.PurchaseReceivals)
                .WillCascadeOnDelete(false);
            HasOptional(pr => pr.PurchaseReceivalDetails);
            Ignore(pr => pr.Errors);
        }
    }
}
