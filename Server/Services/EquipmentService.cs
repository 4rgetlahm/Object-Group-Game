using GameLibrary;
using GameLibrary.Database;
using GameLibrary.Inventory;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Server.Authentication;
using Server.Logging;
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
            EquipItem(session.SessionID, item);
        }

        public void EquipItem(byte[] sessionID, Item item)
        {
            Session realSession = SessionManager.Instance.GetRealSession(sessionID);
            if (realSession == null)
            {
                return;
            }
            EquipItem(SessionManager.Instance.Sessions[realSession], item);
        }

        public void EquipItem(Player player, Item item)
        {
            if (player == null)
            {
                return;
            }
            if (player.Character == null)
            {
                return;
            }
            if (item == null)
            {
                return;
            }

            IItemService itemService = new ItemService();
            Item dbItem = itemService.GetRealItem(item);

            if (dbItem == null)
            {
                return;
            }

            if (!itemService.CheckIfPlayerHasItem(player, dbItem))
            {
                return;
            }

            switch (dbItem.ItemType)
            {
                case ItemType.HELMET:
                    player.Character.Equipment.Helmet = dbItem;
                    break;
                case ItemType.BODY:
                    player.Character.Equipment.BodyItem = dbItem;
                    break;
                case ItemType.LEGS:
                    player.Character.Equipment.LegItem = dbItem;
                    break;
                case ItemType.BOOTS:
                    player.Character.Equipment.Boots = dbItem;
                    break;
                case ItemType.WEAPON:
                    player.Character.Equipment.Weapon = dbItem;
                    break;
            }
        }
        public void UnequipItem(Session session, ItemType itemType)
        {
            UnequipItem(session.SessionID, itemType);
        }

        public void UnequipItem(byte[] sessionID, ItemType itemType)
        {
            Session realSession = SessionManager.Instance.GetRealSession(sessionID);
            if (realSession == null)
            {
                return;
            }
            UnequipItem(SessionManager.Instance.Sessions[realSession], itemType);
        }

        public void UnequipItem(Player player, ItemType itemType)
        {
            switch (itemType)
            {
                case ItemType.HELMET:
                    player.Character.Equipment.Helmet = null;
                    break;
                case ItemType.BODY:
                    player.Character.Equipment.BodyItem = null;
                    break;
                case ItemType.LEGS:
                    player.Character.Equipment.LegItem = null;
                    break;
                case ItemType.BOOTS:
                    player.Character.Equipment.Boots = null;
                    break;
                case ItemType.WEAPON:
                    player.Character.Equipment.Weapon = null;
                    break;
            }
            Logger.Log("AFTER: " + JsonConvert.SerializeObject(player));
        }
    }
}
