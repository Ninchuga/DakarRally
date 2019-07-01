using DakarRally.Enums;
using DakarRally.Models.Domain;
using DakarRally.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DakarRally.Services
{
    public interface IAmRaceService
    {
        Task StartRaceBy(Guid raceId);
        Task CreateRace(int year);
        Task<Race> RaceBy(int year);
        Task<Race> RaceBy(Guid raceId);
        Task AddVehicle(UpsertVehicle vehicle);
        Task RemoveVehicleBy(Guid vehicleId);
        Task UpdateVehicleInfo(UpsertVehicle vehicle);
        Task<RaceStatus> RaceStatusBy(Guid raceId);
        Task<List<Vehicle>> AllVehiclesLeaderBoard();
        Task<List<Vehicle>> LeaderBoardForVehicleType(VehicleType vehicleType);
        Task<VehicleStatistics> VehicleStatisticsBy(Guid vehicleId);
        Task<List<Vehicle>> FindVehicleBy(string teamName, string model, string status);
    }
}