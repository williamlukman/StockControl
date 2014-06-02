using System.Data.Entity.ModelConfiguration;
using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class PurchaseInvoiceMapping : EntityTypeConfiguration<PurchaseInvoice>
    {

        public PurchaseInvoiceMapping()
        {
            HasKey(pi => pi.Id);
            HasRequired(pi => pi.Contact)
                .WithMany(c => c.PurchaseInvoices)
                .HasForeignKey(pi => pi.ContactId);
            HasOptional(pi => pi.PurchaseInvoiceDetails)
                .WithOptionalPrincipal()
                .Map(pid => pid.MapKey("Id"));
            Ignore(pi => pi.Errors);
        }
    }
}
