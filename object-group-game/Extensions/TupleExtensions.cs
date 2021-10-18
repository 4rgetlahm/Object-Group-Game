using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace object_group_game.Extensions
{
    public static class TupleExtensions
    {
        public static Player GetPlayer(this Tuple<int, Player> response)
        {
            return response.Item2;
        }

        public static int GetStatus(this Tuple<int, Player> response)
        {
            return response.Item1;
        }
    }
}
