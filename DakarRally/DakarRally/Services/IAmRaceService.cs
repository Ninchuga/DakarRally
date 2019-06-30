﻿using DakarRally.Enums;
using DakarRally.Models.Domain;
using DakarRally.Models.DTOs;
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
        Task<List<Race>> AllRaces();
        Task AddVehicle(UpsertVehicle vehicle);
        Task RemoveVehicleBy(Guid vehicleId);
        Task UpdateVehicleInfo(UpsertVehicle vehicle);
        Task<RaceStatusDto> RaceStatusBy(Guid raceId);
        Task<List<Vehicle>> AllVehiclesLeaderBoard();
        Task<List<Vehicle>> LeaderBoardForVehicleType(VehicleType vehicleType);
    }
}