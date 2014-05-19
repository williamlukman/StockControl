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

            // Mappings
            modelBuilder.Configurations.Add(new ContactMapping());
            modelBuilder.Configurations.Add(new ItemMapping());
            modelBuilder.Configurations.Add(new PurchaseOrderMapping());
            modelBuilder.Configurations.Add(new PurchaseOrderDetailMapping());
            modelBuilder.Configurations.Add(new StockMutationMapping());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Item> Items{get; set;}
        public DbSet<PurchaseOrder> PurchaseOrders{get; set;}
        public DbSet<PurchaseOrderDetail> PurchaseOrderDetails{get; set;}
        public DbSet<StockMutation> StockMutations{get; set;}
    }
}