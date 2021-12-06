using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox;
using Mapbox.Unity.Map;
using Mapbox.Examples;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using RestSharp;
using Mapbox.Json;
using System;
using GameLibrary.Exceptions;
using GameLibrary;

public class MapLoader : MonoBehaviour
{
	public struct Coordinate
	{
		public double Latitude { get; set; }
		public double Longtitude { get; set; }
	}
	class LocationModel : IEquatable<LocationModel>
	{
		public int LocationID { get; set; }
		public string Name { get; set; }
		public LocationType LocationType { get; set; }

		public int Radius { get;  set;}

		public Coordinate Coordinate;

		public bool Equals(LocationModel other)
        {
            if(this.LocationID == other.LocationID)
            {
				return true;
            }
			return false;
        }
    }


	[SerializeField]
	AbstractMap _map;

	[SerializeField]
	List<LocationModel> VisitedLocations;

	[SerializeField]
	List<LocationModel> AllLocations;

	[SerializeField]
	float _spawnScale;

	[SerializeField]
	GameObject _markerPrefab;

	Dictionary<LocationModel, GameObject> SpawnedObjects;

	void Start()
	{
		VisitedLocations = new List<LocationModel>();
		 
		var request = new RestRequest("/player/location/list?sessionid=" + Uri.EscapeDataString(Convert.ToBase64String(Session.SessionID)), Method.GET);
		var requestAllLocations = new RestRequest("/locationlist", Method.GET);
		try
		{
			var response = Network.Instance.restClient.Execute(request);
			if(response.StatusCode != System.Net.HttpStatusCode.OK)
            {
				throw new BadResponseException("Request failed!");
            }
			VisitedLocations = JsonConvert.DeserializeObject<List<LocationModel>>(response.Content);

			var allLocationsResponse = Network.Instance.restClient.Execute(requestAllLocations);
			if (response.StatusCode != System.Net.HttpStatusCode.OK)
			{
				throw new BadResponseException("Request failed!");
			}
			AllLocations = JsonConvert.DeserializeObject<List<LocationModel>>(response.Content);
		}
		catch(Exception e)
        {
			Debug.LogError(e.Message + " " + e.StackTrace);
			return;
        }

		SpawnedObjects = new Dictionary<LocationModel, GameObject>();
		foreach(var location in AllLocations)
		{
			var instance = Instantiate(_markerPrefab);
			instance.GetComponent<ColorSetter>().Set(location.LocationType);
			instance.GetComponent<LabelSetter>().Set(location.Name);
			//instance.GetComponent<RadiusRenderer>().SetRadius(location.Radius);
			instance.GetComponent<LocationInfoHolder>().Location = new Location(location.LocationID, location.Name, location.LocationType, location.Coordinate.Latitude, location.Coordinate.Longtitude, location.Radius);
			instance.GetComponent<LocationInfoHolder>().Visited = false;
			if (VisitedLocations.Contains(location))
            {
				instance.GetComponent<LocationInfoHolder>().Visited = true;
			}
			instance.transform.localPosition = _map.GeoToWorldPosition(new Vector2d(location.Coordinate.Latitude, location.Coordinate.Longtitude), true);
			instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
			SpawnedObjects.Add(location, instance);
			Debug.Log(location.Radius);
		}
	}

	private void Update()
	{
		foreach (var location in SpawnedObjects)
		{
			location.Value.transform.localPosition = _map.GeoToWorldPosition(new Vector2d(location.Key.Coordinate.Latitude, location.Key.Coordinate.Longtitude), true);
			//location.Value.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
			//Debug.Log(1 / _map.Zoom);
			//location.Value.GetComponent<RadiusRenderer>().ChangeZoomLevel(_map.Zoom);
		}
	}
}
