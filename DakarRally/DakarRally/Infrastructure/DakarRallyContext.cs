using DakarRally.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DakarRally.Infrastructure
{
    public class DakarRallyContext : DbContext
    {
        public DakarRallyContext(DbContextOptions<DakarRallyContext> options)
            : base(options)
        {
        }

        public DbSet<Race> Races { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }
    }
}
