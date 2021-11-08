using GameLibrary.Database;
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
        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<Location> VisitedLocations { get; set; }

        public delegate void CharacterUpdateEventHandler(CharacterEventArgs args);
        public event CharacterUpdateEventHandler CharacterUpdateEvent;

        protected Character()
        {

        }

        public Character(string name, double health = 100.0, double experience = 0.0, double mana = 100.0, double gold = 0.0)
        {
            this.Name = name;
            this.Health = health;
            this.Experience = experience;
            this.Mana = mana;
            this.Gold = gold;
        }

        public void SetStats(double health = 0, double experience = 0, double mana = 0, double gold = 0)
        {
            this.Health = health;
            this.Experience = experience;
            this.Mana = mana;
            this.Gold = gold;
        }

        public int GetStrength()
        {
            if(Items == null)
            {
                return 0;
            }

            int totalStrength = 0;
            foreach(Item item in Items)
            {
                totalStrength += item.Strength;
            }
            return totalStrength;
        }

        public int GetDexterity()
        {
            if (Items == null)
            {
                return 0;
            }

            int totalDexterity = 0;
            foreach (Item item in Items)
            {
                totalDexterity += item.Dexterity;
            }
            return totalDexterity;
        }

        public int GetIntelligence()
        {
            if (Items == null)
            {
                return 0;
            }
            int totalIntelligence = 0;
            foreach(Item item in Items)
            {
                totalIntelligence += item.Intelligence;
            }
            return totalIntelligence;
        }

        public void OnCharacterUpdate(CharacterEventArgs args)
        {
            if (CharacterUpdateEvent != null)
            {
                CharacterUpdateEvent(args);
            }
        }

    }
}
