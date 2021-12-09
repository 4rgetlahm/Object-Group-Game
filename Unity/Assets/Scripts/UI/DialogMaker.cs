using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogMaker : MonoBehaviour
{
    [SerializeField]
    private GameObject dialog;
    [SerializeField]
    private TMP_Text messageObject;
    [SerializeField]
    private TMP_Text primaryButtonText;
    [SerializeField]
    private TMP_Text secondaryButtonText;

    private Action primaryAction;
    private Action secondaryAction;

    public void ShowDialog(string dialogMessage, string primaryButtonText, string secondaryButtonText, 
       Action primaryAction, Action secondaryAction)
    {
        this.messageObject.text = dialogMessage;
        this.primaryButtonText.text = primaryButtonText;
        this.secondaryButtonText.text = secondaryButtonText;
        this.primaryAction = primaryAction;
        this.secondaryAction = secondaryAction;
        this.dialog.SetActive(true);
    }

    public void OnPrimaryButtonClick()
    {
        primaryAction.Invoke();
        this.HideDialog();
    }

    public void OnSecondaryButtonClick()
    {
        secondaryAction.Invoke();
        this.HideDialog();
    }

    public void HideDialog()
    {
        dialog.SetActive(false);
    }
}
