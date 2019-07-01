using DakarRally.Enums;

namespace DakarRally.Models.ValueObjects
{
    public class RaceStatus
    {
        public RaceStatus(RaceStatusType status, int numberOfVehiclesWithPendingStatus, int numberOfVehiclesWithHeavyMalfunctionStatus, int numberOfVehiclesWithLightMalfunctionStatus,
            int numberOfVehiclesWithRunningStatus, int numberOfVehiclesWithFinishedStatus, int numberOfSportCars, int numberOfTerrainCars, int numberOfTrucks, int numberOfSportMotorcycles,
            int numberOfCrossMotorcycles)
        {
            Status = status;
            NumberOfVehiclesWithPendingStatus = numberOfVehiclesWithPendingStatus;
            NumberOfVehiclesWithHeavyMalfunctionStatus = numberOfVehiclesWithHeavyMalfunctionStatus;
            NumberOfVehiclesWithLightMalfunctionStatus = numberOfVehiclesWithLightMalfunctionStatus;
            NumberOfVehiclesWithRunningStatus = numberOfVehiclesWithRunningStatus;
            NumberOfVehiclesWithFinishedStatus = numberOfVehiclesWithFinishedStatus;
            NumberOfSportCars = numberOfSportCars;
            NumberOfTerrainCars = numberOfTerrainCars;
            NumberOfTrucks = numberOfTrucks;
            NumberOfSportMotorcycles = numberOfSportMotorcycles;
            NumberOfCrossMotorcycles = numberOfCrossMotorcycles;
        }

        public RaceStatusType Status { get; }
        public int NumberOfVehiclesWithPendingStatus { get; }
        public int NumberOfVehiclesWithHeavyMalfunctionStatus { get; }
        public int NumberOfVehiclesWithLightMalfunctionStatus { get; }
        public int NumberOfVehiclesWithRunningStatus { get; }
        public int NumberOfVehiclesWithFinishedStatus { get; }
        public int NumberOfSportCars { get; }
        public int NumberOfTerrainCars { get; }
        public int NumberOfTrucks { get; }
        public int NumberOfSportMotorcycles { get; }
        public int NumberOfCrossMotorcycles { get; }
    }
}
