using GameLibrary;
using GameLibrary.Database;
using GameLibrary.Inventory;
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
    [ApiController]
    [Route("player/expedition/reward")]
    public class ExpeditionRewardController : ControllerBase
    {
        private readonly IExpeditionService _expeditionService;
        private readonly IItemService _itemService;
        public ExpeditionRewardController(IExpeditionService expeditionService, IItemService itemService)
        {
            _expeditionService = expeditionService;
            _itemService = itemService;
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
            Player player = _expeditionService.GetPlayerFromExpedition(expedition);
            Console.WriteLine(player.Character.Items);
            if (rewards != null)
            {
                _itemService.GiveItems(player, rewards);
            }
            _expeditionService.RemovePlayerExpedition(player);

            return new Tuple<int, Character>(1, player.Character);
        }
    }
}
