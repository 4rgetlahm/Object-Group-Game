using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace object_group_game
{
    public class Character
    {
        [Key]
        public int CharacterID { get; set; }
        public string Name { get; private set; }
        public double Health { get; set; }
        public double Experience { get; set; }    
        public double Mana { get; set; }
        public double Gold { get; set; }
        public ICollection<Item> Items { get; set; }
        public ICollection<Location> VisitedLocations { get; set; }

        public Character(int CharacterID, string name, double health, double experience, double mana, double gold, ICollection<Item> items, ICollection<Location> visitedLocations)
        {
            this.CharacterID = CharacterID;
            this.Name = name;
            this.Health = health;
            this.Experience = experience;
            this.Mana = mana;
            this.Gold = gold;
            this.Items = items;
            this.VisitedLocations = visitedLocations;
        }

        public Character(int CharacterID, string name, double health, double experience, double mana, double gold)
        {
            this.CharacterID = CharacterID;
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

    }
}
