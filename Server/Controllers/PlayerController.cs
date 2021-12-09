using GameLibrary;
using GameLibrary.Database;
using GameLibrary.Inventory;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Server.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [ApiController]
    [Route("session/player")]
    public class PlayerSessionController : Controller
    {
        [HttpPost]
        public Player Post([FromBody] Session body)
        {
            if(body == null){
                return null;
            }
            Player player = SessionManager.Instance.Sessions[SessionManager.Instance.GetRealSession(body.SessionID)];
            return player;
        }
    }
}
