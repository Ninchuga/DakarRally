using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DakarRally.Enums;
using DakarRally.Extensions;
using DakarRally.Infrastructure.Repositories;
using DakarRally.Models.DTOs;
using DakarRally.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DakarRally.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IAmVehicleService _vehicleService;

        public VehicleController(IAmVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet]
        [Route("/getVehicleStatistics")]
        public async Task<ActionResult<VehicleStatisticsDto>> GetVehicleStatistics(Guid vehicleId)
        {
            var response = await _vehicleService.VehicleStatisticsBy(vehicleId);

            return Ok(response.ToVehicleStatisticsDto());
        }

        [HttpGet]
        [Route("/findVehicle")]
        public ActionResult FindVehicle(string teamName = null, string model = null, DateTime? manufacturingDate = null, string status = null, double? distance = null)
        {
            // sort order
            throw new NotImplementedException();
        }
    }
}