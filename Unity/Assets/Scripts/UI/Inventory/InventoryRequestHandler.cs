using GameLibrary;
using GameLibrary.Inventory;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ItemEquipModel
{
    public ItemEquipModel(Item item, string sessionID)
    {
        this.Item = item;
        this.SessionID = sessionID;
    }
    public Item Item { get; set; }
    public string SessionID { get; set; }
}

class EquipmentResponse
{
    public Equipment Equipment { get; set; }
}

public class InventoryRequestHandler : MonoBehaviour
{
    public void SendItemChangeRequest(ItemRenderer itemRenderer)
    {
        var request = new RestRequest("/equipment/equip", Method.POST);
        var body = new ItemEquipModel(itemRenderer.Item, Convert.ToBase64String(Session.SessionID));

        request.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(body, Formatting.Indented), ParameterType.RequestBody);
        request.RequestFormat = DataFormat.Json;

        Debug.Log(request);

        try
        {
            var response = Network.Instance.restClient.Execute(request);
            if (!response.IsSuccessful)
            {
                Debug.Log("An error occured on Item Equip.");
                return;
            }
            Equipment serverResponse = JsonConvert.DeserializeObject<Equipment>(response.Content);
            if(serverResponse != null)
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
