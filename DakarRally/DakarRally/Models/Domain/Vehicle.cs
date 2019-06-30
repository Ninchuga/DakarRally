using DakarRally.Enums;
using DakarRally.Helpers;
using DakarRally.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DakarRally.Models.Domain
{
    public class Vehicle
    {
        public Vehicle(Guid id, string teamName, string model, DateTime manufacturingDate, VehicleType type, VehicleStatus status, double distance, string finishTime)
        {
            Id = id;
            TeamName = teamName;
            Model = model;
            ManufacturingDate = manufacturingDate;
            Type = type;
            Status = status;
            Distance = distance;
            FinishTime = finishTime;
            //RepairmentHours = repairmentHours;
            //TimeRacing = timeRacing;
        }

        public Guid Id { get; set; }
        public string TeamName { get; private set; }
        public string Model { get; private set; }
        public DateTime ManufacturingDate { get; private set; }
        public VehicleType Type { get; private set; }
        public VehicleStatus Status { get; private set; }
        public double Distance { get; private set; }
        public string FinishTime { get; private set; }
        public int RepairmentHours { get; private set; }
        public int TimeRacing { get; private set; }
        public int TimeFromBeginningOfRaceInSeconds { get; set; }

        public Vehicle UpdateStatus(int checkVehicleStatusTimeInSeconds, int raceTotalDistance)
        {
            if(Status == VehicleStatus.Pending)
                Status = VehicleStatus.Running;

            if (Distance >= raceTotalDistance)
            {
                Status = VehicleStatus.Finished;
                TimeSpan result = TimeSpan.FromSeconds(TimeFromBeginningOfRaceInSeconds);
                string raceFinishTime = result.ToString("hh':'mm");
                FinishTime = raceFinishTime;
            }

            if (Status == VehicleStatus.Running)
            {
                TimeFromBeginningOfRaceInSeconds = TimeFromBeginningOfRaceInSeconds + checkVehicleStatusTimeInSeconds;
                TimeRacing = TimeRacing + checkVehicleStatusTimeInSeconds;
                var maxSpeed = GetMaxSpeed(Type);

                Distance = maxSpeed * TimeRacing;

                var currentStatus = MalfunctionHelper.CalculateMalfunctionBy(Type);

                Status = currentStatus;
            }

            if(Status == VehicleStatus.LightMalfunction)
            {
                TimeFromBeginningOfRaceInSeconds = TimeFromBeginningOfRaceInSeconds + checkVehicleStatusTimeInSeconds;
                var repairementStatus = MalfunctionHelper.RepairementHoursBy(Type, ++RepairmentHours);
                Status = repairementStatus.VehicleStatus;
                RepairmentHours = repairementStatus.RepairementHours;
            }

            return this;
        }

        public Vehicle UpdateInfo(UpsertVehicle vehicle)
        {
            TeamName = vehicle.TeamName;
            Model = vehicle.Model;
            ManufacturingDate = vehicle.ManufacturingDate;
            Type = vehicle.Type;

            return this;
        }

        private double GetMaxSpeed(VehicleType type)
        {
            switch (type)
            {
                case VehicleType.SportsCar:
                    return 140;
                case VehicleType.TerrainCar:
                    return 100;
                case VehicleType.Truck:
                    return 80;
                case VehicleType.SportMotorcycle:
                    return 85;
                case VehicleType.CrossMotorcycle:
                    return 130;
                default:
                    return 0;
            }
        }
    }
}
