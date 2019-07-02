using DakarRally.Enums;
using DakarRally.Models.Domain;
using DakarRally.Models.ValueObjects;
using System;

namespace DakarRallyTests.Builders
{
    public class VehicleBuilder
    {
        public static Guid Id = Guid.NewGuid();
        public static string TeamName = "VR46";
        public static string Model = "187";
        public static DateTime ManufacturingDate = new DateTime(2015, 5, 5);
        public static VehicleType Type = VehicleType.SportMotorcycle;
        public static VehicleStatus Status = VehicleStatus.Pending;
        public static double Distance = 0;
        public static string FinishTime = string.Empty;
        public static int RepairmentHours = 0;
        public static double TotalTimeRacingInSeconds = 0;
        public static double TimeFromBeginningOfRaceInSeconds = 0;

        public static UpsertVehicle BuildUpsertVehicle()
        {
            return new UpsertVehicle(Id, TeamName, Model, ManufacturingDate, Type);
        }

        public static Vehicle Build()
        {
            return new Vehicle(Id, TeamName, Model, ManufacturingDate, Type, Status, Distance, FinishTime);
        }

        public static Vehicle BuildWithRunningStatus()
        {
            return new Vehicle(Id, TeamName, Model, ManufacturingDate, Type, VehicleStatus.Running, 20, FinishTime);
        }

        public static UpsertVehicle BuildUpsertVehicleWithIdAndTeamNameAndModel(Guid id, string teamName, string model)
        {
            return new UpsertVehicle(id, teamName, model, ManufacturingDate, VehicleType.CrossMotorcycle);
        }

        public static UpsertVehicle BuildUpsertVehicleWithId(Guid id)
        {
            return new UpsertVehicle(id, TeamName, Model, ManufacturingDate, VehicleType.CrossMotorcycle);
        }

        public static Vehicle BuildWithType(VehicleType type)
        {
            return new Vehicle(Id, TeamName, Model, ManufacturingDate, type, VehicleStatus.Running, Distance, FinishTime);
        }

        public static Vehicle BuildWithTypeAndStatus(VehicleType type, VehicleStatus vehicleStatus)
        {
            return new Vehicle(Id, TeamName, Model, ManufacturingDate, type, vehicleStatus, 10, FinishTime);
        }

        public static Vehicle BuildWithTeamNameAndModelAndStatus(string teamName, string model, VehicleStatus vehicleStatus)
        {
            return new Vehicle(Id, teamName, model, ManufacturingDate, Type, vehicleStatus, Distance, FinishTime);
        }

        public static Vehicle BuildWithVehicleStatusAndDistanceAndFinishTime(VehicleStatus vehicleStatus, double distance, string finishTime)
        {
            return new Vehicle(Id, TeamName, Model, ManufacturingDate, Type, Status, distance, finishTime);
        }

        public static Vehicle BuildWithDistance(double distance)
        {
            return new Vehicle(Id, TeamName, Model, ManufacturingDate, Type, VehicleStatus.Running, distance, FinishTime);
        }
    }
}
