using GameLibrary;
using System.Collections.Generic;

namespace Server.Services
{
    public interface IExpeditionService
    {
        void ApplyRewards(Expedition expedition, List<Item> rewards);
        int CheckIfExpeditionCompleted(Expedition expedition);
        List<Item> GenerateExpeditionRewards(Expedition expedition);
        void RemoveExpedition(Expedition expedition);
        void RemovePlayerExpedition(Player player);
        Player GetPlayerFromExpedition(Expedition expedition);
    }
}