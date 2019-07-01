using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DakarRally.Models.DTOs
{
    public class RaceDto
    {
        public Guid Id { get; set; }
        public int Year { get; set; }
        public List<VehicleDto> Vehicles { get; set; }
        public string Status { get; set; }
        public int Distance { get; set; }
    }
}
