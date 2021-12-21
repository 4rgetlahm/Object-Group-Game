using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageBoxData : MonoBehaviour
{
    [SerializeField]
    private TMP_Text messageTextLabel;
    [SerializeField]
    private TMP_Text buttonTextLabel;

    public void SetMessageBoxData(string message, string buttonText)
    {
        this.messageTextLabel.text = message;
        this.buttonTextLabel.text = buttonText;
    }
}
