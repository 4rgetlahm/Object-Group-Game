using Microsoft.AspNetCore.Mvc;
using GameLibrary;
using GameLibrary.Database;
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
        public IEnumerable<Location> Get()
        {
            using(var context = new DataContext())
            {
                return context.Location.ToList();
            }
        }
    }
}
