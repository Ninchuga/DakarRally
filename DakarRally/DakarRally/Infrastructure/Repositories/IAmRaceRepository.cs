using DakarRally.DTOs;
using DakarRally.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DakarRally.Infrastructure.Repositories
{
    public interface IAmRaceRepository
    {
        Task CreateRace(int year);
        Task<List<Race>> AllRaces();
        Task AddVehicle(VehicleDto vehicle);
        Task<Race> RaceBy(int year);
        Task<RaceStatusDto> RaceStatusBy(Guid raceId);
        Task RemoveVehicleBy(Guid vehicleId);
        Task UpdateVehicleInfo(VehicleDto vehicle);
    }
}