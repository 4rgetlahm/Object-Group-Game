using GameLibrary;
using GameLibrary.Database;
using GameLibrary.Inventory;
using Microsoft.AspNetCore.Mvc;
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
                    switch (itemUnequipRequest.ItemType)
                    {
                        case ItemType.HELMET:
                            SessionManager.Instance.Sessions[session].Character.Equipment.Helmet = null;
                            break;
                        case ItemType.BODY:
                            SessionManager.Instance.Sessions[session].Character.Equipment.BodyItem = null;
                            break;
                        case ItemType.LEGS:
                            SessionManager.Instance.Sessions[session].Character.Equipment.LegItem = null;
                            break;
                        case ItemType.BOOTS:
                            SessionManager.Instance.Sessions[session].Character.Equipment.Boots = null;
                            break;
                        case ItemType.WEAPON:
                            SessionManager.Instance.Sessions[session].Character.Equipment.Weapon = null;
                            break;

                    }
                    return SessionManager.Instance.Sessions[session].Character.Equipment;

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
