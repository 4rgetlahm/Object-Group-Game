using GameLibrary.Exceptions;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoutDialog : MonoBehaviour
{
    [SerializeField]
    private DialogMaker dialogMaker;
    public void LogoutButton()
    {
        dialogMaker.ShowDialog("Are you sure you want to log out?", "Yes", "No",
            OnLogout, OnCancel);
    }

    private void SendLogoutRequest()
    {
        var request = new RestRequest("/logout", Method.POST);
        var body = new SessionModel(Session.SessionID);

        request.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(body, Formatting.Indented), ParameterType.RequestBody);
        request.RequestFormat = DataFormat.Json;

        try
        {
            var response = Network.Instance.restClient.Execute(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new BadResponseException("Bad response!");
            }
        }
        catch (Exception e)
        {
            Debug.Log("Exception occured on logging out: " + e.StackTrace);
        }
    }

    void OnLogout()
    {
        SendLogoutRequest();
        LocalPlayer.Instance.SessionUpdater.Dispose();
        SceneManager.LoadScene("AuthenticationScene");
    }

    void OnCancel()
    {

    }
}
