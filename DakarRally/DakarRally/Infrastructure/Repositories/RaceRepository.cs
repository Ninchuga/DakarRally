using DakarRally.Enums;
using DakarRally.Extensions;
using DakarRally.Models;
using DakarRally.Models.Domain;
using DakarRally.Models.DTOs;
using DakarRally.Models.Entities;
using DakarRally.Models.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Race = DakarRally.Models.Domain.Race;
using Vehicle = DakarRally.Models.Domain.Vehicle;

namespace DakarRally.Infrastructure.Repositories
{
    public class RaceRepository : IAmRaceRepository
    {
        private readonly DakarRallyContext _context;

        public RaceRepository(DakarRallyContext dakarRallyContext)
        {
            _context = dakarRallyContext;
        }

        public async Task Create(Race race)
        {
            var raceEntity = race.ToEntity();
            await _context.Races.AddAsync(raceEntity);

            await SaveChanges();
        }

        public async Task<Race> RaceBy(int year)
        {
            var race = await _context.Races.Include(x => x.Vehicles).FirstOrDefaultAsync(x => x.Year.Equals(year));
            return race.ToDomain();
        }

        public async Task<Race> RaceBy(Guid raceId)
        {
            var race = await _context.Races.Include(x => x.Vehicles).FirstOrDefaultAsync(x => x.Id.Equals(raceId));
            return race.ToDomain();
        }

        public async Task<List<Race>> AllRaces()
        {
            var raceList = await _context.Races.Include(x => x.Vehicles).ToListAsync();
            return raceList.ToDomainList();
        }

        public async Task AddVehicle(UpsertVehicle vehicle)
        {
            var vehicleEntity = vehicle.ToEntity();
            await _context.Vehicles.AddAsync(vehicleEntity);

            var race = await _context.Races.Include(x => x.Vehicles).FirstOrDefaultAsync(x => x.Year.Equals(DateTime.Now.Year));
            race.Vehicles.Add(vehicleEntity);
            _context.Races.Update(race);

            await SaveChanges();
        }

        public async Task<RaceStatusDto> RaceStatusBy(Guid raceId)
        {
            var race = await _context.Races.Include(x => x.Vehicles).FirstOrDefaultAsync(x => x.Id.Equals(raceId));

            return race.ToRaceStatusDto();
        }

        public async Task RemoveVehicleBy(Guid vehicleId)
        {
            var race = await _context.Races.Include(x => x.Vehicles).FirstOrDefaultAsync(x => x.Year.Equals(DateTime.Now.Year));
            var vehicleToRemove = race.Vehicles.FirstOrDefault(vehicle => vehicle.Id.Equals(vehicleId));
            race.Vehicles.Remove(vehicleToRemove);
            _context.Races.Update(race);

            await SaveChanges();
        }

        public async Task UpdateVehicleInfo(UpsertVehicle vehicle)
        {
            var vehicleEntity = vehicle.ToEntity();
            _context.Vehicles.Update(vehicleEntity);

            var race = await RaceBy(DateTime.Now.Year);
            race.Vehicles.FirstOrDefault(v => v.Id.Equals(vehicle.Id)).UpdateInfo(vehicle);

            //var updatedVehicles = race.Vehicles.Where(x =>
            //{
            //    if (x.Id.Equals(vehicleDto.Id))
            //    {
            //        x.ManufacturingDate = vehicleDto.ManufacturingDate;
            //        x.Model = vehicleDto.Model;
            //        x.Status = vehicleDto.Status;
            //        x.TeamName = vehicleDto.TeamName;
            //        x.Type = vehicleDto.Type;
            //    }

            //    return true;
            //}).ToList();

            //race.Vehicles = updatedVehicles;
            _context.Races.Update(race.ToEntity());
            
            await SaveChanges();
        }

        public async Task<List<Vehicle>> AllVehiclesLeaderBoard()
        {
            var race = await _context.Races.Include(x => x.Vehicles).FirstOrDefaultAsync(x => x.Year.Equals(DateTime.Now.Year));
            return race.Vehicles.ToDomainList();
        }

        public async Task<List<Vehicle>> LeaderBoardForVehicleType(VehicleType vehicleType)
        {
            var race = await _context.Races.Include(x => x.Vehicles).FirstOrDefaultAsync(x => x.Year.Equals(DateTime.Now.Year));
            return race.Vehicles.Where(vehicle => vehicle.Type.Equals(vehicleType.ToString())).ToList().ToDomainList();
        }

        public async Task Update(Race race)
        {
            _context.Races.Update(race.ToEntity());

            foreach (var vehicle in race.Vehicles)
            {
                _context.Vehicles.Update(vehicle.ToEntity());
            }

            await SaveChanges();
        }

        private async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
