using GameLibrary.Inventory;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemSprite
{
    public ItemModel itemModel;
    public Sprite itemSprite;
}

public class ItemMapper : MonoBehaviour {
    [SerializeField]
    public List<ItemSprite> ItemSprites;   
}
