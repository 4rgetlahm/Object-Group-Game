using GameLibrary;
using GameLibrary.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Authentication;
using Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Controllers
{
    public class ExpeditionModel
    {
        public string SessionID { get; set; }
        public Mission Mission { get; set; }
    }

    [ApiController]
    [Route("player/expedition")]
    public class PlayerExpeditionController : Controller
    {
        private readonly IExpeditionService _expeditionService;

        public PlayerExpeditionController(IExpeditionService expeditionService)
        {
            _expeditionService = expeditionService;
        }

        [HttpPost]
        public Expedition Post([FromBody] ExpeditionModel body)
        {
            Session realSession = SessionManager.Instance.GetRealSession(Convert.FromBase64String(body.SessionID));
            if(realSession == null)
            {
                return null;
            }
            Player localPlayer = SessionManager.Instance.Sessions[realSession];
            return _expeditionService.GenerateExpeditionForPlayer(localPlayer, body.Mission);
        }
    }
}
