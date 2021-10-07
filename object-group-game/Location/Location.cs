using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace object_group_game
{
	public class Location
	{
		[Key]
		public int LocationID { get; set; }
		public string DisplayName { get; set; }
		public LocationType LocationType { get; set; }
		public double Latitude { get; set; }
		public double Longtitude { get; set; }
		public int Radius { get; set; }


		public Location (int LocationID, string DisplayName, LocationType LocationType, double Latitude, double Longtitude, int Radius)
		{
			this.LocationID = LocationID;
			this.DisplayName = DisplayName;
			this.LocationType = LocationType;
			this.Latitude = Latitude;
			this.Longtitude = Longtitude;
			this.Radius = Radius;
		}
	}
}
