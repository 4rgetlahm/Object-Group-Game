using GameLibrary.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary
{
    [Table("Characters")]
    public class Character
    {
        [Key]
        public int CharacterID { get; set; }
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnCharacterUpdate(new CharacterEventArgs(this));
            }
        }
        private double _health;
        public double Health
        {
            get
            {
                return _health;
            }
            set
            {
                _health = value;
                OnCharacterUpdate(new CharacterEventArgs(this));
            }
        }
        private double _experience;
        public double Experience
        {
            get
            {
                return _experience;
            }
            set
            {
                _experience = value;
                OnCharacterUpdate(new CharacterEventArgs(this));
            }
        }
        private double _mana;
        public double Mana
        {
            get
            {
                return _mana;
            }
            set
            {
                _mana = value;
                OnCharacterUpdate(new CharacterEventArgs(this));
            }
        }
        private double _gold;
        public double Gold { 
            get 
            {
                return _gold;    
            }
            set
            {
                _gold = value;
                OnCharacterUpdate(new CharacterEventArgs(this));
            }
        }

        private CharacterType _characterType;
        public CharacterType CharacterType
        {
            get
            {
                return _characterType;
            }
            set
            {
                _characterType = value;
                OnCharacterTypeChange(new CharacterEventArgs(this));
                //OnCharacterUpdate(new CharacterEventArgs(this));
            }
        }

        public List<Item> Items { get; set; }

        private Equipment _equipment;
        public Equipment Equipment { 
            get {
                return _equipment;
            }
            set {
                if (_equipment != null)
                {
                    _equipment.EquipmentChangeEvent -= this.OnEquipmentChange;
                }
                _equipment = value;
                _equipment.EquipmentChangeEvent += this.OnEquipmentChange;
                OnCharacterEquipmentChange(new CharacterEventArgs(this));
            }
        }

        private Expedition _expedition;
        public Expedition Expedition
        {
            get
            {
                return _expedition;
            }
            set
            {
                _expedition = value;
                OnCharacterUpdate(new CharacterEventArgs(this));
            }
        }
        public List<Location> VisitedLocations { get; set; }

        public delegate void CharacterUpdateEventHandler(CharacterEventArgs args);
        public event CharacterUpdateEventHandler CharacterUpdateEvent;

        public delegate void CharacterTypeChangedHandler(CharacterEventArgs args);
        public event CharacterTypeChangedHandler CharacterTypeChangeEvent;

        public delegate void CharacterEquipmentChangedHandler(CharacterEventArgs args);
        public event CharacterTypeChangedHandler CharacterEquipmentChangeEvent;


        public void OnEquipmentChange(EquipmentEventArgs args)
        {
            OnCharacterEquipmentChange(new CharacterEventArgs(this));
        }

        protected Character()
        {
            //this.Equipment.EquipmentChangeEvent += this.OnEquipmentChange;
        }

        public Character(string name, CharacterType characterType, double health, double mana, double experience, 
            double gold, List<Item> itemList, List<Location> visitedLocations, Equipment equipment) : base()
        {
            this.Name = name;
            this.CharacterType = characterType;
            this.Health = health;
            this.Mana = mana;
            this.Experience = experience;
            this.Gold = gold;
            this.Items = itemList;
            this.VisitedLocations = visitedLocations;
            this.Equipment = equipment;
        }

        public Character(string name, CharacterType characterType, double health = 100.0, double mana = 100.0) : base()
        {
            this.Name = name;
            this.CharacterType = characterType;
            this.Equipment = new Equipment();
            this.VisitedLocations = new List<Location>();
            this.Items = new List<Item>();
            this.Health = health;
            this.Mana = mana;
        }

        public List<string> getItemNames()
        {
            List<string> names = new List<string>();
            foreach(Item item in this.Items)
            {
                names.Add(item.Name);
            }
            return names;
        }

        public void OnCharacterUpdate(CharacterEventArgs args)
        {
            if (CharacterUpdateEvent != null)
            {
                CharacterUpdateEvent(args);
            }
        }

        public void OnCharacterTypeChange(CharacterEventArgs args) { 
            if(CharacterTypeChangeEvent != null)
            {
                CharacterTypeChangeEvent(args);
                OnCharacterUpdate(args);
            }
        }

        public void OnCharacterEquipmentChange(CharacterEventArgs args)
        {
            if(CharacterEquipmentChangeEvent != null)
            {
                CharacterEquipmentChangeEvent(args);
                OnCharacterUpdate(args);
            }
        }

    }
}
