using System;

namespace DakarRally.Models.DTOs
{
    public class UpsertVehicleDto
    {
        public Guid Id { get; set; }
        public string TeamName { get; set; }
        public string Model { get; set; }
        public DateTime ManufacturingDate { get; set; }
        public string Type { get; set; }
    }
}
