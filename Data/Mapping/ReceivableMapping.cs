using System.Data.Entity.ModelConfiguration;
using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class ReceivableMapping : EntityTypeConfiguration<Receivable>
    {

        public ReceivableMapping()
        {
            HasKey(r => r.Id);
            HasRequired(r => r.Contact)
                .WithMany(c => c.Receivables)
                .HasForeignKey(r => r.ContactId);
            HasOptional(r => r.ReceiptVouchers);
            Ignore(r => r.Errors);
        }
    }
}
