using GameLibrary;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class ExpeditionModel
{
    public ExpeditionModel(string SessionID, Mission mission)
    {
        this.SessionID = SessionID;
        this.Mission = mission;
    }
    public string SessionID { get; set; }
    public Mission Mission { get; set; }
}

public class MissionConfirmDialog : MonoBehaviour
{
    [SerializeField]
    private DialogMaker dialogMaker;
    public Mission Mission { get; set; }
    public void CreateDialog()
    {
        dialogMaker.ShowDialog(
            "Are you sure that you want to go to this expedition?",
            "Yes",
            "No",
            SendExpeditionRequest,
            OnCancelled);
    }

    public void SendExpeditionRequest()
    {
        Debug.Log(Mission.Title);
        if(this.Mission == null)
        {
            Debug.LogError("Mission is null!");
            return;
        }
        var request = new RestRequest("/player/expedition", Method.POST);
        var body = new ExpeditionModel(Convert.ToBase64String(Session.SessionID), this.Mission);

        request.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(body, Formatting.Indented), ParameterType.RequestBody);
        request.RequestFormat = DataFormat.Json;

        try
        {
            var response = Network.Instance.restClient.Execute(request);
            if (!response.IsSuccessful)
            {
                Debug.Log("An error occured on Expedition request.");
                return;
            }
            Expedition serverResponse = JsonConvert.DeserializeObject<Expedition>(response.Content);
            if(serverResponse == null)
            {
                GameObject.Find("MessageBoxGenerator").GetComponent<MessageBoxGenerator>().ShowMessageBox("You are already on expedition!", "OK");
                return;
            }

            LocalPlayer.Instance.Player.Character.Expedition = serverResponse;
        }
        catch (Exception e)
        {
            Debug.Log("Caught exception: \n" + e.Message + " \n" + e.StackTrace);
        }

    }

    public void OnCancelled()
    {

    }
}
