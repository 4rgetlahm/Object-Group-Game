using GameLibrary;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionRenderer : MonoBehaviour
{
    [SerializeField]
    private GameObject MissionTemplate;
    [SerializeField]
    private TMP_Text TemplateTitle;
    [SerializeField]
    private TMP_Text TemplateDescription;
    [SerializeField]
    private TMP_Text TemplateType;

    [SerializeField]
    private RectTransform MissionSelectorBackgroundTransform;
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
        float increment = MissionTemplate.GetComponent<RectTransform>().sizeDelta.y + GAP_SIZE_Y;
        float positionY = 0;
        while (MissionSelectorBackgroundTransform.sizeDelta.y - increment - positionY > 0)
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
        MissionTemplate.SetActive(true);
        int count = 0;
        foreach(Mission mission in Location.Missions)
        {
            // first change values, then instantiate
            TemplateTitle.text = mission.Title;
            TemplateDescription.text = mission.Description;
            TemplateType.text = mission.MissionType.ToString();
            GameObject instantiatedItem = Instantiate(MissionTemplate, MissionSelectorBackgroundTransform.transform);
            instantiatedItem.transform.localPosition = new Vector3(
                    MissionTemplate.GetComponent<RectTransform>().localPosition.x,
                    MissionTemplate.GetComponent<RectTransform>().localPosition.y - (count * (MissionTemplate.GetComponent<RectTransform>().sizeDelta.y + GAP_SIZE_Y)),
                    0
                );
            missionItems.Add(instantiatedItem);
            count++;
        }
        MissionTemplate.SetActive(false);
    }
}
