using DakarRally.DTOs;
using DakarRally.Enums;
using DakarRally.Extensions;
using DakarRally.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DakarRally.Infrastructure.Repositories
{
    public class RaceRepository : IAmRaceRepository
    {
        private readonly DakarRallyContext _context;

        public RaceRepository(DakarRallyContext dakarRallyContext)
        {
            _context = dakarRallyContext;
        }

        public async Task CreateRace(int year)
        {
            var race = new Race { Id = Guid.NewGuid(), Year = year, Status = RaceStatus.Pending.ToString() };

            await _context.Races.AddAsync(race);

            await SaveChanges();
        }

        public async Task<Race> RaceBy(int year)
        {
            return await _context.Races.Include(x => x.Vehicles).FirstOrDefaultAsync(x => x.Year.Equals(year));
        }

        public async Task<Race> RaceBy(Guid raceId)
        {
            return await _context.Races.Include(x => x.Vehicles).FirstOrDefaultAsync(x => x.Id.Equals(raceId));
        }

        public async Task<List<Race>> AllRaces()
        {
            return await _context.Races.Include(x => x.Vehicles).ToListAsync();
        }

        public async Task AddVehicle(VehicleDto vehicleDto)
        {
            var vehicle = vehicleDto.ToEntity();
            vehicle.Id = Guid.NewGuid();
            await _context.Vehicles.AddAsync(vehicle);

            var race = await RaceBy(DateTime.Now.Year);
            race.Vehicles.Add(vehicle);
            _context.Races.Update(race);

            await SaveChanges();
        }

        public async Task<RaceStatusDto> RaceStatusBy(Guid raceId)
        {
            var race = await RaceBy(raceId);

            return race.ToRaceStatusDto();
        }

        public async Task RemoveVehicleBy(Guid vehicleId)
        {
            var race = await RaceBy(DateTime.Now.Year);
            var vehicle = await _context.Vehicles.FirstOrDefaultAsync(v => v.Id.Equals(vehicleId));
            race.Vehicles.Remove(vehicle);
            _context.Races.Update(race);

            await SaveChanges();
        }

        public async Task UpdateVehicleInfo(VehicleDto vehicleDto)
        {
            var vehicleEntity = vehicleDto.ToEntity();
            _context.Vehicles.Update(vehicleEntity);

            var race = await RaceBy(DateTime.Now.Year);

            var updatedVehicles = race.Vehicles.Where(x =>
            {
                if (x.Id.Equals(vehicleDto.Id))
                {
                    x.ManufacturingDate = vehicleDto.ManufacturingDate;
                    x.Model = vehicleDto.Model;
                    x.Status = vehicleDto.Status;
                    x.TeamName = vehicleDto.TeamName;
                    x.Type = vehicleDto.Type;
                }

                return true;
            }).ToList();

            race.Vehicles = updatedVehicles;
            _context.Races.Update(race);
            
            await SaveChanges();
        }

        private async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
