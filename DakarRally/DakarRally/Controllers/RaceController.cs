using DakarRally.DTOs;
using DakarRally.Infrastructure.Repositories;
using DakarRally.Models;
using Microsoft.AspNetCore.Mvc;
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
        public ActionResult CreateRace(int year)
        {
            _raceRepository.CreateRace(year);

            return Ok();
        }

        [HttpGet]
        [Route("/getRace")]
        public ActionResult GetRaceBy(int year)
        {
            var race = _raceRepository.RaceBy(year);

            return Ok(race);
        }

        [HttpGet]
        [Route("/getAllRaces")]
        public ActionResult<List<Race>> GetAllRaces()
        {
            var races = _raceRepository.AllRaces();

            return Ok(races);
        }

        [HttpPost]
        [Route("/addVehicle")]
        public ActionResult AddVehicle([FromBody] VehicleDto vehicleDto)
        {
            _raceRepository.AddVehicle(vehicleDto);

            return Ok();
        }
    }
}
