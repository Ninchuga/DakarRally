using DakarRally.Enums;
using System;

namespace DakarRally.Models.ValueObjects
{
    public class UpsertVehicle
    {
        public UpsertVehicle(string teamName, string model, DateTime manufacturingDate, VehicleType type)
        {
            TeamName = teamName;
            Model = model;
            ManufacturingDate = manufacturingDate;
            Type = type;
        }

        public Guid Id { get; set; }
        public string TeamName { get; }
        public string Model { get; }
        public DateTime ManufacturingDate { get; }
        public VehicleType Type { get; }
    }
}
