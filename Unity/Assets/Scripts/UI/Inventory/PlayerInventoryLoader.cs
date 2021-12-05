using GameLibrary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryLoader : MonoBehaviour
{

    [SerializeField]
    private GameObject InventoryItemTemplate;
    [SerializeField]
    private float GAP_SIZE_X;
    [SerializeField]
    private float GAP_SIZE_Y;
    [SerializeField]
    private RectTransform BackgroundRectTransform;

    private List<GameObject> inventoryItems = new List<GameObject>();
    void OnEnable()
    {
        LoadInventoryItems();
        LocalPlayer.Instance.Player.Character.CharacterEquipmentChangeEvent += this.OnCharacterEquipmentUpdate;
    }

    void OnCharacterEquipmentUpdate(CharacterEventArgs args)
    {
        ReloadInventoryItems();
    }
    
    private int GetItemsPerRow()
    {

        int itemsPerRow = 0;
        float increment = InventoryItemTemplate.GetComponent<RectTransform>().sizeDelta.x + GAP_SIZE_X;
        float positionX = 0;
        while(BackgroundRectTransform.sizeDelta.x - increment > positionX)
        {
            itemsPerRow++;
            positionX += increment;
        }
        return itemsPerRow;
    }

    private int GetRowCount()
    {
        /*return (int)(BackgroundRectTransform.sizeDelta.y - InventoryItemTemplate.GetComponent<RectTransform>().position.y 
            / (InventoryItemTemplate.GetComponent<RectTransform>().sizeDelta.y + GAP_SIZE_Y));*/
        int columnCount = 0;
        float increment = InventoryItemTemplate.GetComponent<RectTransform>().sizeDelta.y + GAP_SIZE_Y;
        float positionY = 0;
        while (BackgroundRectTransform.sizeDelta.y - increment - positionY > 0)
        {
            columnCount++;
            positionY += increment;
        }
        return columnCount;
    }

    public void ReloadInventoryItems()
    {
        ClearInventory();
        LoadInventoryItems();
    }

    public void ClearInventory()
    {
        foreach(GameObject gameObject in inventoryItems)
        {
            gameObject.Destroy();
        }
        inventoryItems.Clear();
    }

    public void LoadInventoryItems()
    {
        int rowCount = GetRowCount();
        int itemsPerRow = GetItemsPerRow();
        InventoryItemTemplate.SetActive(true);
        int currentItem = 0;
        Debug.Log(LocalPlayer.Instance.Player.Character.Items.Count);
        for(int i = 0; i != rowCount; i++)
        {
            if (currentItem == LocalPlayer.Instance.Player.Character.Items.Count)
            {
                break;
            }
            for (int j = 0; j != itemsPerRow; j++){
                if(currentItem == LocalPlayer.Instance.Player.Character.Items.Count)
                {
                    break;
                }
                GameObject instantiatedItem = Instantiate(InventoryItemTemplate, BackgroundRectTransform.transform);
                instantiatedItem.transform.localPosition = new Vector3(
                        InventoryItemTemplate.GetComponent<RectTransform>().localPosition.x + (j * (InventoryItemTemplate.GetComponent<RectTransform>().sizeDelta.x + GAP_SIZE_X)),
                        InventoryItemTemplate.GetComponent<RectTransform>().localPosition.y - (i * (InventoryItemTemplate.GetComponent<RectTransform>().sizeDelta.y + GAP_SIZE_Y)),
                        0
                    );
                instantiatedItem.GetComponent<ItemRenderer>().Item = LocalPlayer.Instance.Player.Character.Items[currentItem];
                currentItem++;
                inventoryItems.Add(instantiatedItem);
            }
        }
        InventoryItemTemplate.SetActive(false);
    }
}
