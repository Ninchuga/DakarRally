using DakarRally.DTOs;
using DakarRally.Enums;
using DakarRally.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DakarRally.Extensions
{
    public static class RaceExtensions
    {
        public static RaceStatusDto ToRaceStatusDto(this Race race)
        {
            return new RaceStatusDto
            {
                Status = race.Status,

                NumberOfVehiclesWithPendingStatus = race.Vehicles.Where(v => v.Status == VehicleStatus.Pending.ToString()).Count(),
                NumberOfVehiclesWithHeavyMalfunctionStatus = race.Vehicles.Where(v => v.Status == VehicleStatus.HeavyMalfunction.ToString()).Count(),
                NumberOfVehiclesWithLightMalfunctionStatus = race.Vehicles.Where(v => v.Status == VehicleStatus.LightMalfunction.ToString()).Count(),
                NumberOfVehiclesWithRunningStatus = race.Vehicles.Where(v => v.Status == VehicleStatus.Running.ToString()).Count(),
                NumberOfVehiclesWithFinishedStatus = race.Vehicles.Where(v => v.Status == VehicleStatus.Finished.ToString()).Count(),

                NumberOfSportCars = race.Vehicles.Where(v => v.Type == VehicleTypes.SportsCar.ToString()).Count(),
                NumberOfTerrainCars = race.Vehicles.Where(v => v.Type == VehicleTypes.TerrainCar.ToString()).Count(),
                NumberOfTrucks = race.Vehicles.Where(v => v.Type == VehicleTypes.Truck.ToString()).Count(),
                NumberOfSportMotorcycles = race.Vehicles.Where(v => v.Type == VehicleTypes.SportMotorcycle.ToString()).Count(),
                NumberOfCrossMotorcycles = race.Vehicles.Where(v => v.Type == VehicleTypes.CrossMotorcycle.ToString()).Count()
            };
        }
    }
}
