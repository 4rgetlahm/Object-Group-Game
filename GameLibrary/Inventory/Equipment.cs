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


        public int GetStrength()
        {
            int sum = 0;
            if(Helmet != null)
            {
                sum += Helmet.Strength;
            }
            if(BodyItem != null)
            {
                sum += BodyItem.Strength;
            }
            if(LegItem != null)
            {
                sum += LegItem.Strength;
            }
            if(Boots != null)
            {
                sum += Boots.Strength;
            }
            if(Weapon != null)
            {
                sum += Weapon.Strength;
            }
            if(OffHandWeapon != null)
            {
                sum += OffHandWeapon.Strength;
            }
            return sum;
        }

        public int GetDexterity()
        {
            int sum = 0;
            if (Helmet != null)
            {
                sum += Helmet.Dexterity;
            }
            if (BodyItem != null)
            {
                sum += BodyItem.Dexterity;
            }
            if (LegItem != null)
            {
                sum += LegItem.Dexterity;
            }
            if (Boots != null)
            {
                sum += Boots.Dexterity;
            }
            if (Weapon != null)
            {
                sum += Weapon.Dexterity;
            }
            if (OffHandWeapon != null)
            {
                sum += OffHandWeapon.Dexterity;
            }
            return sum;
        }

        public int GetIntelligence()
        {
            int sum = 0;
            if (Helmet != null)
            {
                sum += Helmet.Intelligence;
            }
            if (BodyItem != null)
            {
                sum += BodyItem.Intelligence;
            }
            if (LegItem != null)
            {
                sum += LegItem.Intelligence;
            }
            if (Boots != null)
            {
                sum += Boots.Intelligence;
            }
            if (Weapon != null)
            {
                sum += Weapon.Intelligence;
            }
            if (OffHandWeapon != null)
            {
                sum += OffHandWeapon.Intelligence;
            }
            return sum;
        }

    }
}
