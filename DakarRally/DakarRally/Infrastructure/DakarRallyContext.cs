using DakarRally.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
