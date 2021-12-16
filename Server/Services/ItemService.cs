using GameLibrary;
using GameLibrary.Database;
using Server.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Services
{
    public class ItemService : IItemService
    {
        public ItemService()
        {
        }

        public Item GetRealItem(Item item)
        {
            using(var context = new DataContext())
            {
                return context.Item.Find(item.ItemID);
            }
        }

        public void GiveItem(Session session, Item item)
        {
            Session realSession = SessionManager.Instance.GetRealSession(session);
            if (realSession == null)
            {
                return;
            }
            GiveItem(SessionManager.Instance.Sessions[realSession], item);
        }

        public void GiveItems(Player player, List<Item> items)
        {
            if(player == null || items == null)
            {
                return;
            }
            foreach(Item item in items)
            {
                GiveItem(player, item);
            }
        }

        public void GiveItem(Player player, Item item)
        {
            if (item == null)
            {
                return;
            }
            if(!CheckIfPlayerHasItem(player, item))
            {
                player.Character.Items.Add(item);
            }
        }

        public List<Item> AddItems(Player player, List<Item> items)
        {
            GiveItems(player, items);
            return player.Character.Items;
        }

        public bool CheckIfPlayerHasItem(Player player, Item item)
        {
            if(item == null)
            {
                return false;
            }

            return player.Character.Items.Find(i => i.ItemID == item.ItemID) != null;
        }
    }
}
