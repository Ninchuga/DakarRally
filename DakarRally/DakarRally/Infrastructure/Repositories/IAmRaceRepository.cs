using DakarRally.DTOs;
using DakarRally.Models;
using System.Collections.Generic;

namespace DakarRally.Infrastructure.Repositories
{
    public interface IAmRaceRepository
    {
        void CreateRace(int year);
        List<Race> AllRaces();
        void AddVehicle(VehicleDto vehicle);
        Race RaceBy(int year);
    }
}