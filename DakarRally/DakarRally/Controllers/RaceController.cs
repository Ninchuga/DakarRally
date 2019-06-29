﻿using DakarRally.Extensions;
using DakarRally.Models.DTOs;
using DakarRally.Models.Entities;
using DakarRally.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DakarRally.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class RaceController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAmRaceService _raceService;

        public RaceController(IAmRaceService raceService, IConfiguration configuration)
        {
            _configuration = configuration;
            _raceService = raceService;
        }

        [HttpPost]
        [Route("/create")]
        public async Task<ActionResult> CreateRace(int year)
        {
            int.TryParse(_configuration["RallyTotalDistance"], out int rallyTotalDistance);
            await _raceService.CreateRace(year, rallyTotalDistance);

            return Ok();
        }

        [HttpGet]
        [Route("/getRace")]
        public async Task<ActionResult> GetRaceBy(int year)
        {
            var race = await _raceService.RaceBy(year);

            return Ok(race);
        }

        [HttpGet]
        [Route("/getAllRaces")]
        public async Task<ActionResult<List<Race>>> GetAllRaces()
        {
            var races = await _raceService.AllRaces();

            return Ok(races);
        }

        [HttpPost]
        [Route("/addVehicle")]
        public async Task<ActionResult> AddVehicle([FromBody] UpsertVehicleDto vehicle)
        {
            await _raceService.AddVehicle(vehicle.ToUpsertVehicle());

            return Ok();
        }

        [HttpDelete]
        [Route("/removeVehicle")]
        public async Task<ActionResult> RemoveVehicleFromRace(Guid vehicleId)
        {
            await _raceService.RemoveVehicleBy(vehicleId);

            return Ok();
        }

        [HttpPut]
        [Route("/updateVehicleInfo")]
        public async Task<ActionResult> UpdateVehicleInfo([FromBody] UpsertVehicleDto vehicle)
        {
            await _raceService.UpdateVehicleInfo(vehicle.ToUpsertVehicle());

            return Ok();
        }

        [HttpPost]
        [Route("/startRace")]
        public async Task<ActionResult> StartRace(Guid raceId)
        {
            await _raceService.StartRaceBy(raceId);

            return Ok();
        }

        [HttpGet]
        [Route("/raceStatus")]
        public async Task<ActionResult<RaceStatusDto>> RaceStatus(Guid raceId)
        {
            var response = await _raceService.RaceStatusBy(raceId);

            return Ok(response);
        }
    }
}
