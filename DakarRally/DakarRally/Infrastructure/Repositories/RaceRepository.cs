using DakarRally.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Race = DakarRally.Models.Domain.Race;

namespace DakarRally.Infrastructure.Repositories
{
    public class RaceRepository : IAmRaceRepository
    {
        private readonly DakarRallyContext _context;

        public RaceRepository(DakarRallyContext dakarRallyContext)
        {
            _context = dakarRallyContext;
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

        //public async Task Update(Race race)
        //{
        //    var raceEntity = race.ToEntity();
        //    await _context.Races.AddAsync(raceEntity);

        //    foreach (var vehicle in race.Vehicles)
        //    {
        //        var vehicleEntity = vehicle.ToEntity();
        //        await _context.Vehicles.AddAsync(vehicleEntity);
        //    }

        //    await SaveChanges();
        //}

        public async Task Upsert(Race race)
        {
            var raceEntity = await _context.Races.FirstOrDefaultAsync(r => r.Id.Equals(race.Id));
            if (raceEntity == null)
                await _context.Races.AddAsync(race.ToEntity());
            else
                _context.Races.Update(race.ToEntity());

            await SaveChanges();
        }

        private async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
