using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using App.DataAccessLayer.Entities;

namespace App.DataAccessLayer.AppContext
{
    class ApplicationContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<AuthorInPrintingEdition> AuthorInPrintingEditions { get; set; }
        public DbSet<Order>Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PrintingEdition> PrintingEditions { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("(LocalDb)\\MSSQLLocalDB;Database=AppDB;Trusted_Connection=True;");
        }
    }
}
