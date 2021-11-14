using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameButtons : MonoBehaviour
{
    [SerializeField]
    private GameObject adminTools;
    [SerializeField]
    private List<GameObject> UI;
    public void OnAdminToolsEnter()
    {
        UI.ForEach(obj => obj.SetActive(false));
        adminTools.SetActive(true);
    }
    public void OnAdminToolsExit()
    {
        UI.ForEach(obj => obj.SetActive(true));
        adminTools.SetActive(false);
    }
}
