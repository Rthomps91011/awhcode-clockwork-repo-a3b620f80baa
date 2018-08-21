using System;
using Microsoft.AspNetCore.Mvc;
using Clockwork.API.Models;

namespace Clockwork.API.Controllers
{
    [Route("api/[controller]")]
    public class CurrentTimeController : Controller
    {
        // GET api/currenttime
        [HttpGet()]
        public IActionResult Get([FromQuery]string timezone)
        {
            Console.WriteLine("our timezone was: " + timezone);
            var utcTime = DateTime.UtcNow;
            var localTime = DateTime.Now;
            if (timezone != null)
            {
                try
                {
                    TimeZoneInfo localZone = TimeZoneInfo.FindSystemTimeZoneById(timezone);
                    localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, localZone);
                }
                catch(Exception e)
                {
                    return StatusCode(400);
                }
            }
            
        var ip = this.HttpContext.Connection.RemoteIpAddress.ToString();

            var returnVal = new CurrentTimeQuery
            {
                UTCTime = utcTime,
                ClientIp = ip,
                Time = localTime,
                
             
            };

            using (var db = new ClockworkContext())
            {

                db.CurrentTimeQueries.Add(returnVal);
                var count = db.SaveChanges();
                Console.WriteLine("{0} records saved to database", count);

                Console.WriteLine();
                foreach (var CurrentTimeQuery in db.CurrentTimeQueries)
                {
                    Console.WriteLine(" - {0}", CurrentTimeQuery);

                    
                }
            }

            return Ok(returnVal);
        }
    }
}
