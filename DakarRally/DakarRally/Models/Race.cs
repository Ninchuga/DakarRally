using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DakarRally.Models
{
    public class Race
    {
        public Guid Id { get; set; }
        public string Year { get; set; }
        public List<Vehicle> Vehicles { get; set; }
        public string Status { get; set; }
    }
}