using DakarRally.Enums;
using DakarRally.Infrastructure.Repositories;
using DakarRally.Models.Domain;
using DakarRally.Models.ValueObjects;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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

        public async Task<Race> RaceBy(int year)
        {
            return await _raceRepository.RaceBy(year);
        }

        public async Task<Race> RaceBy(Guid raceId)
        {
            return await _raceRepository.RaceBy(raceId);
        }

        public async Task AddVehicle(UpsertVehicle vehicle)
        {
            var race = (await RaceBy(DateTime.Now.Year)).AddVehicle(vehicle);

            await _raceRepository.Upsert(race);
        }

        public async Task<List<Vehicle>> AllVehiclesLeaderBoard()
        {
            return (await RaceBy(DateTime.Now.Year)).AllVehiclesLeaderBoard();
        }

        public async Task CreateRace(int year)
        {
            int.TryParse(_configuration["RallyTotalDistance"], out int rallyTotalDistance);
            var newRace = Race.Create(year, rallyTotalDistance);
            await _raceRepository.Upsert(newRace);
        }

        public async Task<List<Vehicle>> LeaderBoardForVehicleType(VehicleType vehicleType)
        {
            return (await RaceBy(DateTime.Now.Year)).LeaderBoardForVehicleType(vehicleType);
        }

        public async Task<RaceStatus> RaceStatusBy(Guid raceId)
        {
            return (await RaceBy(raceId)).RaceStatus();
        }

        public async Task RemoveVehicleBy(Guid vehicleId)
        {
            var race = (await RaceBy(DateTime.Now.Year)).RemoveVehicleBy(vehicleId);

            await _raceRepository.Upsert(race);
        }

        public async Task StartRaceBy(Guid raceId)
        {
            int.TryParse(_configuration["CheckRaceProgressionInSeconds"], out int checkRaceProgressionInSeconds);

            await Task.Run(async () =>
            {
                var race = await _raceRepository.RaceBy(raceId);
                while (race.Status != RaceStatusType.Finished)
                {
                    race = race.StartRace(checkRaceProgressionInSeconds);

                    await _raceRepository.Upsert(race);

                    await Task.Delay(checkRaceProgressionInSeconds * 1000);
                }
            }).ConfigureAwait(false);
        }

        public async Task UpdateVehicleInfo(UpsertVehicle vehicle)
        {
            var race = (await RaceBy(DateTime.Now.Year)).UpdateVehicleInfo(vehicle);

            await _raceRepository.Upsert(race);
        }

        public async Task<VehicleStatistics> VehicleStatisticsBy(Guid vehicleId)
        {
            return (await RaceBy(DateTime.Now.Year)).VehicleStatisticsBy(vehicleId);
        }

        public async Task<List<Vehicle>> FindVehicleBy(string teamName, string model, string status)
        {
            return (await RaceBy(DateTime.Now.Year)).FindVehicleBy(teamName, model, status);
        }
    }
}
