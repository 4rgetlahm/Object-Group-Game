using GameLibrary;
using NUnit.Framework;
using Server.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Utils
{
    public class LoggerTests
    {
        private ILogger _logger;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _logger = new Logger();
        }

        [Test]
        public void Log_String()
        {
            _logger.Log("testing logging");
            Assert.Pass();
        }

        [Test]
        public void Log_Exception()
        {
            _logger.Log(new BadSessionException("testing bad session exception"));
            Assert.Pass();
        }

        [Test]
        public void Log_Object()
        {
            _logger.Log(new Item("Test item", GameLibrary.Inventory.ItemType.BODY, GameLibrary.Inventory.ItemModel.GOLDEN_ARMOR));
            Assert.Pass();
        }
    }
}
