using GameLibrary;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpeditionTracker : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        if(LocalPlayer.Instance.Player.Character.Expedition == null)
        {
            return;
        }
        if(DateTime.Compare((
            LocalPlayer.Instance.Player.Character.Expedition.StartTime.ToUniversalTime() + LocalPlayer.Instance.Player.Character.Expedition.Duration), 
            DateTime.Now.ToUniversalTime()) < 0)
        {
            Debug.Log("sendrequest");
            SendExpeditionCheckRequest();
        }
        //LocalPlayer.Instance.Player.Character.Expedition = null;
    }

    public void SendExpeditionCheckRequest()
    {
        var request = new RestRequest("/player/expedition/reward", Method.POST);
        var body = LocalPlayer.Instance.Player.Character.Expedition;
        request.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(body, Formatting.Indented), ParameterType.RequestBody);
        request.RequestFormat = DataFormat.Json;
        try
        {
            var response = Network.Instance.restClient.Execute(request);
            if (!response.IsSuccessful)
            {
                throw new Exception("Response was not successful.");
            }
            Tuple<int, Character> deserializedResponse = JsonConvert.DeserializeObject<Tuple<int, Character>>(response.Content);
            if(deserializedResponse.Item1 == 1)
            {
                LocalPlayer.Instance.Player.Character = deserializedResponse.Item2;
                GameObject.Find("MessageBoxGenerator").GetComponent<MessageBoxGenerator>().
                    ShowMessageBox("You have completed your expedition!", "OK");
            }
            //Debug.Log(response.Content);
        }
        catch (Exception e)
        {
            Debug.LogError("Unexpected exception: " + e.Message);
        }
    }
}
