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
		public double Latitude { get; set; }
		public double Longtitude { get; set; }
		public int Radius { get; set; }
		public virtual List<Character> Characters {get;set;}


		public Location (string DisplayName, LocationType LocationType, double Latitude, double Longtitude, int Radius)
		{
			this.DisplayName = DisplayName;
			this.LocationType = LocationType;
			this.Latitude = Latitude;
			this.Longtitude = Longtitude;
			this.Radius = Radius;
		}

		protected Location()
        {

        }

		//Locations are equal if their coordinates match
        public bool Equals(Location other)
        {
			if(this.Latitude == other.Latitude && this.Longtitude == other.Longtitude)
            {
				return true;
            }
			return false;
        }
    }
}
