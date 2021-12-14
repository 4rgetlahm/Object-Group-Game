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
                    using (var context = new DataContext())
                    {
                        // get player by sessionid
                        Player player = SessionManager.Instance.Sessions[session];
                        //get character by player via db
                        Character realCharacter = context.Character
                            .Include(i => i.Items)
                            .Include(e => e.Equipment)
                            .Where(c => c.CharacterID == player.Character.CharacterID).FirstOrDefault();
                        if (realCharacter == null)
                        {
                            Console.WriteLine("no char");
                            return null;
                        }

                        switch (itemUnequipRequest.ItemType)
                        {
                            case ItemType.HELMET:
                                realCharacter.Equipment.Helmet = null;
                                break;
                            case ItemType.BODY:
                                realCharacter.Equipment.BodyItem = null;
                                break;
                            case ItemType.LEGS:
                                realCharacter.Equipment.LegItem = null;
                                break;
                            case ItemType.BOOTS:
                                realCharacter.Equipment.Boots = null;
                                break;
                            case ItemType.WEAPON:
                                realCharacter.Equipment.Weapon = null;
                                break;

                        }
                        context.Entry(player).State = EntityState.Modified;
                        context.SaveChanges();
                        //context.Entry(player).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                        player.Character = realCharacter;
                        return realCharacter.Equipment;
                    }

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
