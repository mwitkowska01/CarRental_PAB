using CarRental.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace CarRental.Infrastructure
{
    public class RentalDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Contractor> Contractors { get; set; }
        public DbSet<Personel> Personels { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

        public RentalDbContext(DbContextOptions<RentalDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}