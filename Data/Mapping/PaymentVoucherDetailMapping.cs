using System.Data.Entity.ModelConfiguration;
using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class PaymentVoucherDetailMapping : EntityTypeConfiguration<PaymentVoucherDetail>
    {

        public PaymentVoucherDetailMapping()
        {
            HasKey(pvd => pvd.Id);
            HasRequired(pvd => pvd.Contact)
                .WithMany(c => c.PaymentVoucherDetails)
                .WillCascadeOnDelete(false);
            HasRequired(pvd => pvd.PaymentVoucher)
                .WithMany(pv => pv.PaymentVoucherDetails)
                .HasForeignKey(pvd => pvd.PaymentVoucherId);
            Ignore(i => i.Errors);
        }
    }
}
