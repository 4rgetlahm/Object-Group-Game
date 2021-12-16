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
        public Tuple<int, Character> ProcessExpedition(Expedition expedition)
        {
            Expedition realExpedition = GetRealExpedition(expedition);

            int expeditionState = CheckIfExpeditionCompleted(realExpedition);
            if (expeditionState != 1)
            {
                return new Tuple<int, Character>(expeditionState, null);
            }
            List<Item> rewards = GenerateExpeditionRewards(realExpedition);
            Player player = GetPlayerFromExpedition(realExpedition);

            if (rewards != null)
            {
                player.Character.Items = new ItemService().AddItems(player, rewards);
            }

            player.Character.Gold += realExpedition.Mission.GoldReward;
            player.Character.Experience += realExpedition.Mission.ExperienceReward;

            RemovePlayerExpedition(player);

            return new Tuple<int, Character>(1, player.Character);
        }

        public Expedition GetRealExpedition(Expedition expedition)
        {
            using(var context = new DataContext())
            {
                Expedition realExpedition = context.Expedition
                     .Include(e => e.Mission)
                     .ThenInclude(m => m.Drops)
                     .ThenInclude(i => i.Item)
                     .Where(e => e.ExpeditionID == expedition.ExpeditionID)
                     .FirstOrDefault();
                return realExpedition;
            }
        }

        public List<Item> GenerateExpeditionRewards(Expedition expedition)
        {
            using (var context = new DataContext()) {
                Random random = new Random();
                List<Item> rewards = new List<Item>();
                foreach (DropItem dropItem in expedition.Mission.Drops)
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

        public Player GetPlayerFromExpedition(Expedition expedition)
        {
            //first find from sessions, otherwise go through database
            Player sessionPlayer = SessionManager.Instance.Sessions.Values.SingleOrDefault(p => p.Character.Expedition?.ExpeditionID == expedition.ExpeditionID);
            if(sessionPlayer != null)
            {
                return sessionPlayer;
            }

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

        public Expedition GenerateExpeditionForPlayer(Player player, Mission mission)
        {
            if(player == null || mission == null)
            {
                return null;
            }

            if(player.Character.Expedition != null)
            {
                return null;
            }

            using(var context = new DataContext())
            {
                Mission realMission = context.Mission.Include(d => d.Drops).SingleOrDefault(m => m.MissionID == mission.MissionID);

                if(realMission == null)
                {
                    return null;
                }

                Random random = new Random();
                Expedition expedition = new Expedition(realMission, DateTime.Now.ToUniversalTime(), new TimeSpan(LongRandom(realMission.MinDuration.Ticks, realMission.MaxDuration.Ticks, random)));
                player.Character.Expedition = expedition;

                context.Expedition.Add(expedition);
                context.SaveChanges();

                return expedition;
            }
        }

        private long LongRandom(long min, long max, Random rand)
        {
            byte[] buf = new byte[8];
            rand.NextBytes(buf);
            long longRand = BitConverter.ToInt64(buf, 0);

            return (Math.Abs(longRand % (max - min)) + min);
        }

    }
}
