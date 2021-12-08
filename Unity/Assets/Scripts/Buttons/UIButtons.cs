using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class UIButtons : MonoBehaviour
{
    [SerializeField]
    private GameObject adminTools;
    [SerializeField]
    private List<GameObject> UI;
    [SerializeField]
    private GameObject InventoryUI;
    [SerializeField]
    private GameObject MissionsUI;

    private void HideGameUI()
    {
        UI.ForEach(obj => obj.SetActive(false));
    }

    private void ShowGameUI()
    {
        UI.ForEach(obj => obj.SetActive(true));
    }

    public void EnterAdminTools()
    {
        HideGameUI();
        adminTools.SetActive(true);
    }
    public void ExitAdminTools()
    {
        HideGameUI();
        adminTools.SetActive(false);
    }

    public void EnterInventory()
    {
        HideGameUI();
        InventoryUI.SetActive(true);
    }

    public void ExitInventory()
    {
        ShowGameUI();
        InventoryUI.SetActive(false);
    }

    public void ExitMissions()
    {
        ShowGameUI();
        MissionsUI.SetActive(false);
    }

}
