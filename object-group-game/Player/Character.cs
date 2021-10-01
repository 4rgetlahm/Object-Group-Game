using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace object_group_game
{
    public class Character
    {
        public string Name { get; private set; }
        public double Health { get; set; }
        public double Experience { get; set; }    
        public double Mana { get; set; }
        public double Gold { get; set; }

        private Inventory Inventory { get; set; }

        public Attributes Attributes { get; set; }

        public Character(string name)
        {
            this.Name = name;
            this.ResetStats();
        }

        public void SetStats(double health = 0, double experience = 0, double mana = 0, double gold = 0)
        {
            this.Health = health;
            this.Experience = experience;
            this.Mana = mana;
            this.Gold = gold;
        }

        private void ResetStats()
        {
            this.Health = 0.0;
            this.Experience = 0.0;
            this.Mana = 0.0;
            this.Gold = 0.0;

            Attributes = new Attributes();
            Inventory = new Inventory();
        }

    }
}
