using GameLibrary;
using GameLibrary.Database;
using GameLibrary.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Controllers
{

    public class ItemUnequipRequest
    {
        public string SessionID { get; set; }
        public ItemType ItemType { get; set; }
    }

    [ApiController]
    [Route("equipment/unequip")]
    public class EquipmentUnequipController : Controller
    {
        [HttpPost]
        public Equipment Post([FromBody] ItemUnequipRequest itemUnequipRequest)
        {
            Session session = SessionManager.Instance.GetRealSession(Convert.FromBase64String(itemUnequipRequest.SessionID));
            Console.WriteLine(itemUnequipRequest.ItemType);
            if (session != null)
            {
                try
                {
                    Player player = SessionManager.Instance.Sessions[session];
                    using (var context = new DataContext())
                    {
                        //context.Attach(player);
                        switch (itemUnequipRequest.ItemType)
                        {
                            case ItemType.HELMET:
                                player.Character.Equipment.Helmet = null;
                                break;
                            case ItemType.BODY:
                                player.Character.Equipment.BodyItem = null;
                                break;
                            case ItemType.LEGS:
                                player.Character.Equipment.LegItem = null;
                                break;
                            case ItemType.BOOTS:
                                player.Character.Equipment.Boots = null;
                                break;
                            case ItemType.WEAPON:
                                player.Character.Equipment.Weapon = null;
                                break;

                        }
                        context.Entry(player).State = EntityState.Modified;
                        context.SaveChanges();
                        //context.Entry(player).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                    }
                    return player.Character.Equipment;

                }
                catch (Exception e)
                {
                    Console.WriteLine("Caught exception while trying to fetch locations: " + e.Message);
                }
            }
            return null;
        }

    }
}
