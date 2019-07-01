using DakarRally.Enums;

namespace DakarRally.Models.ValueObjects
{
    public class VehicleStatistics
    {
        public VehicleStatistics(VehicleStatus status, double distance, string finishTime)
        {
            Status = status;
            Distance = distance;
            FinishTime = finishTime;
        }

        public VehicleStatus Status { get; }
        public double Distance { get; }
        public string FinishTime { get; }
    }
}
