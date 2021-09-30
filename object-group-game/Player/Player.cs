using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace object_group_game
{
    class Player
    {
        public string Name { get; private set; }
        public Character Character { get; set; }
        public List<Item> Items { get; set; }
        public List<Location> VisitedLocations { get; set; }

        public Player(string name, Character character)
        {
            this.Character = character;
        }

        public Player(string name, Character character, List<Item> items, List<Location> visitedLocations)
        {
            this.Character = character;
            this.Items = items;
            this.VisitedLocations = visitedLocations;
        }
    }
}
