using System.Data.Entity.ModelConfiguration;
using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class SalesInvoiceMapping : EntityTypeConfiguration<SalesInvoice>
    {

        public SalesInvoiceMapping()
        {
            HasKey(si => si.Id);
            HasRequired(si => si.Contact)
                .WithMany(c => c.SalesInvoices)
                .WillCascadeOnDelete(false);
            HasOptional(si => si.SalesInvoiceDetails)
                .WithOptionalPrincipal()
                .Map(sid => sid.MapKey("Id"));
            Ignore(i => i.Errors);
        }
    }
}
