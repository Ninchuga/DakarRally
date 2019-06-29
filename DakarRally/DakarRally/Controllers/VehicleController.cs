using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DakarRally.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {


        //[HttpPut]
        //[Route("/updateVehicleInfo")]
        //public ActionResult UpdateVehicleInfo(VehicleDto vehicleDto)
        //{
        //    throw new NotImplementedException();
        //}

        [HttpGet]
        [Route("/getLeaderboardForAllVehicles")]
        public ActionResult GetLeaderboardForAllVehicles()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("/getLeaderboardForVehicleType")]
        public ActionResult GetLeaderboardForVehicleType(string type)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("/getVehicleStatistics")]
        public ActionResult GetVehicleStatistics(Guid vehicleId)
        {
            throw new NotImplementedException();
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