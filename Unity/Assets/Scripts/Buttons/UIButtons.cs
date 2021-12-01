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

    private void HideGameUI()
    {
        UI.ForEach(obj => obj.SetActive(false));
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
}
