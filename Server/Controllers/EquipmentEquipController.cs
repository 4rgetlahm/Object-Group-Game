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
                    if(itemEquipRequest.Item == null)
                    {
                        return null;
                    }
                    // check if player has the item
                    if(SessionManager.Instance.Sessions[session].Character.Items.Find(i => i.ItemID == itemEquipRequest.Item.ItemID) == null)
                    {
                        return null;
                    }
                    Player player = SessionManager.Instance.Sessions[session];
                    using (var context = new DataContext())
                    {
                        Item dbItem = context.Item.Where(i => i.ItemID == itemEquipRequest.Item.ItemID).FirstOrDefault();
                        if(dbItem == null)
                        {
                            return null;
                        }
                        switch (itemEquipRequest.Item.ItemType)
                        {
                            case ItemType.HELMET:
                                player.Character.Equipment.Helmet = itemEquipRequest.Item;
                                break;
                            case ItemType.BODY:
                                player.Character.Equipment.BodyItem = itemEquipRequest.Item;
                                break;
                            case ItemType.LEGS:
                                player.Character.Equipment.LegItem = itemEquipRequest.Item;
                                break;
                            case ItemType.BOOTS:
                                player.Character.Equipment.Boots = itemEquipRequest.Item;
                                break;
                            case ItemType.WEAPON:
                                player.Character.Equipment.Weapon = itemEquipRequest.Item;
                                break;
                        }
                        context.Entry(player).State = EntityState.Modified;
                        context.SaveChanges();
                        //context.Entry(player).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                        return SessionManager.Instance.Sessions[session].Character.Equipment;
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
