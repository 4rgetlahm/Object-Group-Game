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
    public class ExpeditionService : IExpeditionService
    {
        public List<Item> GenerateExpeditionRewards(Expedition expedition)
        {
            using (var context = new DataContext()) {
                Expedition realExpedition = context.Expedition
                    .Include(e => e.Mission)
                    .ThenInclude(m => m.Drops)
                    .ThenInclude(i => i.Item)
                    .Where(e => e.ExpeditionID == expedition.ExpeditionID)
                    .FirstOrDefault();
                Random random = new Random();
                List<Item> rewards = new List<Item>();
                foreach (DropItem dropItem in realExpedition.Mission.Drops)
                {
                    if (random.NextDouble() <= dropItem.DropRate)
                    {
                        rewards.Add(dropItem.Item);
                    }
                }
                foreach(Item item in rewards){
                    Console.WriteLine(item.Name);
                }
                return rewards;
            }
        }

        public int CheckIfExpeditionCompleted(Expedition expedition)
        {
            using (var context = new DataContext())
            {
                Expedition realExpedition = context.Expedition.Include(e => e.Mission).SingleOrDefault(e => e.ExpeditionID == expedition.ExpeditionID);
                if (realExpedition == null)
                {
                    return -1;
                }
                Console.WriteLine((realExpedition.StartTime + realExpedition.Duration).ToUniversalTime());
                Console.WriteLine(DateTime.UtcNow);
                if (DateTime.Compare((realExpedition.StartTime + realExpedition.Duration).ToUniversalTime(),DateTime.UtcNow) < 0)
                {
                    return 1;
                }
                return 0;
            }
        }

        public void ApplyRewards(Expedition expedition, List<Item> rewards)
        {
            using (var context = new DataContext())
            {
                /*Character expeditionCharacter = context.Character
                    .Include(e => e.Expedition)
                    .Include(i => i.Items)
                    .Where(c => c.Expedition.ExpeditionID == expedition.ExpeditionID)
                    .FirstOrDefault();*/
                Expedition realExpedition = context.Expedition.Include(e => e.Mission).Where(e => e.ExpeditionID == expedition.ExpeditionID).FirstOrDefault();
                var expeditionCharacter = context.Character.Include(e => e.Expedition).Include(i => i.Items).SingleOrDefault(c => c.Expedition.ExpeditionID == expedition.ExpeditionID);
                if (expeditionCharacter == null || realExpedition == null)
                {
                    return;
                }
                context.Entry(expeditionCharacter).State = EntityState.Detached;
                context.Character.Attach(expeditionCharacter);
                expeditionCharacter.Gold += realExpedition.Mission.GoldReward;
                expeditionCharacter.Experience += realExpedition.Mission.ExperienceReward;
                foreach(Item item in rewards){
                    if(expeditionCharacter.Items.Where(i => i.ItemID == item.ItemID).FirstOrDefault() == null){
                        Console.WriteLine(item.Name);
                        expeditionCharacter.Items.Add(item);
                        context.Entry(item).State = EntityState.Modified;
                    }
                }
                
                //context.Entry(expeditionCharacter).State = EntityState.Modified;
                //context.Entry(expeditionCharacter.Items.First()).State = EntityState.Added;

                context.SaveChanges();
            }
        }

        public Player GetPlayerFromExpedition(Expedition expedition)
        {
            using(var context = new DataContext())
            {
                Player dbPlayer = context.Player.SingleOrDefault(p => p.Character.Expedition.ExpeditionID == expedition.ExpeditionID);
                if(dbPlayer == null)
                {
                    return null;
                }
                return SessionManager.Instance.GetRealPlayer(dbPlayer);
            }
        }

        public void RemoveExpedition(Expedition expedition)
        {
            Player player = GetPlayerFromExpedition(expedition);
            using (var context = new DataContext())
            {
                //Expedition realExpedition = context.Expedition.SingleOrDefault(e => e.ExpeditionID == expedition.ExpeditionID);
                Player realPlayer = context.Player
                    .Include(p => p.Character)
                    .ThenInclude(e => e.Expedition)
                    .SingleOrDefault(p => p.Character.Expedition.ExpeditionID == expedition.ExpeditionID);
                if (realPlayer != null)
                {
                    context.Remove(realPlayer.Character.Expedition);
                    //context.Remove(realExpedition);
                    context.SaveChanges();
                }
            }
        }

        public void RemovePlayerExpedition(Player player)
        {
            if(player != null)
            {
                player.Character.Expedition = null;
            }
        }

        /*
        public Character RemoveExpedition(Expedition expedition)
        {
            using (var context = new DataContext())
            {
                Character expeditionCharacter = context.Character
                    .Include(e => e.Expedition)
                    .Include(e => e.Equipment)
                    .Include(l => l.VisitedLocations)
                    .Include(i => i.Items)
                    .Where(c => c.Expedition.ExpeditionID == expedition.ExpeditionID)
                    .FirstOrDefault();

                if(expeditionCharacter == null){
                    return null;
                }
                context.Expedition.Remove(expeditionCharacter.Expedition);
                context.SaveChanges();
                return expeditionCharacter;
            }
        }*/
    }
}
