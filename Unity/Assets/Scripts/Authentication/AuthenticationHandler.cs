using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RestSharp;
using Newtonsoft.Json;
using System;


class SessionResponse
{
    public string sessionID { get; set; }
}

class RequestModel
{
    public string Username { get; set; }
    public string Password { get; set; }

    public RequestModel(string Username, string Password)
    {
        this.Username = Username;
        this.Password = Password;
    }
}

public class AuthenticationHandler : MonoBehaviour
{
    public GameObject usernameInput;
    public GameObject passwordInput;

    private RestRequest FormatAuthRequest(string apiURL, Method method)
    {
        string username = usernameInput.GetComponent<TMP_InputField>().text;
        string password = passwordInput.GetComponent<TMP_InputField>().text;

        var request = new RestRequest(apiURL, method);

        var body = new RequestModel(username, password);

        request.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(body, Formatting.Indented), ParameterType.RequestBody);
        request.RequestFormat = DataFormat.Json;

        return request;
    }

    void OnLogin()
    {

    }

    public void Register()
    {
        try
        {
            var response = Network.GetInstance().restClient.Execute(FormatAuthRequest("/register", Method.POST));
            if (!response.IsSuccessful)
            {
                Debug.Log("Error authenticating");
                return;
            }
            Tuple<int, SessionResponse> serverResponse = JsonConvert.DeserializeObject<Tuple<int, SessionResponse>>(response.Content);
            switch (serverResponse.Item1)
            {
                case 1:
                    Session.sessionID = Convert.FromBase64String(serverResponse.Item2.sessionID);
                    Debug.Log("Registered, status 1, session: " + serverResponse.Item2.sessionID);
                    OnLogin();
                    break;
                case -1:
                    Debug.Log("Username already exists!");
                    break;
                case -2:
                    Debug.Log("Invalid username!");
                    break;
                default:
                    Debug.Log("Something went wrong...");
                    break;
            }
        }
        catch (Exception e)
        {
            Debug.Log("Caught exception");
        }
    }

    public void Login(){
        try
        {
            var response = Network.GetInstance().restClient.Execute(FormatAuthRequest("/login", Method.POST));
            if (!response.IsSuccessful)
            {
                Debug.Log("Error authenticating");
                return;
            }
            Tuple<int, SessionResponse> serverResponse = JsonConvert.DeserializeObject<Tuple<int, SessionResponse>>(response.Content);
            switch (serverResponse.Item1)
            {
                case 1:
                    Session.sessionID = Convert.FromBase64String(serverResponse.Item2.sessionID);
                    Debug.Log("Logged in status 1, session: " + serverResponse.Item2.sessionID);
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