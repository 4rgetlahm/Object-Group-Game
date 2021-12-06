using GameLibrary;
using GameLibrary.Database;
using GameLibrary.Inventory;
using NUnit.Framework;
using System.Collections.Generic;

namespace Tests.Database
{
    class DatabaseTests
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

                Item item = new Item("Thunderfury", ItemType.WEAPON, 999, 100, 0, effects);

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
                //db.Remove(location);
                //db.SaveChanges();
            }
            Assert.Pass();
        }
    }
}
