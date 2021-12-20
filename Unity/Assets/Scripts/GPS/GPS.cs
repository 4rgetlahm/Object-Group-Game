using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPS : MonoBehaviour
{


    /*private static readonly Lazy<GPS> _instance =
        new Lazy<GPS>(() => new GPS());*/

    public bool isLocationServiceActive = false;

    public float Latitude { get; set; }
    public float Longtitude { get; set; }

    /*public static GPS Instance
    {
        get
        {
            return _instance.Value;
        }
    }*/

    private void Start()
    {
        StartCoroutine(StartLocationService());
    }

    private IEnumerator StartLocationService()
    {
        Debug.Log("Start service");
        if (!Input.location.isEnabledByUser)
        {
            Debug.LogError("Location service is not enabled");
            yield break;
        }

        Input.location.Start();
        int waitFor = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && waitFor > 0)
        {
            Debug.Log("Waiting");
            waitFor--;
            yield return new WaitForSeconds(1);
        }

        if (Input.location.status == LocationServiceStatus.Failed || Input.location.status == LocationServiceStatus.Initializing)
        {
            Debug.LogError("Location service failed or timed out");
            yield break;
        }

        isLocationServiceActive = true;
        this.Latitude = Input.location.lastData.latitude;
        this.Longtitude = Input.location.lastData.longitude;

        yield break;
    }
}
