using System.Data.Entity.ModelConfiguration;
using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class PurchaseInvoiceDetailMapping : EntityTypeConfiguration<PurchaseInvoiceDetail>
    {

        public PurchaseInvoiceDetailMapping()
        {
            HasKey(pi => pi.Id);
            HasRequired(pid => pid.Contact)
                .WithMany(c => c.PurchaseInvoiceDetails)
                .WillCascadeOnDelete(false);
            HasRequired(pid => pid.PurchaseInvoice)
                .WithMany(pi => pi.PurchaseInvoiceDetails)
                .HasForeignKey(pid => pid.PurchaseInvoiceId);
            Ignore(i => i.Errors);
        }
    }
}
