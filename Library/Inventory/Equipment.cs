using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Inventory
{
    public class Equipment
    {
        [Key]
        public int EquipmentID { get; set; }
        public Item Helmet { get; set; }
        public Item BodyItem { get; set; }
        public Item LegItem { get; set; }
        public Item Boots { get; set; }
        public Item Weapon { get; set; }
        public Item OffHandWeapon { get; set; }
    }
}
