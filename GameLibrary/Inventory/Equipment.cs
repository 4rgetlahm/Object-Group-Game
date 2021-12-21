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
        private Item _helmet;
        public Item Helmet {
            get
            {
                return _helmet;
            }
            set
            {
                _helmet = value;
                OnEquipmentChange(new EquipmentEventArgs(this));
            }
        }
        private Item _bodyItem;
        public Item BodyItem {
            get
            {
                return _bodyItem;
            }
            set
            {
                _bodyItem = value;
                OnEquipmentChange(new EquipmentEventArgs(this));
            }
        }
        private Item _legItem;
        public Item LegItem {
            get
            {
                return _legItem;
            }
            set
            {
                _legItem = value;
                OnEquipmentChange(new EquipmentEventArgs(this));
            }
        }
        private Item _boots;
        public Item Boots {
            get
            {
                return _boots;
            }
            set
            {
                _boots = value;
                OnEquipmentChange(new EquipmentEventArgs(this));
            }
        }
        private Item _weapon;
        public Item Weapon {
            get
            {
                return _weapon;
            }
            set
            {
                _weapon = value;
                OnEquipmentChange(new EquipmentEventArgs(this));
            }
        }
        private Item _offHandWeapon;
        public Item OffHandWeapon {
            get
            {
                return _offHandWeapon;
            }
            set
            {
                _offHandWeapon = value;
                OnEquipmentChange(new EquipmentEventArgs(this));
            }
        }

        public delegate void EquipmentChangeHandler(EquipmentEventArgs args);
        public event EquipmentChangeHandler EquipmentChangeEvent;


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

        protected virtual void OnEquipmentChange(EquipmentEventArgs args)
        {
            if(EquipmentChangeEvent != null)
            {
                EquipmentChangeEvent(args);
            }

        }

    }
}
