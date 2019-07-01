using DakarRally.Enums;
using System;

namespace DakarRally.Helpers
{
    public class MalfunctionHelper
    {
        const int repairementHoursForCars = 5;
        const int repairementHoursForTrucks = 7;
        const int repairementHoursForMotorcycles = 3;

        public static VehicleStatus CalculateMalfunctionBy(VehicleType vehicleType)
        {
            Random random = new Random();
            var malfunctionPercentage = random.Next(0, 100);

            switch (vehicleType)
            {
                case VehicleType.SportsCar:
                    return CheckMalfunction(malfunctionPercentage, 2, 12);
                case VehicleType.TerrainCar:
                    return CheckMalfunction(malfunctionPercentage, 1, 3);
                case VehicleType.Truck:
                    return CheckMalfunction(malfunctionPercentage, 4, 6);
                case VehicleType.SportMotorcycle:
                    return CheckMalfunction(malfunctionPercentage, 10, 18);
                case VehicleType.CrossMotorcycle:
                    return CheckMalfunction(malfunctionPercentage, 2, 3);
                default:
                    throw new ArgumentException($"Vehicle type '{vehicleType}' doesn't exist.");
            }
        }

        public static RepairementStatus RepairementHoursBy(VehicleType vehicleType, int repairementHours)
        {
            if (vehicleType == VehicleType.SportsCar || vehicleType == VehicleType.TerrainCar)
            {
                return repairementHours == repairementHoursForCars ? new RepairementStatus(0, VehicleStatus.Running) 
                    : new RepairementStatus(repairementHours, VehicleStatus.LightMalfunction);
            }
            else if(vehicleType == VehicleType.CrossMotorcycle || vehicleType == VehicleType.SportMotorcycle)
            {
                return repairementHours == repairementHoursForMotorcycles ? new RepairementStatus(0, VehicleStatus.Running)
                    : new RepairementStatus(repairementHours, VehicleStatus.LightMalfunction);
            }
            else
            {
                return repairementHours == repairementHoursForTrucks ? new RepairementStatus(0, VehicleStatus.Running)
                    : new RepairementStatus(repairementHours, VehicleStatus.LightMalfunction);
            }
        }

        private static VehicleStatus CheckMalfunction(int malfunctionPercentage, int heavyMalfunctionPercentage, int lightMalfunctionPercentage)
        {
            if (malfunctionPercentage <= heavyMalfunctionPercentage)
                return VehicleStatus.HeavyMalfunction;
            else if (malfunctionPercentage >= heavyMalfunctionPercentage && malfunctionPercentage <= lightMalfunctionPercentage)
                return VehicleStatus.LightMalfunction;

            return VehicleStatus.Running;
        }
    }
}
