using GameLibrary.Exceptions;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionUpdater
{
    class SessionModel
    {
        public byte[] SessionID { get; set; }
        public SessionModel(byte[] SessionID)
        {
            this.SessionID = SessionID;
        }
    }

    public SessionUpdater()
    {

    }

    public void SendUpdate(System.Object stateInfo)
    {
        var request = new RestRequest("/session/update", Method.POST);
        var body = new SessionModel(Session.SessionID);
        request.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(body, Formatting.Indented), ParameterType.RequestBody);
        request.RequestFormat = DataFormat.Json;

        try
        {
            Console.WriteLine(body);
            var response = Network.GetInstance().restClient.Execute(request);
            if (!response.IsSuccessful)
            {
                throw new SessionUpdateFailException("Response was not successful.");
            }
            Debug.Log("OK");
        }
        catch (SessionUpdateFailException e)
        {
            Debug.LogError("Session update failed: " + e.Message);
        } catch (Exception e)
        {
            Debug.LogError("Unexpected exception: " + e.Message);
        }
    }
}
