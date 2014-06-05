using System.Data.Entity.ModelConfiguration;
using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class SalesInvoiceDetailMapping : EntityTypeConfiguration<SalesInvoiceDetail>
    {

        public SalesInvoiceDetailMapping()
        {
            HasKey(sid => sid.Id);
            HasRequired(sid => sid.Contact)
                .WithMany(c => c.SalesInvoiceDetails)
                .WillCascadeOnDelete(false);
            HasRequired(sid => sid.SalesInvoice)
                .WithMany(si => si.SalesInvoiceDetails)
                .HasForeignKey(sid => sid.SalesInvoiceId);
            Ignore(sid => sid.Errors);
        }
    }
}
