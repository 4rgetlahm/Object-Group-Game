using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageBoxPrimaryButton : MonoBehaviour
{
    [SerializeField]
    private GameObject messageBox;

    public void ExitMessageBox()
    {
        messageBox.Destroy();
    }
}
