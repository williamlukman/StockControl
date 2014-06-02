using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Collections.Generic;
using Core.DomainModel;
using System.Data.Entity.ModelConfiguration.Conventions;
using Data.Mapping;

namespace Data.Context
{
    public class StockControlEntities : DbContext
    {
        public StockControlEntities()
        {
            Database.SetInitializer<StockControlEntities>(new DropCreateDatabaseIfModelChanges<StockControlEntities>());
        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Goods Process Mappings
            modelBuilder.Configurations.Add(new ContactMapping());
            modelBuilder.Configurations.Add(new ItemMapping());
            modelBuilder.Configurations.Add(new PurchaseOrderMapping());
            modelBuilder.Configurations.Add(new PurchaseOrderDetailMapping());
            modelBuilder.Configurations.Add(new PurchaseReceivalMapping());
            modelBuilder.Configurations.Add(new PurchaseReceivalDetailMapping());
            modelBuilder.Configurations.Add(new SalesOrderMapping());
            modelBuilder.Configurations.Add(new SalesOrderDetailMapping());
            modelBuilder.Configurations.Add(new DeliveryOrderMapping());
            modelBuilder.Configurations.Add(new DeliveryOrderDetailMapping());
            modelBuilder.Configurations.Add(new StockMutationMapping());
            modelBuilder.Configurations.Add(new StockAdjustmentMapping());
            modelBuilder.Configurations.Add(new StockAdjustmentDetailMapping());

            // Accounting Finance Mappings
            
            modelBuilder.Configurations.Add(new CashBankMapping());
            modelBuilder.Configurations.Add(new PurchaseInvoiceMapping());
            modelBuilder.Configurations.Add(new PurchaseInvoiceDetailMapping());
            modelBuilder.Configurations.Add(new PayableMapping());
            modelBuilder.Configurations.Add(new PaymentVoucherMapping());
            modelBuilder.Configurations.Add(new PaymentVoucherDetailMapping());
            modelBuilder.Configurations.Add(new SalesInvoiceMapping());
            modelBuilder.Configurations.Add(new SalesInvoiceDetailMapping());
            modelBuilder.Configurations.Add(new ReceivableMapping());
            modelBuilder.Configurations.Add(new ReceiptVoucherMapping());
            modelBuilder.Configurations.Add(new ReceiptVoucherDetailMapping());
            
            base.OnModelCreating(modelBuilder);
        }

        /* Goods Process */

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Item> Items{get; set;}
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        public DbSet<PurchaseReceival> PurchaseReceivals { get; set; }
        public DbSet<PurchaseReceivalDetail> PurchaseReceivalDetails { get; set; }
        public DbSet<SalesOrder> SalesOrders { get; set; }
        public DbSet<SalesOrderDetail> SalesOrderDetails { get; set; }
        public DbSet<DeliveryOrder> DeliveryOrders { get; set; }
        public DbSet<DeliveryOrderDetail> DeliveryOrderDetails { get; set; }
        public DbSet<StockMutation> StockMutations { get; set; }
        public DbSet<StockAdjustment> StockAdjustments { get; set; }
        public DbSet<StockAdjustmentDetail> StockAdjustmentDetails { get; set; }

        /* Accounting Finance */

        public DbSet<CashBank> CashBanks { get; set; }
        public DbSet<PurchaseInvoice> PurchaseInvoices { get; set; }
        public DbSet<PurchaseInvoiceDetail> PurchaseInvoiceDetails { get; set; }
        public DbSet<Payable> Payables { get; set; }
        public DbSet<PaymentVoucher> PaymentVouchers { get; set; }
        public DbSet<PaymentVoucherDetail> PaymentVoucherDetails { get; set; }
        public DbSet<SalesInvoice> SalesInvoices { get; set; }
        public DbSet<SalesInvoiceDetail> SalesInvoiceDetails { get; set; }
        public DbSet<Receivable> Receivables { get; set; }
        public DbSet<ReceiptVoucher> ReceiptVouchers { get; set; }
        public DbSet<ReceiptVoucherDetail> ReceiptVoucherDetails { get; set; }
    }
}