using NUnit.Framework;
using object_group_game;
using object_group_game.Database;
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
                var character = new Character("Test Character", 50.0, 999999.0, 80.0, 3333333.4);
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
                Effect effect = new Effect("coolstatus", "cooler name", 5);

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
                effects.Add(new Effect("coolstatus", "cooler name", 5));
                effects.Add(new Effect("shiny", "shiny name", 0));

                Item item = new Item("Thunderfury", 999, 100, 0, effects);

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
    }
}
