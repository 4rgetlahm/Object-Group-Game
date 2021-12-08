using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionsButton : MonoBehaviour
{
    [SerializeField]
    private LocationInfoHolder locationInfoHolder;
    public void OpenMissions()
    {
        GameObject.Find("MissionLoader").GetComponent<MissionLoader>().OpenMissions(locationInfoHolder);
    }

    public void OnMouseDown()
    {
        Debug.Log("onmousedown");
        OpenMissions();
    }
}
