﻿using System;

namespace DakarRally.DTOs
{
    public class VehicleDto
    {
        public Guid Id { get; set; }
        public string TeamName { get; set; }
        public string Model { get; set; }
        public DateTime ManufacturingDate { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public double Distance { get; set; }
        public DateTime FinishTime { get; set; }
    }
}
