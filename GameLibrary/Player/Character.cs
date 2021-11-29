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
        private double _stamina;
        public double Stamina
        {
            get {
                return _stamina;
            }
            set {
                _stamina = value;
                OnCharacterUpdate(new CharacterEventArgs(this));
            }
        }
        private int _gold;
        public int Gold { 
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
        public List<Item> Items { get; set; }
        public List<Location> VisitedLocations { get; set; }

        public delegate void CharacterUpdateEventHandler(CharacterEventArgs args);
        public event CharacterUpdateEventHandler CharacterUpdateEvent;

        protected Character()
        {

        }

        public Character(string name, int gold = 0)
        {
            this.Name = name;
            this.Gold = gold;

            UpdateStamina();
            UpdateHealth();
        }

        private void UpdateStamina()
        {
            this.Stamina = this.GetEndurance() + 1;
		}

        private void UpdateHealth()
        {
            this.Health = this.GetConstitution() + 1;
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

        public int GetEndurance()
        {
            if (Items == null)
            {
                return 0;
            }
            int totalEndurance = 0;
            foreach (Item item in Items)
            {
                totalEndurance += item.Endurance;
            }
            return totalEndurance;
        }

        public int GetConstitution()
        {
            if (Items == null)
            {
                return 0;
            }
            int totalConstitution = 0;
            foreach (Item item in Items)
            {
                totalConstitution += item.Constitution;
            }
            return totalConstitution;
        }

        public List<string> getItemNames()
        {
            List<string> names = new List<string>();
            foreach(Item item in this.Items)
            {
                names.Add(item.DisplayName);
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
    }
}
