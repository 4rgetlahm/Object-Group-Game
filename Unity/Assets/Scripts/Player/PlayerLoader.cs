using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RestSharp;
using Newtonsoft.Json;
using System;
using GameLibrary.Exceptions;
using GameLibrary;

class SessionModel
{
    public byte[] SessionID { get; set; }
    public SessionModel(byte[] SessionID)
    {
        this.SessionID = SessionID;
    }
}

public class PlayerLoader : MonoBehaviour
{
    [SerializeField]
    private TMP_Text usernameLabel;

    [SerializeField]
    private TMP_Text characterNameLabel;

    [SerializeField]
    private TMP_Text healthLabel;
    [SerializeField]
    private TMP_Text manaLabel;
    [SerializeField]
    private TMP_Text expLabel;

    [SerializeField]
    private TMP_Text goldLabel;

    [SerializeField]
    private TMP_Text dexterityLabel;
    [SerializeField]
    private TMP_Text strengthLabel;
    [SerializeField]
    private TMP_Text intelligenceLabel;

    [SerializeField]
    private TMP_Text itemListLabel;

    public class PlayerData
    {
        public string Username { get; set; }
        public string CharacterName { get; set; }
        public double Health { get; set; }
        public double Mana { get;  set; }
        public double Experience { get; set; }
        public double Gold { get; set; }
        public double Dexterity { get; set; }
        public double Strength { get; set; }
        public double Intelligence { get; set; }

        public List<string> ItemNameList;
    }


    void updateLabels(PlayerData playerData)
    {
        usernameLabel.text = playerData.Username;
        characterNameLabel.text = playerData.CharacterName;

        healthLabel.text = "Health: " + playerData.Health;
        manaLabel.text = "Mana: " + playerData.Mana;
        expLabel.text = "Experience: " + playerData.Experience;

        goldLabel.text = "Gold: " + playerData.Gold;

        dexterityLabel.text = "Dexterity: " + playerData.Dexterity;
        strengthLabel.text = "Strength: " + playerData.Strength;
        intelligenceLabel.text = "Intelligence: " + playerData.Intelligence;
        itemListLabel.text = String.Join("\n", playerData.ItemNameList.ToArray());
    }

    void Start()
    {
        var request = new RestRequest("/session/player", Method.POST);
        var body = new SessionModel(Session.SessionID);

        request.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(body, Formatting.Indented), ParameterType.RequestBody);
        request.RequestFormat = DataFormat.Json;

        try
        {
            var response = Network.GetInstance().restClient.Execute(request);
            if (!response.IsSuccessful)
            {
                throw new BadResponseException("Player data was not received successfully");
            }
            PlayerData playerData = JsonConvert.DeserializeObject<PlayerData>(response.Content);
            if(playerData == null)
            {
                throw new BadResponseException("Returned player is null!");
            }

            updateLabels(playerData);
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
