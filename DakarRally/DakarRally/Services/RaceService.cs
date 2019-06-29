﻿using DakarRally.Enums;
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

        public RaceService(IAmRaceRepository raceRepository)
        {
            _raceRepository = raceRepository;
        }

        public async Task AddVehicle(UpsertVehicle vehicle)
        {
            await _raceRepository.AddVehicle(vehicle);
        }

        public async Task<List<Race>> AllRaces()
        {
            return await _raceRepository.AllRaces();
        }

        public async Task CreateRace(int year, int rallyTotalDistance)
        {
            var newRace = Race.Create(year, rallyTotalDistance);
            await _raceRepository.Create(newRace);
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
            await Task.Run(async () => (await _raceRepository.RaceBy(raceId)).StartRace());
        }

        public async Task UpdateVehicleInfo(UpsertVehicle vehicle)
        {
            await _raceRepository.UpdateVehicleInfo(vehicle);
        }
    }
}