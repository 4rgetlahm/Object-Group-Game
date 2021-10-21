using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace object_group_game
{
	[Table("Locations")]
	public class Location : IEquatable<Location>
	{
		[Key]
		public int LocationID { get; set; }
		public string DisplayName { get; set; }
		public LocationType LocationType { get; set; }
		public Coordinate Coordinate { get; set; }
		public int Radius { get; set; }
		public virtual List<Character> Characters { get; set; }


		public Location (string DisplayName, LocationType LocationType, double Latitude, double Longtitude, int Radius)
		{
			this.DisplayName = DisplayName;
			this.LocationType = LocationType;
			Coordinate = new Coordinate(Latitude, Longtitude);
			this.Radius = Radius;
		}

		protected Location()
        {

        }

		//Locations are equal if their coordinates match
        public bool Equals(Location other)
        {
			if(this.Coordinate.Latitude == other.Coordinate.Latitude && this.Coordinate.Longtitude == other.Coordinate.Longtitude)
            {
				return true;
            }
			return false;
        }
    }
}
