using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DakarRally.Models.Domain.Events
{
    public class RaceProgressionChecked : IAmDomainEvent
    {
        public RaceProgressionChecked(Race race)
        {
            Race = race;
        }

        public Race Race { get; }

    }
}
