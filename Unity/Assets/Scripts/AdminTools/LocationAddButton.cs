using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using GameLibrary;

public class LocationAddButton : MonoBehaviour
{
    TMP_InputField nameText;
    TMP_Dropdown typeText;
    TMP_InputField latitudeText;
    TMP_InputField longtitudeText;
    TMP_InputField radiusText;

    // Start is called before the first frame update
    void Start()
    {
        nameText = GameObject.Find("LocationNameInput").GetComponent<TMP_InputField>();
        latitudeText = GameObject.Find("LatitudeInput").GetComponent<TMP_InputField>();
        longtitudeText = GameObject.Find("LongtitudeInput").GetComponent<TMP_InputField>();
        radiusText = GameObject.Find("RadiusInput").GetComponent<TMP_InputField>();
        typeText = GameObject.Find("TypeDropdown").GetComponent<TMP_Dropdown>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {
        string name = nameText.text;
        double latitude = double.Parse(latitudeText.text);
        double longtitude = double.Parse(longtitudeText.text);
        int radius = int.Parse(radiusText.text);
        LocationType type = (LocationType)Enum.Parse(typeof(LocationType), typeText.options[typeText.value].text);

        new Location(name, type, latitude, longtitude, radius);
        // insert into database
    }
}
