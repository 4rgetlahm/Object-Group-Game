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
                    .SingleOrDefault(p => p.PlayerID == localPlayer.PlayerID);

                Console.WriteLine(JsonConvert.SerializeObject(localPlayer));

                Console.WriteLine("\n" + JsonConvert.SerializeObject(truePlayer));

                if (truePlayer == null)
                {
                    Console.WriteLine("null");
                    return;
                }
                Console.WriteLine(truePlayer.Character.Mana);

                truePlayer.Character.Gold = localPlayer.Character.Gold;
                truePlayer.Character.VisitedLocations = localPlayer.Character.VisitedLocations;
                truePlayer.Character.Mana = localPlayer.Character.Mana;
                truePlayer.Character.Health = localPlayer.Character.Health;

                if(truePlayer.Character.Expedition != null && localPlayer.Character.Expedition == null)
                {
                    context.Remove(truePlayer.Character.Expedition);
                }

                context.SaveChanges();
                context.Entry(truePlayer).State = EntityState.Detached;
            }
            SaveEquipment(localPlayer);
            SaveExpedition(localPlayer);
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
                dbEquipment.Helmet = newEquipment.Helmet;
                dbEquipment.BodyItem = newEquipment.BodyItem;
                dbEquipment.LegItem = newEquipment.LegItem;
                dbEquipment.Boots = newEquipment.Boots;
                dbEquipment.Weapon = newEquipment.Weapon;
                context.SaveChanges();
                context.Entry(dbEquipment).State = EntityState.Detached;
            } 
        }

        public void SaveExpedition(Player localPlayer)
        {
            if(localPlayer.Character.Expedition == null)
            {
                return;
            }

            using (var context = new DataContext())
            {
                Expedition dbExpedition = context.Expedition
                    .Include(m => m.Mission)
                    .SingleOrDefault(e => e.ExpeditionID == localPlayer.Character.Expedition.ExpeditionID);
                Expedition newExpedition = localPlayer.Character.Expedition;

                dbExpedition = newExpedition;
                dbExpedition.StartTime = newExpedition.StartTime.ToUniversalTime();

                context.SaveChanges();
                context.Entry(dbExpedition).State = EntityState.Detached;
            }

        }
    }
}
