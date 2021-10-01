using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace object_group_game
{
    public class Attributes
    {
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Intelligence { get; set; }

        public Attributes(int strength = 0, int dexterity = 0, int intelligence = 0)
        {
            this.Strength = strength;
            this.Dexterity = dexterity;
            this.Intelligence = intelligence;
        }

        public void Reset()
        {
            this.Strength = 0;
            this.Dexterity = 0;
            this.Intelligence = 0;
        }

    }
}
