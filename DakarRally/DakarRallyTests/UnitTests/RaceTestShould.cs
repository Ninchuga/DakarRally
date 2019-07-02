using DakarRally.Models.Domain;
using System;
using Xunit;
using FluentAssertions;
using DakarRally.Enums;
using DakarRallyTests.Builders;
using System.Linq;
using System.Collections.Generic;

namespace DakarRallyTests.UnitTests
{
    public class RaceTestShould
    {
        [Fact]
        public void CreateNewRaceForCurrentYear()
        {
            var year = DateTime.Now.Year;
            var rallyTotalDistanceInKm = 100;

            var newRace = Race.Create(year, rallyTotalDistanceInKm);

            newRace.Year.Should().Be(year);
            newRace.Distance.Should().Be(rallyTotalDistanceInKm);
            newRace.Vehicles.Should().BeEmpty();
            newRace.Status.Should().Be(RaceStatusType.Pending);
        }

        [Fact]
        public void ThrowArgumentExceptionWhenCreatingNewRaceIfTheYearIsInPast()
        {
            var year = 2018;
            var rallyTotalDistanceInKm = 100;

            Exception ex = Assert.Throws<ArgumentException>(() => Race.Create(year, rallyTotalDistanceInKm));

            ex.Message.Should().Be("Year cannot be in the past.");
        }

        [Fact]
        public void ThrowArgumentExceptionWhenCreatingNewRaceIfTotalDistanceInKmIsZero()
        {
            var year = 2019;
            var rallyTotalDistanceInKm = 0;

            Exception ex = Assert.Throws<ArgumentException>(() => Race.Create(year, rallyTotalDistanceInKm));

            ex.Message.Should().Be("Total distance from start to finish must be greater than 0.");
        }

        [Fact]
        public void AddVehicleToTheRace()
        {
            var vehicleToAdd = VehicleBuilder.BuildUpsertVehicle();

            var race = RaceBuilder.BuildWithoutVehicles().AddVehicle(vehicleToAdd);

            race.Vehicles.Should().NotBeEmpty();
            race.Vehicles.First().Should().BeEquivalentTo(vehicleToAdd);
        }

        [Fact]
        public void ThrowExceptionIfTheRaceIsRunningAndVehicleIsTryingToBeAddedToTheRace()
        {
            var vehicleToAdd = VehicleBuilder.BuildUpsertVehicle();
            var race = RaceBuilder.BuildRaceWithRunningStatus();

            Exception ex = Assert.Throws<Exception>(() => race.AddVehicle(vehicleToAdd));

            ex.Message.Should().Be("Cannot add vehicle to the race that is running.");
        }

        [Fact]
        public void ThrowExceptionIfTheTeamNameIsAlreadyTakenWhenAddingNewCarToTheRace()
        {
            var vehicle = VehicleBuilder.Build();
            var vehicleToAdd = VehicleBuilder.BuildUpsertVehicleWithId(vehicle.Id);
            var race = RaceBuilder.BuildWithVehicles(vehicle);

            Exception ex = Assert.Throws<Exception>(() => race.AddVehicle(vehicleToAdd));

            ex.Message.Should().Be($"Team name '{vehicleToAdd.TeamName}' is already taken.");
        }

        [Fact]
        public void RemoveVehicleFromTheRace()
        {
            var vehicle = VehicleBuilder.BuildWithRunningStatus();

            var race = RaceBuilder.BuildWithVehicle(vehicle).RemoveVehicleBy(vehicle.Id);

            race.Vehicles.Should().NotContain(vehicle);
        }

        [Fact]
        public void ThrowExceptionIfTheRaceIsRunningAndVehicleIsTryingToBeRemovedFromTheRace()
        {
            var vehicle = VehicleBuilder.BuildWithRunningStatus();
            var race = RaceBuilder.BuildWithVehicleAndRunningStatus(vehicle);

            Exception ex = Assert.Throws<Exception>(() => race.RemoveVehicleBy(vehicle.Id));

            ex.Message.Should().Be("Vehicle cannot be removed from the race while the race is running.");
        }

        [Fact]
        public void UpdateVehicleInfo()
        {
            var vehicle = VehicleBuilder.Build();
            var vehicleToUpdate = VehicleBuilder.BuildUpsertVehicleWithIdAndTeamNameAndModel(vehicle.Id, "UpdatedTeamName", "UpdatedModel");

            var race = RaceBuilder.BuildWithVehicle(vehicle).UpdateVehicleInfo(vehicleToUpdate);

            var actualVehicle = race.Vehicles.First(); 
            actualVehicle.Model.Should().Be(vehicleToUpdate.Model);
            actualVehicle.TeamName.Should().Be(vehicleToUpdate.TeamName);
            actualVehicle.Type.Should().Be(vehicleToUpdate.Type);
        }

        [Fact]
        public void ThrowExceptionIfTheRaceIsRunningAndVehicleIsTryingToBeUpdated()
        {
            var vehicle = VehicleBuilder.BuildWithRunningStatus();
            var updatedVehicle = VehicleBuilder.BuildUpsertVehicleWithIdAndTeamNameAndModel(vehicle.Id, "UpdatedTeamName", "UpdatedModel");
            var race = RaceBuilder.BuildWithVehicleAndRunningStatus(vehicle);

            Exception ex = Assert.Throws<Exception>(() => race.UpdateVehicleInfo(updatedVehicle));

            ex.Message.Should().Be("Cannot update vehicle info while the race is running.");
        }

        [Fact]
        public void ThrowExceptionIfTheTeamNameIsAlreadyTakenWhileTryingToUpdateVehicleInfo()
        {
            var vehicle = VehicleBuilder.Build();
            var vehicleToUpdate = VehicleBuilder.BuildUpsertVehicleWithId(vehicle.Id);
            var race = RaceBuilder.BuildWithVehicles(vehicle);

            Exception ex = Assert.Throws<Exception>(() => race.UpdateVehicleInfo(vehicleToUpdate));

            ex.Message.Should().Be($"Team name '{vehicleToUpdate.TeamName}' is already taken.");
        }

        [Fact]
        public void StartRace()
        {
            var checkRaceProgressionInSeconds = 10;
            var vehicle = VehicleBuilder.Build();
            var vehicle2 = VehicleBuilder.Build();

            var race = RaceBuilder.BuildWithVehicles(vehicle, vehicle2).StartRace(checkRaceProgressionInSeconds);

            var vehiclesStatusIsNotPending = race.Vehicles.All(v => v.Status != VehicleStatus.Pending);
            race.Status.Should().NotBe(RaceStatusType.Pending);
            race.Vehicles.Should().NotBeEmpty();
            vehiclesStatusIsNotPending.Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(VehiclesLeaderBoardData))]
        public void ReturnAllVehiclesLeaderBoard(Vehicle vehicle1, Vehicle vehicle2, Vehicle vehicle3)
        {
            var vehicles = RaceBuilder.BuildWithVehicles(vehicle1, vehicle2, vehicle3).AllVehiclesLeaderBoard();

            vehicles.Should().HaveCount(3);
            vehicles.Should().Contain(vehicle1);
            vehicles.Should().Contain(vehicle2);
            vehicles.Should().Contain(vehicle3);
        }

        [Theory]
        [MemberData(nameof(VehiclesLeaderBoardDataWithType))]
        public void ReturnLeaderBoardForVehicleType(Vehicle vehicle1, Vehicle vehicle2, Vehicle vehicle3, VehicleType vehicleType)
        {
            var vehicles = RaceBuilder.BuildWithVehicles(vehicle1, vehicle2, vehicle3).LeaderBoardForVehicleType(vehicleType);

            var vehiclesAllTheSameType = vehicles.All(v => v.Type.Equals(vehicleType));
            vehiclesAllTheSameType.Should().BeTrue();
        }

        [Theory]
        [InlineData("VR46", "187", "Pending")]
        [InlineData("Koksha", "187", "Pending")]
        [InlineData("Nino", "187", "Pending")]
        public void FindOneVehicleWhenAllThreeSearchParametersAreEntered(string teamName, string model, string vehicleStatus)
        {
            var vehicle1 = VehicleBuilder.Build();
            var vehicle2 = VehicleBuilder.BuildWithTeamNameAndModelAndStatus("Koksha", "187", VehicleStatus.Pending);
            var vehicle3 = VehicleBuilder.BuildWithTeamNameAndModelAndStatus("Nino", "187", VehicleStatus.Pending);

            var vehicles = RaceBuilder.BuildWithVehicles(vehicle1, vehicle2, vehicle3).FindVehicleBy(teamName, model, vehicleStatus);

            vehicles.Should().HaveCount(1);
        }

        [Theory]
        [InlineData("187", "Pending")]
        public void FindVehiclesByModelAndStatus(string model, string vehicleStatus)
        {
            var vehicle1 = VehicleBuilder.BuildWithTeamNameAndModelAndStatus("Koksha", "187", VehicleStatus.Pending);
            var vehicle2 = VehicleBuilder.BuildWithTeamNameAndModelAndStatus("Nino", "187", VehicleStatus.Pending);

            var vehicles = RaceBuilder.BuildWithVehicles(vehicle1, vehicle2).FindVehicleBy(string.Empty, model, vehicleStatus);

            vehicles.Should().HaveCount(2);
        }

        [Theory]
        [InlineData("Pending")]
        public void FindVehiclesByStatus(string vehicleStatus)
        {
            var vehicle1 = VehicleBuilder.BuildWithTeamNameAndModelAndStatus("Koksha", "Peugeot", VehicleStatus.Pending);
            var vehicle2 = VehicleBuilder.BuildWithTeamNameAndModelAndStatus("Nino", "Subaru", VehicleStatus.Pending);
            var vehicle3 = VehicleBuilder.BuildWithTeamNameAndModelAndStatus("Tibika", "Citroen", VehicleStatus.Running);

            var vehicles = RaceBuilder.BuildWithVehicles(vehicle1, vehicle2).FindVehicleBy(string.Empty, string.Empty, vehicleStatus);

            vehicles.Should().HaveCount(2);
        }

        [Fact]
        public void ReturnVehicleStatistics()
        {
            var vehicle = VehicleBuilder.Build();
            var vehicle2 = VehicleBuilder.BuildWithVehicleStatusAndDistanceAndFinishTime(VehicleStatus.Finished, 250, "20:33");

            var vehicleStatistics = RaceBuilder.BuildWithVehicles(vehicle, vehicle2).VehicleStatisticsBy(vehicle.Id);

            vehicleStatistics.Distance.Should().Be(vehicle.Distance);
            vehicleStatistics.Status.Should().Be(vehicle.Status);
            vehicleStatistics.FinishTime.Should().Be(vehicle.FinishTime);
        }

        public static IEnumerable<object[]> VehiclesLeaderBoardData =>
        new List<object[]>
        {
            new object[] { VehicleBuilder.BuildWithType(VehicleType.SportsCar), VehicleBuilder.BuildWithType(VehicleType.TerrainCar), VehicleBuilder.BuildWithType(VehicleType.TerrainCar)}
        };

        public static IEnumerable<object[]> VehiclesLeaderBoardDataWithType =>
        new List<object[]>
        {
            new object[] { VehicleBuilder.BuildWithType(VehicleType.SportsCar), VehicleBuilder.BuildWithType(VehicleType.TerrainCar), VehicleBuilder.BuildWithType(VehicleType.TerrainCar), VehicleType.TerrainCar },
            new object[] { VehicleBuilder.BuildWithType(VehicleType.SportsCar), VehicleBuilder.BuildWithType(VehicleType.SportsCar), VehicleBuilder.BuildWithType(VehicleType.TerrainCar), VehicleType.SportsCar }
        };
    }
}
