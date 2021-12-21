using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RestSharp;
using Newtonsoft.Json;
using System;
using GameLibrary.Exceptions;
using GameLibrary;

public class PlayerLoader : MonoBehaviour
{

    void Start()
    {
        var request = new RestRequest("/session/player", Method.POST);
        var body = new SessionModel(Session.SessionID);

        request.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(body, Formatting.Indented), ParameterType.RequestBody);
        request.RequestFormat = DataFormat.Json;

        try
        {
            var response = Network.Instance.restClient.Execute(request);
            if (!response.IsSuccessful)
            {
                throw new BadResponseException("Player data was not received successfully");
            }
            Debug.Log(response.Content);
            Player player = JsonConvert.DeserializeObject<Player>(response.Content);

            if(player == null)
            {
                throw new BadResponseException("Returned player is null!");
            }
            LocalPlayer.Instance.Player = player;
        }
        catch (BadResponseException e)
        {
            Debug.Log("Caught BadResponseException, can't proceed with the game. Exiting.");
            Debug.Log(e.Message);
            Application.Quit();
        }
        catch (Exception e)
        {
            Debug.Log("Caught exception: " + e.Message + "\n" + e.StackTrace + "\n" + e.Source);
        }
    }

}
