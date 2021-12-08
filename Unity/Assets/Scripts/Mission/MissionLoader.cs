using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionLoader : MonoBehaviour
{
    [SerializeField]
    private GameObject missionsUI;
    [SerializeField]
    private MissionRenderer missionRenderer;
    [SerializeField]
    private GameObject UI;
    [SerializeField]
    private TMP_Text locationName;

    public void OpenMissions(LocationInfoHolder locationInfoHolder)
    {
        missionRenderer.Location = locationInfoHolder.Location;
        locationName.text = locationInfoHolder.Location.Name;
        UI.SetActive(false);
        missionsUI.SetActive(true);
    }
}
