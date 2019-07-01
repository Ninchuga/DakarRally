using DakarRally.Enums;
using DakarRally.Models.DTOs;
using DakarRally.Models.Entities;
using DakarRally.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DakarRally.Extensions
{
    public static class RaceExtensions
    {
        public static Models.Domain.Race ToDomain(this Race race)
        {
            Enum.TryParse(race.Status, out RaceStatusType raceStatus);
            return Models.Domain.Race.Create(race.Id, race.Year, race.Vehicles.ToDomainList(), raceStatus, race.Distance);
        }

        public static Race ToEntity(this Models.Domain.Race race)
        {
            return new Race
            {
                Id = race.Id.Equals(Guid.Empty) ? Guid.NewGuid() : race.Id,
                Distance = race.Distance,
                Status = race.Status.ToString(),
                Vehicles = race.Vehicles.ToEntityList(),
                Year = race.Year
            };
        }

        public static RaceDto ToDto(this Models.Domain.Race race)
        {
            return new RaceDto
            {
                Id = race.Id,
                Distance = race.Distance,
                Status = race.Status.ToString(),
                Year = race.Year,
                Vehicles = race.Vehicles.ToVehicleDtoList()
            };
        }

        public static List<Models.Domain.Race> ToDomainList(this List<Race> races)
        {
            return races.Select(race => race.ToDomain()).ToList();
        }

        public static RaceStatusDto ToRaceStatusDto(this RaceStatus race)
        {
            return new RaceStatusDto
            {
                Status = race.Status.ToString(),
                NumberOfVehiclesWithPendingStatus = race.NumberOfVehiclesWithPendingStatus,
                NumberOfVehiclesWithHeavyMalfunctionStatus = race.NumberOfVehiclesWithHeavyMalfunctionStatus,
                NumberOfVehiclesWithLightMalfunctionStatus = race.NumberOfVehiclesWithLightMalfunctionStatus,
                NumberOfVehiclesWithRunningStatus = race.NumberOfVehiclesWithRunningStatus,
                NumberOfVehiclesWithFinishedStatus = race.NumberOfVehiclesWithFinishedStatus,
                NumberOfSportCars = race.NumberOfSportCars,
                NumberOfTerrainCars = race.NumberOfTerrainCars,
                NumberOfTrucks = race.NumberOfTrucks,
                NumberOfSportMotorcycles = race.NumberOfSportMotorcycles,
                NumberOfCrossMotorcycles = race.NumberOfCrossMotorcycles
            };
        }
    }
}
