using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RestSharp;
using Newtonsoft.Json;
using System;
using GameLibrary.Exceptions;

public class PlayerLoader : MonoBehaviour
{

    void ChangeLocalData(PlayerData playerData)
    {
        LocalPlayer.Instance.Username = playerData.CharacterName;
        LocalPlayer.Instance.CharacterName = playerData.CharacterName;
        LocalPlayer.Instance.Health = playerData.Health;
        LocalPlayer.Instance.Mana = playerData.Mana;
        LocalPlayer.Instance.Gold = playerData.Gold;
        LocalPlayer.Instance.Dexterity = playerData.Dexterity;
        LocalPlayer.Instance.Strength = playerData.Strength;
        LocalPlayer.Instance.Intelligence = playerData.Intelligence;
        LocalPlayer.Instance.ItemNameList = playerData.ItemNameList;
        LocalPlayer.Instance.PlayerRole = playerData.PlayerRole;
    }

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
            PlayerData playerData = JsonConvert.DeserializeObject<PlayerData>(response.Content);
            if(playerData == null)
            {
                throw new BadResponseException("Returned player is null!");
            }
            ChangeLocalData(playerData);
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
