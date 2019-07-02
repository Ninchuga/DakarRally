using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;
using DakarRallyTests.Builders;
using DakarRally.Enums;
using DakarRally.Models.Domain;

namespace DakarRallyTests.UnitTests
{
    public class VehicleTestShould
    {
        [Theory]
        [InlineData(30, 100)]
        public void UpdateStatusToRunningWhenRaceStarts(int checkVehicleStatusTimeInSeconds, int raceTotalDistance)
        {
            var vehicle = VehicleBuilder.Build();

            var actual = vehicle.UpdateStatus(checkVehicleStatusTimeInSeconds, raceTotalDistance);

            actual.Status.Should().Be(VehicleStatus.Running);
            actual.Distance.Should().Be(0);
        }

        [Theory]
        [MemberData(nameof(UpdateVehicleStatusWhenVehicleStartsRunningData))]
        public void UpdateStatusAndDistanceWhenVehicleStartsRunning(Vehicle vehicle, double vehicleMaxSpeed, int checkVehicleStatusTimeInSeconds, int raceTotalDistance)
        {
            double expectedDistance = vehicleMaxSpeed * checkVehicleStatusTimeInSeconds / 3600;

            var actual = vehicle.UpdateStatus(checkVehicleStatusTimeInSeconds, raceTotalDistance);

            actual.Distance.Should().Be(expectedDistance);
            actual.TimeFromBeginningOfRaceInSeconds.Should().Be(checkVehicleStatusTimeInSeconds);
            actual.TotalTimeRacingInSeconds.Should().Be(checkVehicleStatusTimeInSeconds);
        }

        [Theory]
        [InlineData(30, 100)]
        public void UpdateStatusAndStartCountingRepairementHoursForLightMalfunctionVehicle(int checkVehicleStatusTimeInSeconds, int raceTotalDistance)
        {
            var vehicle = VehicleBuilder.BuildWithTypeAndStatus(VehicleType.CrossMotorcycle, VehicleStatus.LightMalfunction);

            var actual = vehicle.UpdateStatus(checkVehicleStatusTimeInSeconds, raceTotalDistance);

            actual.Status.Should().Be(VehicleStatus.LightMalfunction);
            actual.Distance.Should().Be(vehicle.Distance);
            actual.RepairmentHours.Should().Be(1);
        }

        [Theory]
        [InlineData(30, 100, 100)]
        public void FinishTheRaceWhenDistanceIsEqualToTotalRallyDistance(int checkVehicleStatusTimeInSeconds, int raceTotalDistance, double vehicleDistance)
        {
            var vehicle = VehicleBuilder.BuildWithDistance(vehicleDistance);

            var actual = vehicle.UpdateStatus(checkVehicleStatusTimeInSeconds, raceTotalDistance);

            actual.Status.Should().Be(VehicleStatus.Finished);
        }

        public static IEnumerable<object[]> UpdateVehicleStatusWhenVehicleStartsRunningData =>
        new List<object[]>
        {
            new object[] { VehicleBuilder.BuildWithType(VehicleType.SportsCar), 140, 30, 100 },
            new object[] { VehicleBuilder.BuildWithType(VehicleType.TerrainCar), 100, 30, 100 },
            new object[] { VehicleBuilder.BuildWithType(VehicleType.Truck), 80, 30, 100 },
            new object[] { VehicleBuilder.BuildWithType(VehicleType.CrossMotorcycle), 130, 30, 100 },
            new object[] { VehicleBuilder.BuildWithType(VehicleType.SportMotorcycle), 85, 30, 100 }
        };
    }
}
