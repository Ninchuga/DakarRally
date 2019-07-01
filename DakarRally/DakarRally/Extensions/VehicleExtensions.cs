using DakarRally.Enums;
using DakarRally.Models;
using DakarRally.Models.DTOs;
using DakarRally.Models.Entities;
using DakarRally.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DakarRally.Extensions
{
    public static class VehicleExtensions
    {
        public static Models.Domain.Vehicle ToDomain(this Vehicle vehicle)
        {
            Enum.TryParse(vehicle.Type, out VehicleType vehicleType);
            Enum.TryParse(vehicle.Status, out VehicleStatus vehicleStatus);
            return new Models.Domain.Vehicle(vehicle.Id, vehicle.TeamName, vehicle.Model, vehicle.ManufacturingDate, vehicleType, vehicleStatus, vehicle.Distance, vehicle.FinishTime);
        }

        public static Vehicle ToEntity(this Models.Domain.Vehicle vehicle)
        {
            return new Vehicle
            {
                Id = Guid.NewGuid(),
                Distance = vehicle.Distance,
                FinishTime = vehicle.FinishTime,
                ManufacturingDate = vehicle.ManufacturingDate,
                Model = vehicle.Model,
                Status = vehicle.Status.ToString(),
                TeamName = vehicle.TeamName,
                Type = vehicle.Type.ToString()
            };
        }

        public static List<Vehicle> ToEntityList(this List<Models.Domain.Vehicle> vehicles)
        {
            return vehicles.Select(vehicle => vehicle.ToEntity()).ToList();
        }

        public static List<Models.Domain.Vehicle> ToDomainList(this List<Vehicle> vehicles)
        {
            return vehicles.Select(vehicle => vehicle.ToDomain()).ToList();
        }

        public static List<VehicleDto> ToVehicleDtoList(this List<Models.Domain.Vehicle> vehicles)
        {
            return vehicles.Select(vehicle => vehicle.ToVehicleDto()).ToList();
        }

        public static VehicleDto ToVehicleDto(this Models.Domain.Vehicle vehicle)
        {
            return new VehicleDto
            {
                Id = vehicle.Id,
                Distance = vehicle.Distance,
                FinishTime = vehicle.FinishTime,
                ManufacturingDate = vehicle.ManufacturingDate,
                Model = vehicle.Model,
                Status = vehicle.Status.ToString(),
                TeamName = vehicle.TeamName,
                Type = vehicle.Type.ToString()
            };
        }

        public static Models.Domain.Vehicle ToDomain(this VehicleDto vehicle)
        {
            Enum.TryParse(vehicle.Type, out VehicleType vehicleType);
            Enum.TryParse(vehicle.Status, out VehicleStatus vehicleStatus);
            return new Models.Domain.Vehicle(vehicle.Id, vehicle.TeamName, vehicle.Model, vehicle.ManufacturingDate, vehicleType, vehicleStatus, vehicle.Distance,
                vehicle.FinishTime);
        }

        public static UpsertVehicle ToUpsertVehicle(this UpsertVehicleDto vehicle)
        {
            Enum.TryParse(vehicle.Type, out VehicleType vehicleType);
            return new UpsertVehicle(vehicle.Id, vehicle.TeamName, vehicle.Model, vehicle.ManufacturingDate, vehicleType);
        }

        public static Vehicle ToEntity(this VehicleDto dto)
        {
            return new Vehicle
            {
                Id = dto.Id,
                ManufacturingDate = dto.ManufacturingDate.Date,
                Model = dto.Model,
                TeamName = dto.TeamName,
                Distance = dto.Distance,
                Status = dto.Status,
                FinishTime = dto.FinishTime,
                Type = dto.Type
            };
        }

        public static VehicleDto ToDto(this Vehicle vehicle)
        {   
            return new VehicleDto
            {
                ManufacturingDate = vehicle.ManufacturingDate.Date,
                Model = vehicle.Model,
                TeamName = vehicle.TeamName,
                Type = vehicle.Type,
                FinishTime = vehicle.FinishTime,
                Status = vehicle.Status,
                Distance = vehicle.Distance
            };
        }

        public static VehicleStatisticsDto ToVehicleStatisticsDto(this VehicleStatistics vehicle)
        {
            return new VehicleStatisticsDto
            {
                FinishTime = vehicle.FinishTime,
                Distance = vehicle.Distance,
                Status = vehicle.Status.ToString()
            };
        }
    }
}
