using GameLibrary;
using GameLibrary.Database;
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
                var character = new Character("Test Character", 50);
                db.Add(character);
                db.SaveChanges();
                db.Remove(character);
                db.SaveChanges();
            }
            Assert.Pass();
        }

        public void Add_Remove_Item()
        {
            using (var db = new DataContext())
            {
                Item item = new Item("Thunderfury", 999, 100, 0, 5, 420);

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
