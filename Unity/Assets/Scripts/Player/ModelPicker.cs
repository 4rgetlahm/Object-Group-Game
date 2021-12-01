using GameLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelPicker : MonoBehaviour
{
    [SerializeField]
    private Image ModelImage;

    [SerializeField]
    private Sprite[] Models;
    private void OnEnable()
    {
        if (LocalPlayer.Instance.Player != null)
        {
            UpdateSprite((int)LocalPlayer.Instance.Player.Character.CharacterType);
        }
        LocalPlayer.Instance.LocalPlayerUpdateEvent += OnLocalPlayerCreated;
        //LocalPlayer.Instance.Player.Character.CharacterTypeChangeEvent += OnCharacterTypeChanged;
    }

    // We want to start tracking Character Type change only after Character is initialized, so we wait for LocalPlayer to be created (first update)
    // and then we unsubscribe
    private void OnLocalPlayerCreated(EventArgs eventaArgs)
    {
        UpdateSprite((int)LocalPlayer.Instance.Player.Character.CharacterType);
        LocalPlayer.Instance.Player.Character.CharacterTypeChangeEvent += OnCharacterTypeChanged;
        LocalPlayer.Instance.LocalPlayerUpdateEvent -= OnLocalPlayerCreated;
    }

    private void OnCharacterTypeChanged(CharacterEventArgs args)
    {
        UpdateSprite((int)args.Character.CharacterType);
    }

    private void UpdateSprite(int spriteID)
    {
        Debug.Log(spriteID);
        Debug.Log(Models[spriteID]);
        ModelImage.sprite = Models[spriteID];
    }
}
