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
            GiveItem(SessionManager.Instance.Sessions[realSession].Character, item);
        }

        public void GiveItems(Character character, List<Item> items)
        {
            foreach(Item item in items)
            {
                GiveItem(character, item);
            }
        }

        public void GiveItem(Character character, Item item)
        {
            if (item == null)
            {
                return;
            }
            using (var context = new DataContext())
            {
                Item realItem = context.Item.Find(item.ItemID);
                List<Item> newItemList = new List<Item>(character.Items);
                newItemList.Add(realItem);
                character.Items = newItemList;
                context.SaveChanges();
            }
        }
    }
}
