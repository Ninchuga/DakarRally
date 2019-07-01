using System;
using System.Threading.Tasks;
using Race = DakarRally.Models.Domain.Race;

namespace DakarRally.Infrastructure.Repositories
{
    public interface IAmRaceRepository
    {
        Task<Race> RaceBy(int year);
        Task<Race> RaceBy(Guid raceId);
        //Task Update(Race race);
        Task Upsert(Race race);
    }
}