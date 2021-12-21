using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AuthenticationSceneSwitch : MonoBehaviour
{
    public void SwitchToLoginScene()
    {
        SceneManager.LoadScene("AuthenticationScene");
    }

    public void SwitchToRegisterScene()
    {
        SceneManager.LoadScene("RegistrationScene");
    }
}
