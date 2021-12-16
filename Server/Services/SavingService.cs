using GameLibrary;
using GameLibrary.Database;
using GameLibrary.Inventory;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Services
{
    public class SavingService : ISavingService
    {
        public void Save(Player localPlayer)
        {
            using(var context = new DataContext())
            {
                //context.Entry(player).State = EntityState.Detached;
                Player truePlayer = 
                    context.Player.Include(c => c.Character)
                        .ThenInclude(e => e.Expedition)
                    .Include(c => c.Character)
                        .ThenInclude(e => e.Items)
                    .SingleOrDefault(p => p.PlayerID == localPlayer.PlayerID);

                if (truePlayer == null)
                {
                    return;
                }

                truePlayer.Character.Gold = localPlayer.Character.Gold;
                truePlayer.Character.VisitedLocations = localPlayer.Character.VisitedLocations;
                truePlayer.Character.Mana = localPlayer.Character.Mana;
                truePlayer.Character.Health = localPlayer.Character.Health;
                truePlayer.Character.Experience = localPlayer.Character.Experience;

                truePlayer.Character.Items.Clear();
                foreach(Item item in GetTrackedItems(context, localPlayer.Character.Items))
                {
                    truePlayer.Character.Items.Add(item);
                }

                SaveEquipment(localPlayer);
                SaveExpedition(truePlayer, localPlayer);

                try
                {
                    context.SaveChanges();
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message + " " + e.StackTrace);
                }
                context.Entry(truePlayer).State = EntityState.Detached;
            }
        }

        public List<Item> GetTrackedItems(DataContext context, List<Item> items)
        {

            List<Item> trackedItems = new List<Item>();
            foreach (Item item in items)
            {
                trackedItems.Add(context.Item.Find(item.ItemID));
            }
            return trackedItems;
        }

        public void SaveEquipment(Player localPlayer)
        {
            using(var context = new DataContext())
            {
                Equipment dbEquipment = context.Equipment
                    .Include(h => h.Helmet)
                    .Include(b => b.BodyItem)
                    .Include(l => l.LegItem)
                    .Include(b => b.Boots)
                    .Include(w => w.Weapon)
                    .SingleOrDefault(e => e.EquipmentID == localPlayer.Character.Equipment.EquipmentID);

                Equipment newEquipment = localPlayer.Character.Equipment;

                if (dbEquipment.Helmet != newEquipment.Helmet)
                {
                    if (newEquipment.Helmet != null)
                    {
                        var currentItem = context.Item.Find(newEquipment.Helmet.ItemID);
                        dbEquipment.Helmet = currentItem;
                    }
                    else
                    {
                        dbEquipment.Helmet = null;
                    }
                }

                if (dbEquipment.BodyItem != newEquipment.BodyItem)
                {
                    if (newEquipment.BodyItem != null)
                    {
                        var currentItem = context.Item.Find(newEquipment.BodyItem.ItemID);
                        dbEquipment.BodyItem = currentItem;
                    }
                    else
                    {
                        dbEquipment.BodyItem = null;
                    }
                }

                if (dbEquipment.LegItem != newEquipment.LegItem)
                {
                    if (newEquipment.LegItem != null)
                    {
                        var currentItem = context.Item.Find(newEquipment.LegItem.ItemID);
                        dbEquipment.LegItem = currentItem;
                    }
                    else
                    {
                        dbEquipment.LegItem = null;
                    }
                }

                if (dbEquipment.Boots != newEquipment.Boots)
                {
                    if (newEquipment.Boots != null)
                    {
                        var currentItem = context.Item.Find(newEquipment.Boots.ItemID);
                        dbEquipment.Boots = currentItem;
                    }
                    else
                    {
                        dbEquipment.Boots = null;
                    }
                }

                if (dbEquipment.Weapon != newEquipment.Weapon)
                {
                    if(newEquipment.Weapon != null)
                    {
                        var currentItem = context.Item.Find(newEquipment.Weapon.ItemID);
                        dbEquipment.Weapon = currentItem;
                    }
                    else
                    {
                        dbEquipment.Weapon = null;
                    }
                }

                try
                {
                    context.SaveChanges();
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message + " " + e.StackTrace);
                }
            } 
        }

        private void ChangeEquipmentItem(ref Item item, Item newItem)
        {
            if (item != newItem)
            {
                if (newItem != null)
                {
                    using (var context = new DataContext())
                    {
                        var currentItem = context.Item.Find(item.ItemID);
                        item = currentItem;
                    }
                }
                else
                {
                    item = null;
                }
            }
        }

        public void SaveExpedition(Player dbPlayer, Player localPlayer)
        {
            using (var context = new DataContext())
            {
                if (dbPlayer.Character.Expedition != null && localPlayer.Character.Expedition == null)
                {
                    Console.WriteLine(dbPlayer.Character.Expedition.ExpeditionID);
                    context.Expedition.Remove(dbPlayer.Character.Expedition);
                    dbPlayer.Character.Expedition = null;
                }
                else if (dbPlayer.Character.Expedition?.ExpeditionID != localPlayer.Character.Expedition?.ExpeditionID)
                {
                    dbPlayer.Character.Expedition = localPlayer.Character.Expedition;
                }
            }

        }
    }
}
