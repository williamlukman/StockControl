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
            HasOptional(c => c.PurchaseOrderDetails);
            HasOptional(c => c.PurchaseReceivals);
            HasOptional(c => c.PurchaseReceivalDetails);
            HasOptional(c => c.SalesOrders);
            HasOptional(c => c.SalesOrderDetails);
            HasOptional(c => c.DeliveryOrders);
            HasOptional(c => c.DeliveryOrderDetails);

            HasOptional(c => c.Payables);
            HasOptional(c => c.PaymentVouchers);
            HasOptional(c => c.PaymentVoucherDetails);
            HasOptional(c => c.Receivables);
            HasOptional(c => c.ReceiptVouchers);
            HasOptional(c => c.ReceiptVoucherDetails);
            HasOptional(c => c.PurchaseInvoices);
            HasOptional(c => c.PurchaseInvoiceDetails);
            HasOptional(c => c.SalesInvoices);
            HasOptional(c => c.SalesInvoiceDetails);
            Ignore(c => c.Errors);
        }
    }
}