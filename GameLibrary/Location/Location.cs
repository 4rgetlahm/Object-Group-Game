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
		private string _name;
		public string Name {
			get
			{
				return _name;
			}
			set
			{
				_name = value;
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

		private int _level;
		public int Level
        {
            get
            {
				return _level;
            }
            set
            {
				_level = value;
				OnLocationEdited(new LocationEventArgs(this));
            }
        }

		private List<Mission> _missions;
		public virtual List<Mission> Missions
        {
            get
            {
				return _missions;
            }
            set
            {
				_missions = value;
				OnLocationEdited(new LocationEventArgs(this));
            }
        }

		[JsonIgnore]
		public List<Character> Characters { get; set; }

		public delegate void LocationUpdateEventHandler(LocationEventArgs args);
		public event LocationUpdateEventHandler LocationUpdateEvent;


		public Location (string Name, LocationType LocationType, double Latitude, double Longtitude, int Radius)
		{
			this.Name = Name;
			this.LocationType = LocationType;
			Coordinate = new Coordinate(Latitude, Longtitude);
			this.Radius = Radius;
			this.Missions = new List<Mission>();
		}

		public Location(int locationID, string Name, LocationType LocationType, double Latitude, double Longtitude, int Radius, List<Mission> missions)
		{
			this.LocationID = locationID;
			this.Name = Name;
			this.LocationType = LocationType;
			Coordinate = new Coordinate(Latitude, Longtitude);
			this.Radius = Radius;
			this.Missions = missions;
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
