using DakarRally.Infrastructure.Repositories;
using DakarRally.Models.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DakarRally.Consumers
{
    public class RaceProgression
    {
        private readonly IAmRaceRepository _raceRepository;

        public RaceProgression(IAmRaceRepository raceRepository)
        {
            _raceRepository = raceRepository;
            DomainEvents.Register<RaceProgressionChecked>(async ev => await ProcessRaceProgression(ev));
        }

        //public RaceProgression()
        //{
        //    DomainEvents.Register<RaceProgressionChecked>(async ev => await ProcessRaceProgression(ev));
        //}

        private async Task ProcessRaceProgression(RaceProgressionChecked ev)
        {
            await _raceRepository.Update(ev.Race);
        }
    }
}
