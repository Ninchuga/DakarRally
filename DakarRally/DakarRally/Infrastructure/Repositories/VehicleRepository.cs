using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DakarRally.Extensions;
using DakarRally.Models.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace DakarRally.Infrastructure.Repositories
{
    public class VehicleRepository : IAmVehicleRepository
    {
        private readonly DakarRallyContext _context;

        public VehicleRepository(DakarRallyContext context)
        {
            _context = context;
        }

        public async Task<VehicleStatistics> VehicleStatisticsBy(Guid vehicleId)
        {
            var vehicleEntity = await _context.Vehicles.FirstOrDefaultAsync(vehicle => vehicle.Id.Equals(vehicleId));
            return vehicleEntity.ToVehicleStatistics();
        }
    }
}
