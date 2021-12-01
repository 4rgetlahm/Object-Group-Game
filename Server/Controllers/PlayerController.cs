using GameLibrary;
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
            foreach(var session in SessionManager.Instance.Sessions){
                if(session.Key.SessionID.SequenceEqual(body.SessionID)){
                    Player player = SessionManager.Instance.Sessions[session.Key];
                    return player;
                }
            }
            return null;
        }
    }
}
