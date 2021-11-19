using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdminButtons : MonoBehaviour
{
    public void OnToolsExitButtonClick()
    {
        SceneManager.LoadScene("Game");
    }
}
