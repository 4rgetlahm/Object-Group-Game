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

        [Test]
        public void Log_String()
        {
            Logger.Log("testing logging");
            Assert.Pass();
        }

        [Test]
        public void Log_Exception()
        {
            Logger.Log(new BadSessionException("testing bad session exception"));
            Assert.Pass();
        }

        [Test]
        public void Log_Object()
        {
            Logger.Log(new Item("Test item", GameLibrary.Inventory.ItemType.BODY, GameLibrary.Inventory.ItemModel.GOLDEN_ARMOR));
            Assert.Pass();
        }
    }
}
