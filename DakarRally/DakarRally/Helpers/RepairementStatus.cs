using DakarRally.Enums;

namespace DakarRally.Helpers
{
    public class RepairementStatus
    {
        public RepairementStatus(int repairementHours, VehicleStatus vehicleStatus)
        {
            RepairementHours = repairementHours;
            VehicleStatus = vehicleStatus;
        }

        public int RepairementHours { get; set; }
        public VehicleStatus VehicleStatus { get; set; }
    }
}
