using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GameLibrary;
using System;
using System.Linq;

public class LocationTypeDropdown : MonoBehaviour
{
    private List<TMP_Dropdown.OptionData> GetOptions()
    {
        var enumList = Enum.GetValues(typeof(LocationType)).Cast<LocationType>().ToList();
        List<TMP_Dropdown.OptionData> list = new List<TMP_Dropdown.OptionData>();

        foreach(var enumValue in enumList)
        {
            list.Add(new TMP_Dropdown.OptionData(enumValue.ToString()));
		}

        return list;
	}

    // Start is called before the first frame update
    void Start()
    {
        TMP_Dropdown dropdown = GameObject.Find("TypeDropdown").GetComponent<TMP_Dropdown>();
        dropdown.AddOptions(GetOptions());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
