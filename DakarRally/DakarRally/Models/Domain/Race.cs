using DakarRally.Enums;
using DakarRally.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DakarRally.Models.Domain
{
    public class Race
    {
        private Race(Guid id, int year, List<Vehicle> vehicles, RaceStatusType status, int distance)
        {
            Id = id;
            Year = year;
            Vehicles = vehicles;
            Status = status;
            Distance = distance;
        }

        public Guid Id { get; }
        public int Year { get; }
        public List<Vehicle> Vehicles { get; private set; }
        public RaceStatusType Status { get; private set; }
        public int Distance { get; }

        public static Race Create(int year, int rallyTotalDistance)
        {
            if (year < DateTime.Now.Year)
                throw new ArgumentException("Year cannot be in the past.");
            if (rallyTotalDistance == 0)
                throw new ArgumentException("Total distance from start to finish must be greater than 0.");

            return new Race(Guid.NewGuid(), year, new List<Vehicle>(), RaceStatusType.Pending, rallyTotalDistance);
        }

        public static Race Create(Guid id, int year, List<Vehicle> vehicles, RaceStatusType status, int distance)
        {
            return new Race(id, year, vehicles, status, distance);
        }

        public Race AddVehicle(UpsertVehicle vehicle)
        {
            if (Status == RaceStatusType.Running)
                throw new Exception("Cannot add vehicle to the race that is running.");
            if (Vehicles.Any(v => v.TeamName.Equals(vehicle.TeamName)))
                throw new Exception($"Team name '{vehicle.TeamName}' is already taken.");

            Vehicles.Add(new Vehicle(vehicle.Id, vehicle.TeamName, vehicle.Model, vehicle.ManufacturingDate, vehicle.Type, VehicleStatus.Pending, 0, string.Empty));

            return this;
        }

        public Race RemoveVehicleBy(Guid vehicleId)
        {
            if (Status == RaceStatusType.Running)
                throw new Exception("Vehicle cannot be removed from the race while the race is running.");

            var vehicleToRemove = Vehicles.FirstOrDefault(vehicle => vehicle.Id.Equals(vehicleId));
            Vehicles.Remove(vehicleToRemove);

            return this;
        }

        public Race UpdateVehicleInfo(UpsertVehicle vehicle)
        {
            if (Status == RaceStatusType.Running)
                throw new Exception("Cannot update vehicle info while the race is running.");
            if (Vehicles.Any(v => v.TeamName.Equals(vehicle.TeamName)))
                throw new Exception($"Team name '{vehicle.TeamName}' is already taken.");

            Vehicles.FirstOrDefault(v => v.Id.Equals(vehicle.Id)).UpdateInfo(vehicle);

            return this;
        }

        public Race StartRace(int checkRaceProgressionInSeconds)
        {
            Status = RaceStatusType.Running;
            foreach (var vehicle in Vehicles)
            {
                vehicle.UpdateStatus(checkRaceProgressionInSeconds, Distance);
            }

            Status = CheckRaceStatus();

            return this;
        }

        public List<Vehicle> AllVehiclesLeaderBoard()
        {
            return Vehicles.OrderBy(v => v.Type).ToList();
        }

        public List<Vehicle> LeaderBoardForVehicleType(VehicleType vehicleType)
        {
            return Vehicles.Where(v => v.Type.Equals(vehicleType)).ToList();
        }

        public RaceStatus RaceStatus()
        {
            var numberOfVehiclesWithPendingStatus = Vehicles.Where(v => v.Status == VehicleStatus.Pending).Count();
            var numberOfVehiclesWithHeavyMalfunctionStatus = Vehicles.Where(v => v.Status == VehicleStatus.HeavyMalfunction).Count();
            var numberOfVehiclesWithLightMalfunctionStatus = Vehicles.Where(v => v.Status == VehicleStatus.LightMalfunction).Count();
            var numberOfVehiclesWithRunningStatus = Vehicles.Where(v => v.Status == VehicleStatus.Running).Count();
            var numberOfVehiclesWithFinishedStatus = Vehicles.Where(v => v.Status == VehicleStatus.Finished).Count();

            var numberOfSportCars = Vehicles.Where(v => v.Type == VehicleType.SportsCar).Count();
            var numberOfTerrainCars = Vehicles.Where(v => v.Type == VehicleType.TerrainCar).Count();
            var numberOfTrucks = Vehicles.Where(v => v.Type == VehicleType.Truck).Count();
            var numberOfSportMotorcycles = Vehicles.Where(v => v.Type == VehicleType.SportMotorcycle).Count();
            var numberOfCrossMotorcycles = Vehicles.Where(v => v.Type == VehicleType.CrossMotorcycle).Count();

            return new RaceStatus(Status, numberOfVehiclesWithPendingStatus, numberOfVehiclesWithHeavyMalfunctionStatus, numberOfVehiclesWithLightMalfunctionStatus,
                numberOfVehiclesWithRunningStatus, numberOfVehiclesWithFinishedStatus, numberOfSportCars, numberOfTerrainCars, numberOfTrucks, numberOfSportMotorcycles, numberOfCrossMotorcycles);
        }

        public List<Vehicle> FindVehicleBy(string teamName, string model, string status)
        {
            if (!string.IsNullOrWhiteSpace(teamName) && !string.IsNullOrWhiteSpace(model) && !string.IsNullOrWhiteSpace(status))
            {
                Enum.TryParse(status, out VehicleStatus vehicleStatus);
                return Vehicles.Where(v => v.TeamName.Equals(teamName)).Where(v => v.Model.Equals(model)).Where(v => v.Status.Equals(vehicleStatus)).ToList();
            }
            if(!string.IsNullOrWhiteSpace(teamName) && !string.IsNullOrWhiteSpace(model) && string.IsNullOrWhiteSpace(status))
                return Vehicles.Where(v => v.TeamName.Equals(teamName)).Where(v => v.Model.Equals(model)).ToList();
            if (!string.IsNullOrWhiteSpace(teamName) && string.IsNullOrWhiteSpace(model) && !string.IsNullOrWhiteSpace(status))
            {
                Enum.TryParse(status, out VehicleStatus vehicleStatus);
                return Vehicles.Where(v => v.TeamName.Equals(teamName)).Where(v => v.Status.Equals(vehicleStatus)).ToList();
            }
            if (string.IsNullOrWhiteSpace(teamName) && !string.IsNullOrWhiteSpace(model) && !string.IsNullOrWhiteSpace(status))
            {
                Enum.TryParse(status, out VehicleStatus vehicleStatus);
                return Vehicles.Where(v => v.Model.Equals(model)).Where(v => v.Status.Equals(vehicleStatus)).ToList();
            }
            if (!string.IsNullOrWhiteSpace(teamName) && string.IsNullOrWhiteSpace(model) && string.IsNullOrWhiteSpace(status))
                return Vehicles.Where(v => v.TeamName.Equals(teamName)).ToList();
            if (string.IsNullOrWhiteSpace(teamName) && !string.IsNullOrWhiteSpace(model) && string.IsNullOrWhiteSpace(status))
                return Vehicles.Where(v => v.Model.Equals(model)).ToList();
            if (string.IsNullOrWhiteSpace(teamName) && string.IsNullOrWhiteSpace(model) && !string.IsNullOrWhiteSpace(status))
            {
                Enum.TryParse(status, out VehicleStatus vehicleStatus);
                return Vehicles.Where(v => v.Status.Equals(vehicleStatus)).ToList();
            }

            return new List<Vehicle>();
        }

        public VehicleStatistics VehicleStatisticsBy(Guid vehicleId)
        {
            return Vehicles.FirstOrDefault(v => v.Id.Equals(vehicleId)).VehicleStatistics();
        }

        private RaceStatusType CheckRaceStatus()
        {
            var isFinished = Vehicles.All(vehicle => vehicle.Status != VehicleStatus.Running && vehicle.Status != VehicleStatus.LightMalfunction);
            return isFinished ? RaceStatusType.Finished : Status;
        }
    }
}
