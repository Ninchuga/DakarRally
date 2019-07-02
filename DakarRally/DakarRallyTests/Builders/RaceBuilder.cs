using DakarRally.Enums;
using DakarRally.Models.Domain;
using System;
using System.Collections.Generic;

namespace DakarRallyTests.Builders
{
    public class RaceBuilder
    {
        public static Guid Id = Guid.NewGuid();
        public static int Year = DateTime.Now.Year;
        public static List<Vehicle> Vehicles = new List<Vehicle>();
        public static RaceStatusType Status = RaceStatusType.Pending;
        public static int Distance = 100;

        public static Race BuildWithoutVehicles()
        {
            return Race.Create(Year, Distance);
        }

        public static Race BuildRaceWithRunningStatus()
        {
            return Race.Create(Id, Year, Vehicles, RaceStatusType.Running, Distance);
        }

        public static Race BuildWithVehicle(Vehicle vehicle)
        {
            Vehicles = new List<Vehicle> { vehicle };
            return Race.Create(Id, Year, Vehicles, Status, Distance);
        }

        public static Race BuildWithVehicles(params Vehicle[] vehicles)
        {
            Vehicles = new List<Vehicle>();
            Vehicles.AddRange(vehicles);
            return Race.Create(Id, Year, Vehicles, Status, Distance);
        }

        public static Race BuildWithStatusAndVehicles(RaceStatusType raceStatus, params Vehicle[] vehicles)
        {
            Vehicles = new List<Vehicle>();
            Vehicles.AddRange(vehicles);
            return Race.Create(Id, Year, Vehicles, raceStatus, Distance);
        }

        public static Race BuildWithVehicleAndRunningStatus(Vehicle vehicle)
        {
            Vehicles = new List<Vehicle> { vehicle };
            return Race.Create(Id, Year, Vehicles, RaceStatusType.Running, Distance);
        }
    }
}
