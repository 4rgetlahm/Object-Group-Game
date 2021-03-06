using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RestSharp;
using Newtonsoft.Json;
using System;
using UnityEngine.SceneManagement;
using GameLibrary.Exceptions;
using GameLibrary;
using System.Threading;

class SessionResponse
{
    public string SessionID { get; set; }
}

class LoginRequestModel
{
    public string Username { get; set; }
    public string Password { get; set; }

    public LoginRequestModel(string Username, string Password)
    {
        this.Username = Username;
        this.Password = Password;
    }
}

public class AuthenticationHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject usernameInput;
    [SerializeField]
    private GameObject passwordInput;

    private RestRequest FormatAuthRequest(string apiURL, Method method)
    {
        string username = usernameInput.GetComponent<TMP_InputField>().text;
        string password = passwordInput.GetComponent<TMP_InputField>().text;

        var request = new RestRequest(apiURL, method);
        var body = new LoginRequestModel(username, password);

        request.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(body, Formatting.Indented), ParameterType.RequestBody);
        request.RequestFormat = DataFormat.Json;

        return request;
    }

    void OnLogin()
    { 
        SceneManager.LoadScene("Game");
    }

    public void Login(){
        try
        {
            var response = Network.Instance.restClient.Execute(FormatAuthRequest("/login", Method.POST));
            if (!response.IsSuccessful)
            {
                Debug.Log("Error authenticating");
                return;
            }
            Tuple<int, SessionResponse> serverResponse = JsonConvert.DeserializeObject<Tuple<int, SessionResponse>>(response.Content);
            Debug.Log(serverResponse);
            switch (serverResponse.Item1)
            {
                case 1:
                    Session.SessionID = Convert.FromBase64String(serverResponse.Item2.SessionID);
                    Debug.Log("Logged in status 1, session: " + serverResponse.Item2.SessionID);
                    OnLogin();
                    break;
                case -1:
                    Debug.Log("Player doesn't exist");
                    break;
                case -2:
                    Debug.Log("Player is already logged in");
                    break;
                default:
                    Debug.Log("Incorrect username or password!");
                    break;
            }

        }
        catch (Exception e)
        {
            Debug.Log("Caught exception");
        }
    }
}
