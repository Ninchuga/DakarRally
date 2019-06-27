using DakarRally.DTOs;
using DakarRally.Models;
using static DakarRally.Controllers.RaceController;

namespace DakarRally.Extensions
{
    public static class VehicleExtensions
    {
        public static Vehicle ToEntity(this VehicleDto dto)
        {
            return new Vehicle
            {
                ManufacturingDate = dto.ManufacturingDate,
                Model = dto.Model,
                TeamName = dto.TeamName
            };
        }

        public static VehicleDto ToDto(this Vehicle vehicle)
        {   
            return new VehicleDto
            {
                ManufacturingDate = vehicle.ManufacturingDate,
                Model = vehicle.Model,
                TeamName = vehicle.TeamName
            };
        }
    }
}
