using DakarRally.Models.DTOs;
using DakarRally.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DakarRally.Services
{
    public interface IAmVehicleService
    {
        Task<VehicleStatistics> VehicleStatisticsBy(Guid vehicleId);
    }
}
