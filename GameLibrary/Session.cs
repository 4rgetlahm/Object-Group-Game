using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace object_group_game
{
    static class Session
    {
        public static Player Player { get; set; }

        public static void Clear()
        {
            Player = null;
        }
    }
}
