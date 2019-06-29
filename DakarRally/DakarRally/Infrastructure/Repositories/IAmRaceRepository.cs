using DakarRally.Models;
using DakarRally.Models.DTOs;
using DakarRally.Models.Entities;
using DakarRally.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Race = DakarRally.Models.Domain.Race;
using Vehicle = DakarRally.Models.Domain.Vehicle;

namespace DakarRally.Infrastructure.Repositories
{
    public interface IAmRaceRepository
    {
        Task Create(Race race);
        Task<List<Race>> AllRaces();
        Task AddVehicle(UpsertVehicle vehicle);
        Task<Race> RaceBy(int year);
        Task<Race> RaceBy(Guid raceId);
        Task<RaceStatusDto> RaceStatusBy(Guid raceId);
        Task RemoveVehicleBy(Guid vehicleId);
        Task UpdateVehicleInfo(UpsertVehicle vehicle);
    }
}