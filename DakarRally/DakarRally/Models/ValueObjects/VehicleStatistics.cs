namespace DakarRally.Models.ValueObjects
{
    public class VehicleStatistics
    {
        public VehicleStatistics(string status, double distance, string finishTime)
        {
            Status = status;
            Distance = distance;
            FinishTime = finishTime;
        }

        public string Status { get; }
        public double Distance { get; }
        public string FinishTime { get; }
    }
}
