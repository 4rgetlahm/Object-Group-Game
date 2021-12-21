using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageBoxGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject messageBoxPrefab;

    public void ShowMessageBox(string message, string buttonText)
    {
        GameObject instantiatedMessageBox = GameObject.Instantiate(messageBoxPrefab);
        instantiatedMessageBox.GetComponent<MessageBoxData>().SetMessageBoxData(message, buttonText);
        instantiatedMessageBox.SetActive(true);
    }
}
