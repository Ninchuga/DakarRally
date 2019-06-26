using System;

namespace DakarRally.Models
{
    public class Vehicle
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime ManufacturingDate { get; set; }
    }
}