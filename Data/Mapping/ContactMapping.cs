using System.Data.Entity.ModelConfiguration;
using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class ContactMapping : EntityTypeConfiguration<Contact>
    {
        public ContactMapping()
        {
            HasKey(c => c.Id);
            HasOptional(c => c.PurchaseOrders);
            HasOptional(c => c.PurchaseReceivals);
            HasOptional(c => c.SalesOrders);
            HasOptional(c => c.DeliveryOrders);

            HasOptional(c => c.Payables);
            HasOptional(c => c.Receivables);
            HasOptional(c => c.PurchaseInvoices);
            HasOptional(c => c.SalesInvoices);
            Ignore(c => c.Errors);
        }
    }
}