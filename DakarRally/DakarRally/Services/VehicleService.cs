using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DakarRally.Infrastructure.Repositories;
using DakarRally.Models.DTOs;
using DakarRally.Models.ValueObjects;

namespace DakarRally.Services
{
    public class VehicleService : IAmVehicleService
    {
        private readonly IAmVehicleRepository _vehicleRepository;

        public VehicleService(IAmVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<VehicleStatistics> VehicleStatisticsBy(Guid vehicleId)
        {
            return await _vehicleRepository.VehicleStatisticsBy(vehicleId);
        }
    }
}
