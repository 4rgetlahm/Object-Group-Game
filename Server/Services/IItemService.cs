using GameLibrary;
using Server.Authentication;
using System;
using System.Collections.Generic;

namespace Server.Services
{
    public interface IItemService
    {
        Item GetRealItem(Item item);
        void GiveItem(Player player, Item item);
        void GiveItems(Player player, List<Item> items);
        void GiveItem(Session session, Item item);
        bool CheckIfPlayerHasItem(Player player, Item item);
    }
}