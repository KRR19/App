using App.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.DataAccessLayer.AppContext
{
    public class ApplicationContext : IdentityDbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<AuthorInPrintingEdition> AuthorInPrintingEditions { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PrintingEdition> PrintingEditions { get; set; }
        public DbSet<User> Users { get; set; }


        public ApplicationContext(DbContextOptions optionsBuilder) : base(optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AuthorInPrintingEdition>().HasKey(t => new { t.AuthorId, t.PrintingEditionId });

            modelBuilder.Entity<AuthorInPrintingEdition>().HasOne(sc => sc.Author).WithMany(s => s.AuthorInPrintingEditions).HasForeignKey(sc => sc.AuthorId);

            modelBuilder.Entity<AuthorInPrintingEdition>().HasOne(sc => sc.PrintingEdition).WithMany(c => c.AuthorInPrintingEditions).HasForeignKey(sc => sc.PrintingEditionId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseLazyLoadingProxies();
        }

    }
}

