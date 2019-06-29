using DakarRally.Enums;
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
                throw new ArgumentException("Year cannot be in past.");
            if (rallyTotalDistance == 0)
                throw new ArgumentException("Total distance from start to finish must be greater than 0.");

            return new Race(Guid.NewGuid(), year, new List<Vehicle>(), RaceStatus.Pending, rallyTotalDistance);
        }

        public static Race Create(Guid id, int year, List<Vehicle> vehicles, RaceStatus status, int distance)
        {
            return new Race(id, year, vehicles, status, distance);
        }

        public void StartRace()
        {
            Status = RaceStatus.Running;

            while (Status != RaceStatus.Finished)
            {
                foreach (var vehicle in Vehicles)
                {
                    vehicle.UpdateStatus(30, Distance);
                }

                Status = CheckRaceStatus();

                Thread.Sleep(30000);
            }
        }

        private RaceStatus CheckRaceStatus()
        {
            var isFinished = Vehicles.All(vehicle => vehicle.Status != VehicleStatus.Running && vehicle.Status != VehicleStatus.LightMalfunction);
            return isFinished ? RaceStatus.Finished : Status;
        }
    }
}
