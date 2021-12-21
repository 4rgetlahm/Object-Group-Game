using Microsoft.AspNetCore.Mvc;
using GameLibrary;
using GameLibrary.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Authentication;
using Server.Logging;

namespace Server.Controllers
{
    [ApiController]
    [Route("player/location/list")]
    public class PlayerLocationListContoller : ControllerBase
    {
        [HttpGet]
        public List<Location> Get(string sessionid)
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
                    Logger.Log(e);
                }
            }
            return null;
        }
    }
}
