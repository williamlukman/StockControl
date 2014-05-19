using System;
using System.Data.Entity;
using Core.DomainModel;

namespace Core
{
    public class StockControl : DbContext
    {
        public DbSet<Contact> contacts { get; set; }
        public DbSet<Item> items{get; set;}
        public DbSet<PurchaseOrder> purchaseOrders{get; set;}
        public DbSet<PurchaseOrderDetail> purchaseOrderDetails{get; set;}
        public DbSet<StockMutation> stockMutations{get; set;}
    }

}
