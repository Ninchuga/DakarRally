using DakarRally.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DakarRally.Infrastructure.Repositories
{
    public interface IAmVehicleRepository
    {
        Task<VehicleStatistics> VehicleStatisticsBy(Guid vehicleId);
    }
}
