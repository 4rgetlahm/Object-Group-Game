using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace object_group_game
{
    class Location
    {
        private float latitude;
        private float longtitude;
        private LocationType locationType;

        public Location(float latitude, float longtitude, LocationType locationType)
        {
            this.latitude = latitude;
            this.longtitude = longtitude;
            this.locationType = locationType;
        }
    }
}
