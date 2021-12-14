using GameLibrary;
using System.Collections.Generic;

namespace Server.Services
{
    public interface IExpeditionService
    {
        Character ApplyRewards(Expedition expedition, List<Item> rewards);
        int CheckIfExpeditionCompleted(Expedition expedition);
        List<Item> GenerateExpeditionRewards(Expedition expedition);
        Character RemoveExpedition(Expedition expedition);
    }
}