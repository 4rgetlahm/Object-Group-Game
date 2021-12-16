using GameLibrary;
using System;
using System.Collections.Generic;

namespace Server.Services
{
    public interface IExpeditionService
    {
        int CheckIfExpeditionCompleted(Expedition expedition);
        List<Item> GenerateExpeditionRewards(Expedition expedition);
        void RemoveExpedition(Expedition expedition);
        void RemovePlayerExpedition(Player player);
        Player GetPlayerFromExpedition(Expedition expedition);
        Tuple<int, Character> ProcessExpedition(Expedition expedition);
        Expedition GenerateExpeditionForPlayer(Player player, Mission mission);
    }
}