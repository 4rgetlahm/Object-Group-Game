using GameLibrary;
using Server.Authentication;
using System;

namespace Server.Services
{
    public interface IItemService
    {
        Item GetRealItem(Item item);
        void GiveItem(Character character, Item item);
        void GiveItem(Session session, Item item);
    }
}