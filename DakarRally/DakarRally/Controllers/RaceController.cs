using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DakarRally.Controllers
{
    public class RaceController : ApiController
    {


        public IHttpActionResult CreateRace(string year)
        {
            return Ok();
        }
    }
}
