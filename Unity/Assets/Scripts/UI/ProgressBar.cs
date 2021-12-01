using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

enum BarType
{
    HEALTH,
    MANA,
    EXPERIENCE
}
public class ProgressBar : MonoBehaviour
{
    public double Maximum;
    public double Current;
    public Image Mask;

    [SerializeField]
    TextMeshProUGUI Value;

    [SerializeField]
    BarType barType;

    void Start()
    {
        LocalPlayer.Instance.LocalPlayerUpdateEvent += this.OnLocalPlayerDataChange;
    }

    void OnLocalPlayerDataChange(EventArgs eventArgs)
    {
        switch (barType)
        {
            case BarType.HEALTH:
                Current = LocalPlayer.Instance.Player.Character.Health;
                break;
            case BarType.MANA:
                Current = LocalPlayer.Instance.Player.Character.Mana;
                break;
            case BarType.EXPERIENCE:
                Current = LocalPlayer.Instance.Player.Character.Experience;
                break;
        }
        if (Value != null)
        {
            Value.text = Current + " / " + Maximum;
        }
        Mask.fillAmount = (float)Current / (float)Maximum;
    }
}