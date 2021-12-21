using GameLibrary;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionRenderer : MonoBehaviour
{
    [SerializeField]
    private GameObject missionTemplate;
    [SerializeField]
    private TMP_Text templateTitle;
    [SerializeField]
    private TMP_Text templateDescription;
    [SerializeField]
    private TMP_Text templateType;

    [SerializeField]
    private RectTransform missionSelectorBackgroundTransform;
    [SerializeField]
    private float GAP_SIZE_Y;

    private List<GameObject> missionItems = new List<GameObject>();

    private Location _location;
    public Location Location { 
        get 
        {
            return _location;
        } set
        {
            ClearMissions();
            _location = value;
            LoadMissions();
        }
    }

    private int GetRowCount()
    {
        int columnCount = 0;
        float increment = missionTemplate.GetComponent<RectTransform>().sizeDelta.y + GAP_SIZE_Y;
        float positionY = 0;
        while (missionSelectorBackgroundTransform.sizeDelta.y - increment - positionY > 0)
        {
            columnCount++;
            positionY += increment;
        }
        return columnCount;
    }

    public void ClearMissions()
    {
        foreach (GameObject gameObject in missionItems)
        {
            gameObject.Destroy();
        }
        missionItems.Clear();
    }

    private void LoadMissions()
    {
        int rowCount = GetRowCount();
        missionTemplate.SetActive(true);
        int count = 0;
        foreach(Mission mission in Location.Missions)
        {
            // first change values, then instantiate
            templateTitle.text = mission.Title;
            templateDescription.text = mission.Description;
            templateType.text = mission.MissionType.ToString();

            GameObject instantiatedItem = Instantiate(missionTemplate, missionSelectorBackgroundTransform.transform);
            instantiatedItem.transform.localPosition = new Vector3(
                    missionTemplate.GetComponent<RectTransform>().localPosition.x,
                    missionTemplate.GetComponent<RectTransform>().localPosition.y - (count * (missionTemplate.GetComponent<RectTransform>().sizeDelta.y + GAP_SIZE_Y)),
                    0
                );
            instantiatedItem.GetComponent<MissionConfirmDialog>().Mission = mission;
            missionItems.Add(instantiatedItem);
            count++;
        }
        missionTemplate.SetActive(false);
    }
}
