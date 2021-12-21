using GameLibrary;
using GameLibrary.Database;
using GameLibrary.Inventory;
using NUnit.Framework;
using Server.Authentication;
using Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Services
{
    class PlayerServicesTests
    {
        private IAuthenticator _authenticator;
        private IExpeditionService _expeditionService;
        private IEquipmentService _equipmentService;
        private ISavingService _savingService;
        private SessionManager sessionManager;
        private string username;
        private string password;
        private Session savedSession;
        private Mission mission;
        private Expedition expedition;
        private Item tempItem;

        [OneTimeSetUp]
        public void FirstTimeSetUp()
        {
            _savingService = new SavingService();
            _authenticator = new Authenticator(_savingService);
            _expeditionService = new ExpeditionService();
            _equipmentService = new EquipmentService();
            username = "testuser2";
            password = "password12@c";
            sessionManager = SessionManager.Instance;
        }

        [OneTimeTearDown]
        public void FinalTearDown()
        {
            using(var context = new DataContext())
            {
                context.Player.Remove(sessionManager.Sessions[savedSession]);
                context.RemoveRange(mission.Drops);
                context.Remove(mission);
                context.Remove(tempItem);
                context.SaveChanges();
            }
        }

        [Test, Order(1)]
        public void Register_User()
        {
            Tuple<int, Session> result = _authenticator.Register(username, password, CharacterType.MODEL_MALE_1);
            savedSession = result.Item2;
            Assert.Multiple(() =>
            {
                Assert.AreEqual(result.Item1, 1);
                Assert.NotNull(result.Item2);
            });
        }

        [Test, Order(2)]
        public void Create_Mission()
        {
            using (var context = new DataContext())
            {
                List<DropItem> gatherDrops = new List<DropItem>();
                gatherDrops.Add(new DropItem(new Item("Weapon", ItemType.WEAPON, ItemModel.DARK_BOW), 1.0f));
                gatherDrops.Add(new DropItem(new Item("Helmet", ItemType.HELMET, ItemModel.KNIGHT_HELM), 1.0f));
                gatherDrops.Add(new DropItem(new Item("Body Item", ItemType.BODY, ItemModel.GOLDEN_ARMOR), 1.0f));
                gatherDrops.Add(new DropItem(new Item("Leg Item", ItemType.LEGS, ItemModel.DEMONIC_LEGGINGS), 1.0f));
                gatherDrops.Add(new DropItem(new Item("Boots", ItemType.BOOTS, ItemModel.STEEL_BOOTS), 1.0f));
                mission = new Mission("Test Mission", "test description", MissionType.GATHER, new TimeSpan(0), new TimeSpan(1), gatherDrops);
                context.Mission.Add(mission);
                context.SaveChanges();
            }
            Assert.Pass();
        }

        [Test, Order(3)]
        public void Create_User_Expedition()
        {
            expedition = _expeditionService.GenerateExpeditionForPlayer(sessionManager.Sessions[savedSession], mission);
            Assert.NotNull(sessionManager.Sessions[savedSession].Character.Expedition);
        }

        [Test, Order(4)]
        public void Save_User_Expedition()
        {
            _savingService.Save(sessionManager.Sessions[savedSession]);
            Assert.Pass();
        }

        [Test, Order(5)]
        public void Finish_Expedition()
        {
            _expeditionService.ProcessExpedition(expedition);
            Assert.Multiple(() =>
            {
                Assert.Null(sessionManager.Sessions[savedSession].Character.Expedition);
                Assert.IsNotEmpty(sessionManager.Sessions[savedSession].Character.Items);
            });
        }

        [Test, Order(6)]
        public void Equip_Items()
        {
            Player player = sessionManager.Sessions[savedSession];
            foreach (Item item in player.Character.Items)
            {
                _equipmentService.EquipItem(savedSession, item);
            }
            Assert.Multiple(() =>
            {
                Assert.NotNull(player.Character.Equipment.Helmet);
                Assert.NotNull(player.Character.Equipment.BodyItem);
                Assert.NotNull(player.Character.Equipment.LegItem);
                Assert.NotNull(player.Character.Equipment.Boots);
                Assert.NotNull(player.Character.Equipment.Weapon);
            });
        }

        // saves null expedition and item list
        [Test, Order(7)]
        public void Save_User_Expedition_And_Items_And_Equipment()
        {
            _savingService.Save(sessionManager.Sessions[savedSession]);
            Assert.Pass();
        }

        [Test, Order(8)]
        public void Unequip_Items()
        {
            Player player = sessionManager.Sessions[savedSession];
            foreach (Item item in player.Character.Items)
            {
                _equipmentService.UnequipItem(savedSession, item.ItemType);
            }
            Assert.Multiple(() =>
            {
                Assert.Null(player.Character.Equipment.Helmet);
                Assert.Null(player.Character.Equipment.BodyItem);
                Assert.Null(player.Character.Equipment.LegItem);
                Assert.Null(player.Character.Equipment.Boots);
                Assert.Null(player.Character.Equipment.Weapon);
            });
        }

        [Test, Order(9)]
        public void Save_Empty_Equipment()
        {
            _savingService.Save(sessionManager.Sessions[savedSession]);
            Assert.Pass();
        }

    }
}
