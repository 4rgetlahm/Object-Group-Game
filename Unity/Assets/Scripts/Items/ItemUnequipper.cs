using GameLibrary.Inventory;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class ItemUnequipModel
{
    public ItemUnequipModel(string sessionID, ItemType itemType)
    {
        this.ItemType = itemType;
        this.SessionID = sessionID;
    }
    public ItemType ItemType { get; set; }
    public string SessionID { get; set; }
}

public class ItemUnequipper : MonoBehaviour
{
    public void SendItemUnequipRequest(ItemLoader itemLoader)
    {
        var request = new RestRequest("/equipment/unequip", Method.POST);
        var body = new ItemUnequipModel(Convert.ToBase64String(Session.SessionID), itemLoader.ItemType);

        request.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(body, Formatting.Indented), ParameterType.RequestBody);
        request.RequestFormat = DataFormat.Json;

        Debug.Log(JsonConvert.SerializeObject(body, Formatting.Indented));

        try
        {
            var response = Network.Instance.restClient.Execute(request);
            if (!response.IsSuccessful)
            {
                Debug.Log("An error occured on Item Equip.");
                return;
            }
            Equipment serverResponse = JsonConvert.DeserializeObject<Equipment>(response.Content);
            Debug.Log(serverResponse);
            if (serverResponse != null)
            {
                LocalPlayer.Instance.Player.Character.Equipment = serverResponse;
            }
        }
        catch (Exception e)
        {
            Debug.Log("Caught exception: \n" + e.Message + " \n" + e.StackTrace);
        }
    }
}
