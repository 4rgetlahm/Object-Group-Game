using Microsoft.AspNetCore.Mvc;
using GameLibrary;
using GameLibrary.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Authentication;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationListController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Location> Get(string sessionid)
        {
            Session session = SessionManager.Instance.GetRealSession(Convert.FromBase64String(sessionid));
            if (session != null)
            {
                try
                {
                    using (var context = new DataContext())
                    {
                        return context.Location.ToList().FindAll(loc => !SessionManager.Instance.Sessions[session].Character.VisitedLocations.Contains(loc));
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Caught exception while trying to fetch locations: " + e.Message);
                }
            }
            return null;
        }
    }
}
