using System;

namespace DakarRally.Models
{
    public class Vehicle : Entity
    {
        public string TeamName { get; set; }
        public string Model { get; set; }
        public DateTime ManufacturingDate { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public double  Distance { get; set; }
        public DateTime FinishTime { get; set; } // maybe this should be int or DateTime in Hours
    }
}
