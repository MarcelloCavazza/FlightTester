using Domain.FlightTest;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class Entities : DbContext
    {
        public DbSet<Flight> Flights => Set<Flight>();

        public Entities(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Flight>().HasKey(f => f.Id);

            modelBuilder.Entity<Flight>().HasMany(f => f.SeatsList).WithOne().IsRequired();
            modelBuilder.Entity<Booking>().HasKey(f => f.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}