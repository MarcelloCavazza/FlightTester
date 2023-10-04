using Domain.FlightTest;
using System.Data.Entity;

namespace Data
{
    public class Entities : DbContext
    {
        public DbSet<Flight> Flights => Set<Flight>();

    }
}