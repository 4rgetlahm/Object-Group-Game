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

public class MapLoader : MonoBehaviour
{
	public struct Coordinate
	{
		public double Latitude { get; set; }
		public double Longtitude { get; set; }
	}
	class LocationModel
    {
		public string DisplayName { get; set; }
		public LocationType LocationType { get; set; }

		public Coordinate Coordinate;
    }


	[SerializeField]
	AbstractMap _map;

	[SerializeField]
	List<LocationModel> Locations;

	[SerializeField]
	float _spawnScale;

	[SerializeField]
	GameObject _markerPrefab;

	Dictionary<LocationModel, GameObject> SpawnedObjects;

	void Start()
	{
		Locations = new List<LocationModel>();
		var request = new RestRequest("/locationlist?sessionid=" + Uri.EscapeDataString(Convert.ToBase64String(Session.SessionID)), Method.GET);
		try
		{
			var response = Network.Instance.restClient.Execute(request);
			if(response.StatusCode != System.Net.HttpStatusCode.OK)
            {
				throw new BadResponseException("Request failed!");
            }
			Locations = JsonConvert.DeserializeObject<List<LocationModel>>(response.Content);
		}
		catch(Exception e)
        {
			Debug.LogError(e.Message + " " + e.StackTrace);
			return;
        }

		SpawnedObjects = new Dictionary<LocationModel, GameObject>();
		foreach(var location in Locations)
		{
			var instance = Instantiate(_markerPrefab);
			instance.GetComponent<MaterialSetter>().Set(location.LocationType);
			instance.GetComponent<LabelSetter>().Set(location.DisplayName);
			instance.transform.localPosition = _map.GeoToWorldPosition(new Vector2d(location.Coordinate.Latitude, location.Coordinate.Longtitude), true);
			instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
			SpawnedObjects.Add(location, instance);
		}
	}

	private void Update()
	{
		foreach (var location in SpawnedObjects)
		{
			location.Value.transform.localPosition = _map.GeoToWorldPosition(new Vector2d(location.Key.Coordinate.Latitude, location.Key.Coordinate.Longtitude), true);
			location.Value.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
		}
	}
}
