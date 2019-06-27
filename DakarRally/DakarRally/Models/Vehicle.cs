﻿using System;

namespace DakarRally.Models
{
    public class Vehicle : Entity
    {
        public string TeamName { get; set; }
        public string Model { get; set; }
        public DateTime ManufacturingDate { get; set; }
    }
}
