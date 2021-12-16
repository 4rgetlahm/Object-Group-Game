using GameLibrary;
using GameLibrary.Database;
using GameLibrary.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Authentication;
using Server.Services;
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
        private readonly IEquipmentService _equipmentService;

        public EquipmentUnequipController(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        [HttpPost]
        public Equipment Post([FromBody] ItemUnequipRequest itemUnequipRequest)
        {
            Session session = SessionManager.Instance.GetRealSession(Convert.FromBase64String(itemUnequipRequest.SessionID));
            if (session != null)
            {
                _equipmentService.UnequipItem(Convert.FromBase64String(itemUnequipRequest.SessionID), itemUnequipRequest.ItemType);
            }
            return SessionManager.Instance.Sessions[session].Character.Equipment;
        }

    }
}
