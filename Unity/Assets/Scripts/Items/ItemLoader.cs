using GameLibrary.Inventory;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameLibrary;

public class ItemLoader : MonoBehaviour
{
    [SerializeField]
    private Image ItemImage;
    [SerializeField]
    public ItemType ItemType;
    [SerializeField]
    private ItemMapper ItemMapper;
    [SerializeField]
    private TMP_Text ItemNameLabel;

    public void OnEnable()
    {
        this.OnEquipmentChange(new CharacterEventArgs(LocalPlayer.Instance.Player.Character));
        LocalPlayer.Instance.Player.Character.CharacterEquipmentChangeEvent += OnEquipmentChange;
    }

    private void SetNoItem()
    {
        ItemImage.sprite = ItemMapper.ItemSprites.Find(item => item.itemModel == ItemModel.NONE).itemSprite;
        ItemNameLabel.text = "Nothing";
    }

    public void OnEquipmentChange(CharacterEventArgs eventArgs)
    {
        switch (this.ItemType)
        {
            case ItemType.HELMET:
                Debug.Log(LocalPlayer.Instance.Player.Character.Equipment.Helmet);
                if (LocalPlayer.Instance.Player.Character.Equipment.Helmet != null)
                {
                    ItemImage.sprite = ItemMapper.ItemSprites.Find(
                        item => item.itemModel == LocalPlayer.Instance.Player.Character.Equipment.Helmet.ItemModel).itemSprite;
                    ItemNameLabel.text = LocalPlayer.Instance.Player.Character.Equipment.Helmet.Name;
                    break;
                }
                SetNoItem();
                break;
            case ItemType.BODY:
                if (LocalPlayer.Instance.Player.Character.Equipment.BodyItem != null)
                {
                    ItemImage.sprite = ItemMapper.ItemSprites.Find(
                    item => item.itemModel == LocalPlayer.Instance.Player.Character.Equipment.BodyItem.ItemModel).itemSprite;
                    ItemNameLabel.text = LocalPlayer.Instance.Player.Character.Equipment.BodyItem.Name;
                    break;
                }
                SetNoItem();
                break;
            case ItemType.LEGS:
                if (LocalPlayer.Instance.Player.Character.Equipment.LegItem != null)
                {
                    ItemImage.sprite = ItemMapper.ItemSprites.Find(
                    item => item.itemModel == LocalPlayer.Instance.Player.Character.Equipment.LegItem.ItemModel).itemSprite;
                    ItemNameLabel.text = LocalPlayer.Instance.Player.Character.Equipment.LegItem.Name;
                    break;
                }
                SetNoItem();
                break;
            case ItemType.BOOTS:
                if (LocalPlayer.Instance.Player.Character.Equipment.Boots != null)
                {
                    ItemImage.sprite = ItemMapper.ItemSprites.Find(
                    item => item.itemModel == LocalPlayer.Instance.Player.Character.Equipment.Boots.ItemModel).itemSprite;
                    ItemNameLabel.text = LocalPlayer.Instance.Player.Character.Equipment.Boots.Name;
                    break;
                }
                SetNoItem();
                break;
            case ItemType.WEAPON:
                if (LocalPlayer.Instance.Player.Character.Equipment.Weapon != null)
                {
                    ItemImage.sprite = ItemMapper.ItemSprites.Find(
                    item => item.itemModel == LocalPlayer.Instance.Player.Character.Equipment.Weapon.ItemModel).itemSprite;
                    ItemNameLabel.text = LocalPlayer.Instance.Player.Character.Equipment.Weapon.Name;
                    break;
                }
                SetNoItem();
                break;
            default:
                ItemImage= null;
                ItemNameLabel.text = "Undefined";
                break;
        }
    }


}
