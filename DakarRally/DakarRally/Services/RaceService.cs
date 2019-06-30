using DakarRally.Enums;
using DakarRally.Infrastructure.Repositories;
using DakarRally.Models.Domain;
using DakarRally.Models.DTOs;
using DakarRally.Models.ValueObjects;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DakarRally.Services
{
    public class RaceService : IAmRaceService
    {
        private readonly IAmRaceRepository _raceRepository;
        private readonly IConfiguration _configuration;

        public RaceService(IAmRaceRepository raceRepository, IConfiguration configuration)
        {
            _raceRepository = raceRepository;
            _configuration = configuration;
        }

        public async Task AddVehicle(UpsertVehicle vehicle)
        {
            await _raceRepository.AddVehicle(vehicle);
        }

        public async Task<List<Race>> AllRaces()
        {
            return await _raceRepository.AllRaces();
        }

        public async Task<List<Vehicle>> AllVehiclesLeaderBoard()
        {
            return await _raceRepository.AllVehiclesLeaderBoard();
        }

        public async Task CreateRace(int year)
        {
            int.TryParse(_configuration["RallyTotalDistance"], out int rallyTotalDistance);
            var newRace = Race.Create(year, rallyTotalDistance);
            await _raceRepository.Create(newRace);
        }

        public async Task<List<Vehicle>> LeaderBoardForVehicleType(VehicleType vehicleType)
        {
            return await _raceRepository.LeaderBoardForVehicleType(vehicleType);
        }

        public async Task<Race> RaceBy(int year)
        {
            return await _raceRepository.RaceBy(year);
        }

        // change this to value object
        public async Task<RaceStatusDto> RaceStatusBy(Guid raceId)
        {
            return await _raceRepository.RaceStatusBy(raceId);
        }

        public async Task RemoveVehicleBy(Guid vehicleId)
        {
            await _raceRepository.RemoveVehicleBy(vehicleId);
        }

        public async Task StartRaceBy(Guid raceId)
        {
            int.TryParse(_configuration["CheckRaceProgressionInSeconds"], out int checkRaceProgressionInSeconds);
            await Task.Run(async () => (await _raceRepository.RaceBy(raceId)).StartRace(checkRaceProgressionInSeconds));
        }

        public async Task UpdateVehicleInfo(UpsertVehicle vehicle)
        {
            await _raceRepository.UpdateVehicleInfo(vehicle);
        }
    }
}
