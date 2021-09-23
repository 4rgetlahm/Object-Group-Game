using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace object_group_game
{
    class Location
    {
        private float Latitude { get; set; }
        private float Longtitude { get; set; }
        private LocationType LocationType { get; set; }

        public Location(float latitude, float longtitude, LocationType locationType)
        {
            this.Latitude = latitude;
            this.Longtitude = longtitude;
            this.LocationType = locationType;
        }
    }
}
