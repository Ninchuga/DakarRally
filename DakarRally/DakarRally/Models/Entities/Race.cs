﻿using System;
using System.Collections.Generic;

namespace DakarRally.Models.Entities
{
    public class Race : Entity
    {
        public Race()
        {
            Vehicles = new List<Vehicle>();
        }

        public int Year { get; set; }
        public List<Vehicle> Vehicles { get; set; }
        public string Status { get; set; }
        public int Distance { get; set; }
    }
}
