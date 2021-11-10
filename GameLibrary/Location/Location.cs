using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary
{
	[Table("Locations")]
	[Serializable]
	public class Location : IEquatable<Location>
	{
		[Key]
		private int _locationID;
		public int LocationID {
			get
			{
				return _locationID;
			}
			set
			{
				_locationID = value;
				OnLocationEdited(new LocationEventArgs(this));
			}
		}
		private string _displayName;
		public string DisplayName {
			get
			{
				return _displayName;
			}
			set
			{
				_displayName = value;
				OnLocationEdited(new LocationEventArgs(this));
			}
		}
		private LocationType _locationType;
		public LocationType LocationType {
			get
			{
				return _locationType;
			}
			set
			{
				_locationType = value;
				OnLocationEdited(new LocationEventArgs(this));
			}
		}
		private Coordinate _coordinate;
		public Coordinate Coordinate {
			get
			{
				return _coordinate;
			}
			set
			{
				_coordinate = value;
				OnLocationEdited(new LocationEventArgs(this));
			}
		}
		private int _radius;
		public int Radius { 
			get 
			{
				return _radius;
			} 
			set
			{
				_radius = value;
				OnLocationEdited(new LocationEventArgs(this));
			}
		}
		[JsonIgnore]
		public List<Character> Characters { get; set; }

		public delegate void LocationUpdateEventHandler(LocationEventArgs args);
		public event LocationUpdateEventHandler LocationUpdateEvent;


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

		protected virtual void OnLocationEdited(LocationEventArgs e)
        {
			if(LocationUpdateEvent != null)
            {
				LocationUpdateEvent(e);
            }
        }
    }
}
