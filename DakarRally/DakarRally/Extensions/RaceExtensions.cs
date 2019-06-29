using DakarRally.Enums;
using DakarRally.Models;
using DakarRally.Models.DTOs;
using DakarRally.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DakarRally.Extensions
{
    public static class RaceExtensions
    {
        public static Models.Domain.Race ToDomain(this Race race)
        {
            Enum.TryParse(race.Status, out RaceStatus raceStatus);
            return Models.Domain.Race.Create(race.Id, race.Year, race.Vehicles.ToDomainList(), raceStatus, race.Distance);
        }

        public static Race ToEntity(this Models.Domain.Race race)
        {
            return new Race
            {
                Id = Guid.NewGuid(),
                Distance = race.Distance,
                Status = race.Status.ToString(),
                Vehicles = race.Vehicles.ToEntityList(),
                Year = race.Year
            };
        }

        public static List<Models.Domain.Race> ToDomainList(this List<Race> races)
        {
            return races.Select(race => race.ToDomain()).ToList();
        }

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

                NumberOfSportCars = race.Vehicles.Where(v => v.Type == VehicleType.SportsCar.ToString()).Count(),
                NumberOfTerrainCars = race.Vehicles.Where(v => v.Type == VehicleType.TerrainCar.ToString()).Count(),
                NumberOfTrucks = race.Vehicles.Where(v => v.Type == VehicleType.Truck.ToString()).Count(),
                NumberOfSportMotorcycles = race.Vehicles.Where(v => v.Type == VehicleType.SportMotorcycle.ToString()).Count(),
                NumberOfCrossMotorcycles = race.Vehicles.Where(v => v.Type == VehicleType.CrossMotorcycle.ToString()).Count()
            };
        }
    }
}
