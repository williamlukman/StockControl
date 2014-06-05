﻿using System.Data.Entity.ModelConfiguration;
using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class PaymentVoucherMapping : EntityTypeConfiguration<PaymentVoucher>
    {

        public PaymentVoucherMapping()
        {
            HasKey(pv => pv.Id);
            HasRequired(pv => pv.Contact)
                .WithMany(c => c.PaymentVouchers)
                .WillCascadeOnDelete(false);
            HasOptional(pr => pr.PaymentVoucherDetails);
            Ignore(i => i.Errors);
        }
    }
}
