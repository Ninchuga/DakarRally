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
                Id = dto.Id,
                ManufacturingDate = dto.ManufacturingDate.Date,
                Model = dto.Model,
                TeamName = dto.TeamName,
                Distance = dto.Distance,
                Status = dto.Status,
                FinishTime = dto.FinishTime,
                Type = dto.Type
            };
        }

        public static VehicleDto ToDto(this Vehicle vehicle)
        {   
            return new VehicleDto
            {
                ManufacturingDate = vehicle.ManufacturingDate.Date,
                Model = vehicle.Model,
                TeamName = vehicle.TeamName,
                Type = vehicle.Type,
                FinishTime = vehicle.FinishTime,
                Status = vehicle.Status,
                Distance = vehicle.Distance
            };
        }
    }
}
