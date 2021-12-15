using GameLibrary;
using GameLibrary.Database;
using GameLibrary.Inventory;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Database
{
    class ModelCreationTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Create_Context()
        {
            var db = new DataContext();
            Assert.IsNotNull(db);
            db.Dispose();
        }

        [Test]
        public void Add_Remove_Player()
        {
            using (var db = new DataContext())
            {
                var player = new Player("Test Player");
                db.Add(player);
                db.SaveChanges();
                db.Remove(player);
                db.SaveChanges();
            }

            Assert.Pass();
        }

        [Test]
        public void Add_Remove_Character()
        {
            using (var db = new DataContext())
            {
                var character = new Character("Test Character", CharacterType.MODEL_MALE_1, 100, 200);
                db.Add(character);
                db.SaveChanges();
                db.Remove(character);
                db.SaveChanges();
            }
            Assert.Pass();
        }

        [Test]
        public void Add_Remove_Effects()
        {
            using (var db = new DataContext())
            {
                Effect effect = new Effect("coolstatus", EffectType.LIFE_STEAL);

                db.Add(effect);
                db.SaveChanges();
                db.Remove(effect);
                db.SaveChanges();
            }
            Assert.Pass();
        }

        public void Add_Remove_Item()
        {
            using (var db = new DataContext())
            {
                List<Effect> effects = new List<Effect>();
                effects.Add(new Effect("coolstatus", EffectType.AOE_LIGHTNING));
                effects.Add(new Effect("shiny", EffectType.DOUBLE_ATTACK));

                Item item = new Item("Thunderfury", ItemType.WEAPON, ItemModel.HOLY_SWORD, 999, 100, 0, effects);

                db.Add(item);
                db.SaveChanges();
                db.Remove(item);
                db.SaveChanges();
            }
            Assert.Pass();
        }

        [Test]
        public void Add_Remove_Location()
        {
            using (var db = new DataContext())
            {
                Location location = new Location("Test Location", LocationType.HOLY, 45.5641654, 65.23131776, 3);

                db.Add(location);
                db.SaveChanges();
                db.Remove(location);
                db.SaveChanges();
            }
            Assert.Pass();
        }

        [Test]
        public void Create_Location_Add_Missions()
        {
            using(var db = new DataContext())
            {
                List<DropItem> drops = new List<DropItem>();
                drops.Add(new DropItem(new Item("Sword", ItemType.WEAPON, ItemModel.HOLY_SWORD), 0.5f));

                List<DropItem> gatherDrops = new List<DropItem>();
                gatherDrops.Add(new DropItem( new Item("Bow", ItemType.WEAPON, ItemModel.DARK_BOW), 1.0f));
                gatherDrops.Add(new DropItem(new Item("White Socks", ItemType.BOOTS, ItemModel.STEEL_BOOTS), 0.5f));

                Location location = new Location("Vilnius Cathedral", LocationType.HOLY, 54.685851681232435, 25.2877395627063, 3);
                Mission mission = new Mission("Eliminate Dark Beings", "There are numerous Dark beings surrounding Cathedral.\nHelp eliminate them!", MissionType.FIGHT, new TimeSpan(0, 5, 0), new TimeSpan(0, 10, 0), drops, 100.0, 50.0);
                Mission mission2 = new Mission("Collect Holy Herbs", "Area around Cathedral have a lot of holy herbs that are used in potions.\nGather those herbs for a reward!", MissionType.GATHER, new TimeSpan(0, 2, 0), new TimeSpan(0, 5, 0), gatherDrops, 200.0, 10.0);
                location.Missions.Add(mission);
                location.Missions.Add(mission2);

                Assert.Multiple(() =>
                    {
                        Assert.NotNull(db.Location.Where(l => l.Name == location.Name).FirstOrDefault());
                        Assert.NotNull(db.Mission.Where(m => m.Title == mission.Title).FirstOrDefault());
                        Assert.NotNull(db.Mission.Where(m => m.Title == mission2.Title).FirstOrDefault());
                    }
                );
            }
        }
    }
}
