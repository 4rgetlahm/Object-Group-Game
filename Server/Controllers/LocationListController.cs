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

        [HttpPost]
        public IEnumerable<Location> Post(){
            using(var context = new DataContext())
            {
                Location location = new Location("Church", LocationType.LAKE, 12.45646, 67.1576, 4);
                context.Add(location);
                context.SaveChanges();
                return context.Location.ToList();
            }
        }
    }
}
