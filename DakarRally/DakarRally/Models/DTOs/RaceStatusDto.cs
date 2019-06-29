namespace DakarRally.Models.DTOs
{
    public class RaceStatusDto
    {
        public string Status { get; set; }

        public int NumberOfVehiclesWithPendingStatus { get; set; }
        public int NumberOfVehiclesWithHeavyMalfunctionStatus { get; set; }
        public int NumberOfVehiclesWithLightMalfunctionStatus { get; set; }
        public int NumberOfVehiclesWithRunningStatus { get; set; }
        public int NumberOfVehiclesWithFinishedStatus { get; set; }

        public int NumberOfSportCars { get; set; }
        public int NumberOfTerrainCars { get; set; }
        public int NumberOfTrucks { get; set; }
        public int NumberOfSportMotorcycles { get; set; }
        public int NumberOfCrossMotorcycles { get; set; }
    }

}
