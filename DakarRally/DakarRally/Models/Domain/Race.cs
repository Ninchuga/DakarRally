using DakarRally.Enums;
using DakarRally.Models.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DakarRally.Models.Domain
{
    public class Race
    {
        private Race(Guid id, int year, List<Vehicle> vehicles, RaceStatus status, int distance)
        {
            Id = id;
            Year = year;
            Vehicles = vehicles;
            Status = status;
            Distance = distance;
        }

        public Guid Id { get; set; }
        public int Year { get; }
        public List<Vehicle> Vehicles { get; }
        public RaceStatus Status { get; private set; }
        public int Distance { get; }

        public static Race Create(int year, int rallyTotalDistance)
        {
            if (year < DateTime.Now.Year)
                throw new ArgumentException("Year cannot be in the past.");
            if (rallyTotalDistance == 0)
                throw new ArgumentException("Total distance from start to finish must be greater than 0.");

            return new Race(Guid.NewGuid(), year, new List<Vehicle>(), RaceStatus.Pending, rallyTotalDistance);
        }

        public static Race Create(Guid id, int year, List<Vehicle> vehicles, RaceStatus status, int distance)
        {
            return new Race(id, year, vehicles, status, distance);
        }

        public void StartRace(int checkRaceProgressionInSeconds)
        {
            Status = RaceStatus.Running;

            while (Status != RaceStatus.Finished)
            {
                foreach (var vehicle in Vehicles)
                {
                    vehicle.UpdateStatus(checkRaceProgressionInSeconds, Distance);
                }

                Status = CheckRaceStatus();

                DomainEvents.Raise(new RaceProgressionChecked(this));

                Thread.Sleep(checkRaceProgressionInSeconds * 100);
            }
        }

        private RaceStatus CheckRaceStatus()
        {
            var isFinished = Vehicles.All(vehicle => vehicle.Status != VehicleStatus.Running && vehicle.Status != VehicleStatus.LightMalfunction);
            return isFinished ? RaceStatus.Finished : Status;
        }
    }
}
