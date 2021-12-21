using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


class RegisterRequestModel
{
    public string Username { get; set; }
    public string Password { get; set; }
    public int CharacterType { get; set; }

    public RegisterRequestModel(string Username, string Password, int CharacterType)
    {
        this.Username = Username;
        this.Password = Password;
        this.CharacterType = CharacterType;
    }
}

public class RegisterHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject usernameInput;
    [SerializeField]
    private GameObject passwordInput;
    [SerializeField]
    private CharacterSelection relatedSelection;

    private RestRequest FormatRegisterRequest(string apiURL, Method method)
    {
        string username = usernameInput.GetComponent<TMP_InputField>().text;
        string password = passwordInput.GetComponent<TMP_InputField>().text;
        //Debug.Log(relatedSelection.GetComponent<CharacterSelection>().GetRelatedSelections().Find(v => v.selected.Equals(true)));
        int selectedCharacter = relatedSelection.value; // start by assigning main node
        if (!relatedSelection.selected) // if main node is not selected, find the node that is selected
        {
            selectedCharacter = relatedSelection.GetRelatedSelections().Find(v => v.selected == true).value;
        }

        var request = new RestRequest(apiURL, method);
        var body = new RegisterRequestModel(username, password, selectedCharacter);

        request.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(body, Formatting.Indented), ParameterType.RequestBody);
        request.RequestFormat = DataFormat.Json;

        Debug.Log(request);

        return request;
    }

    void OnRegister()
    {
        SceneManager.LoadScene("Game");
    }

    public void Register()
    {
        try
        {
            var response = Network.Instance.restClient.Execute(FormatRegisterRequest("/register", Method.POST));
            if (!response.IsSuccessful)
            {
                Debug.Log("Error authenticating");
                return;
            }
            Tuple<int, SessionResponse> serverResponse = JsonConvert.DeserializeObject<Tuple<int, SessionResponse>>(response.Content);
            switch (serverResponse.Item1)
            {
                case 1:
                    Session.SessionID = Convert.FromBase64String(serverResponse.Item2.SessionID);
                    Debug.Log("Registered, status 1, session: " + serverResponse.Item2.SessionID);
                    OnRegister();
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
            Debug.Log("Caught exception: \n" + e.Message + " \n" + e.StackTrace);
        }
    }
}
