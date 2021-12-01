using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary
{
    public class PlayerEventArgs
    {
        public Player Player { get; set; }
        public PlayerEventArgs(Player player)
        {
            this.Player = player;
        }
    }
}
