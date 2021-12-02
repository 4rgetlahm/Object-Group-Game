using GameLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemRenderer : MonoBehaviour
{
    [SerializeField]
    private ItemMapper ItemMapper;

    public Item Item = null;

    private void Start()
    {
        RenderItem();
    }

    private void RenderItem()
    {
        if (this.Item == null)
        {
            Image image = this.gameObject.GetComponent<Image>();
            image = null;
            return;
        }
        try
        {
            this.gameObject.GetComponent<Image>().sprite = ItemMapper.ItemSprites.Find(itemsprite => itemsprite.itemModel.Equals(Item.ItemModel)).itemSprite;
        }
        catch (Exception e)
        {
            Debug.LogError("Error rendering item!");
        }
    }
}
