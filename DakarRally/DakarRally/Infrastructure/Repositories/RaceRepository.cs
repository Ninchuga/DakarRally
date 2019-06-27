using DakarRally.DTOs;
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

        public void CreateRace(int year)
        {
            var race = new Race { Id = Guid.NewGuid(), Year = year, Status = RaceStatus.Pending.ToString() };

            _context.Races.Add(race);

            _context.SaveChangesAsync();
        }

        public Race RaceBy(int year)
        {
            return _context.Races.Include(x => x.Vehicles).FirstOrDefault(x => x.Year.Equals(year));
        }

        public List<Race> AllRaces()
        {
            return _context.Races.Include(x => x.Vehicles).ToList();
        }

        public void AddVehicle(VehicleDto vehicleDto)
        {
            var vehicle = vehicleDto.ToEntity();
            vehicle.Id = Guid.NewGuid();
            _context.Vehicles.Add(vehicle);

            var race = RaceBy(DateTime.Now.Year);
            race.Vehicles.Add(vehicle);
            _context.Races.Update(race);

            _context.SaveChangesAsync();
        }
    }
}
