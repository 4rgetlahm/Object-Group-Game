using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace object_group_game
{
    public class LocationEventArgs : EventArgs
    {
        public Location Location { get; set; }
        public LocationEventArgs(Location location)
        {
            Location = location;
        }
    }
}
