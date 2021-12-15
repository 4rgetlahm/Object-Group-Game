using GameLibrary;
using GameLibrary.Database;
using GameLibrary.Inventory;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Server.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Services
{
    public class EquipmentService : IEquipmentService
    {
        public void EquipItem(Session session, Item item)
        {
            Session realSession = SessionManager.Instance.GetRealSession(session);
            if (realSession == null)
            {
                return;
            }
            EquipItem(SessionManager.Instance.Sessions[realSession].Character, item);
        }

        public void EquipItem(byte[] sessionID, Item item)
        {
            Session realSession = SessionManager.Instance.GetRealSession(sessionID);
            if (realSession == null)
            {
                return;
            }
            EquipItem(SessionManager.Instance.Sessions[realSession].Character, item);
        }

        public void EquipItem(Character character, Item item)
        {
            if (character == null)
            {
                return;
            }

            Console.WriteLine(character.Name);
            if (item == null)
            {
                return;
            }

            IItemService itemService = new ItemService();
            Item dbItem = itemService.GetRealItem(item);
            Console.WriteLine(dbItem.Name);
            if (dbItem == null)
            {
                return;
            }
            // check if player has the item
            using (var context = new DataContext())
            {
                Console.WriteLine("checkitems");
                if (character.Items.Find(i => i.ItemID == dbItem.ItemID) == null)
                {
                    return;
                }

                switch (dbItem.ItemType)
                {
                    case ItemType.HELMET:
                        character.Equipment.Helmet = dbItem;
                        break;
                    case ItemType.BODY:
                        character.Equipment.BodyItem = dbItem;
                        break;
                    case ItemType.LEGS:
                        character.Equipment.LegItem = dbItem;
                        break;
                    case ItemType.BOOTS:
                        character.Equipment.Boots = dbItem;
                        break;
                    case ItemType.WEAPON:
                        character.Equipment.Weapon = dbItem;
                        break;
                }
                context.Entry(character).State = EntityState.Modified;
                context.SaveChanges();
                Character realCharacter = context.Character
                            .Include(i => i.Items)
                            .Include(e => e.Equipment)
                            .Where(c => c.CharacterID == character.CharacterID).FirstOrDefault();

            }
        }
    }
}
