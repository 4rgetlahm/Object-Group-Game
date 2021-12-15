using GameLibrary;
using GameLibrary.Database;
using GameLibrary.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [ApiController]
    [Route("player/expedition/reward")]
    public class ExpeditionRewardController : ControllerBase
    {
        private readonly IExpeditionService _expeditionService;
        public ExpeditionRewardController(IExpeditionService expeditionService)
        {
            _expeditionService = expeditionService;
        }

        [HttpPost]
        public Tuple<int,Character> Post([FromBody] Expedition expedition)
        {
            int expeditionState = _expeditionService.CheckIfExpeditionCompleted(expedition);
            if(expeditionState != 1)
            {
                return new Tuple<int, Character>(expeditionState, null);
            }
            List<Item> rewards = _expeditionService.GenerateExpeditionRewards(expedition);
            Character applyCharacter = _expeditionService.ApplyRewards(expedition, rewards);
            if (applyCharacter == null)
            {
                return new Tuple<int, Character>(-2, null);
            }
            Character finalCharacter = _expeditionService.RemoveExpedition(expedition);
            return new Tuple<int, Character>(1, finalCharacter);
        }
    }
}
