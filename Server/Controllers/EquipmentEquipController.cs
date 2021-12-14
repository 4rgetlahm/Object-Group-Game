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

    public class ItemEquipRequest
    {
        public string SessionID { get; set; }
        public Item Item { get; set; }
    }

    [ApiController]
    [Route("equipment/equip")]
    public class EquipmentEquipController : Controller
    {
        [HttpPost]
        public Equipment Post([FromBody] ItemEquipRequest itemEquipRequest)
        {
            Session session = SessionManager.Instance.GetRealSession(Convert.FromBase64String(itemEquipRequest.SessionID));
            if (session != null)
            {
                try
                {
                    if (itemEquipRequest.Item == null)
                    {
                        Console.WriteLine("itemequiprequestnull");
                        return null;
                    }
                    // check if player has the item
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

                        Item dbItem = context.Item.Where(i => i.ItemID == itemEquipRequest.Item.ItemID).FirstOrDefault();
                        if (dbItem == null)
                        {
                            Console.WriteLine("no item");
                            return null;
                        }

                        if (realCharacter.Items.Find(i => i.ItemID == itemEquipRequest.Item.ItemID) == null)
                        {
                            Console.WriteLine("char no item");
                            return null;
                        }
                        switch (dbItem.ItemType)
                        {
                            case ItemType.HELMET:
                                realCharacter.Equipment.Helmet = dbItem;
                                break;
                            case ItemType.BODY:
                                realCharacter.Equipment.BodyItem = dbItem;
                                break;
                            case ItemType.LEGS:
                                realCharacter.Equipment.LegItem = dbItem;
                                break;
                            case ItemType.BOOTS:
                                realCharacter.Equipment.Boots = dbItem;
                                break;
                            case ItemType.WEAPON:
                                realCharacter.Equipment.Weapon = dbItem;
                                break;
                        }
                        context.Entry(player).State = EntityState.Modified;
                        context.SaveChanges();
                        player.Character = realCharacter;
                        //context.Entry(player).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
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
