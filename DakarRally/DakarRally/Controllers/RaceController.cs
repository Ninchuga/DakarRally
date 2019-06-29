using DakarRally.DTOs;
using DakarRally.Infrastructure.Repositories;
using DakarRally.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DakarRally.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class RaceController : ControllerBase
    {
        private readonly IAmRaceRepository _raceRepository;

        public RaceController(IAmRaceRepository raceRepository)
        {
            _raceRepository = raceRepository;
        }

        [HttpPost]
        [Route("/create")]
        public async Task<ActionResult> CreateRace(int year)
        {
            await _raceRepository.CreateRace(year);

            return Ok();
        }

        [HttpGet]
        [Route("/getRace")]
        public async Task<ActionResult> GetRaceBy(int year)
        {
            var race = await _raceRepository.RaceBy(year);

            return Ok(race);
        }

        [HttpGet]
        [Route("/getAllRaces")]
        public async Task<ActionResult<List<Race>>> GetAllRaces()
        {
            var races = await _raceRepository.AllRaces();

            return Ok(races);
        }

        [HttpPost]
        [Route("/addVehicle")]
        public async Task<ActionResult> AddVehicle([FromBody] VehicleDto vehicleDto)
        {
            await _raceRepository.AddVehicle(vehicleDto);

            return Ok();
        }

        [HttpDelete]
        [Route("/removeVehicle")]
        public async Task<ActionResult> RemoveVehicleFromRace(Guid vehicleId)
        {
            await _raceRepository.RemoveVehicleBy(vehicleId);

            return Ok();
        }

        [HttpPut]
        [Route("/updateVehicleInfo")]
        public async Task<ActionResult> UpdateVehicleInfo([FromBody] VehicleDto vehicle)
        {
            await _raceRepository.UpdateVehicleInfo(vehicle);

            return Ok();
        }

        [HttpPost]
        [Route("/startRace")]
        public async Task<ActionResult> StartRace(Guid raceId)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("/raceStatus")]
        public async Task<ActionResult<RaceStatusDto>> RaceStatus(Guid raceId)
        {
            var response = await _raceRepository.RaceStatusBy(raceId);

            return Ok(response);
        }
    }
}
