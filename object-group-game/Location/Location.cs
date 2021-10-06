using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace object_group_game
{
	public class Location
	{
		public int ID { get; set; }
		public string DisplayName { get; set; }
		public LocationType Type { get; set; }
		public double Latitude { get; set; }
		public double Longtitude { get; set; }
		public int Radius { get; set; }
		public Item Item { get; set; }


		public Location (Database.Location location, Item item)
		{
			ID = location.ID;
			DisplayName = location.DisplayName;
			Type = location.Type;
			Latitude = location.Latitude;
			Longtitude = location.Longtitude;
			Radius = location.Radius;
			Item = item;
		}
	}
}
