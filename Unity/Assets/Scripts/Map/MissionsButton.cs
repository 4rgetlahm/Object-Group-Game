using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionsButton : MonoBehaviour
{
    [SerializeField]
    private LocationInfoHolder locationInfoHolder;

    private GameObject mainUI;

    public void OpenMissions()
    {
        if (!mainUI.activeSelf)
        {
            return;
        }
        if (GameObject.Find("MessageBox(Clone)") != null)
        {
            return;
        }
        GameObject.Find("MissionLoader").GetComponent<MissionLoader>().OpenMissions(locationInfoHolder);
    }

    private void Start()
    {
        mainUI = GameObject.Find("UI");
    }

    public void OnMouseDown()
    {
        OpenMissions();
    }
}
