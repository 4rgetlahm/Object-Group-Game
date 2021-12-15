using GameLibrary;
using GameLibrary.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationListController : ControllerBase
    {
        [HttpGet]
        public List<Location> Get()
        {
            try
            {
                using (var context = new DataContext())
                {
                    return context.Location.Include(m => m.Missions).ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Caught exception while trying to fetch locations: " + e.Message);
            }

            return null;
        }
    }
}
