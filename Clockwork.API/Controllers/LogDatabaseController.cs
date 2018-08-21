using System;
using Microsoft.AspNetCore.Mvc;
using Clockwork.API.Models;
using System.Collections.Generic;

namespace Clockwork.API.Controllers
{
    [Route("api/[controller]")]
    public class LogDatabaseController : Controller
    {
        // GET api/currenttime
        [HttpGet]
        public IActionResult Get()
        {



            using (var db = new ClockworkContext())
            {

                List<CurrentTimeQuery> logs = new List<CurrentTimeQuery>();

                foreach (var log in db.CurrentTimeQueries)
                {
                    logs.Add(log);
                }

                return Ok(logs);
            }
        }
    }
}

