using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace object_group_game
{
    public class CharacterEventArgs
    {
        public Character Character { get; set; }
        public CharacterEventArgs(Character character)
        {
            Character = character;
        }
    }
}
