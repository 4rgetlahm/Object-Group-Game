using GameLibrary;
using GameLibrary.Exceptions;
using GameLibrary.Inventory;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public class SessionModel
{
    public byte[] SessionID { get; set; }
    public SessionModel(byte[] SessionID)
    {
        this.SessionID = SessionID;
    }
}

public class LocalPlayer
{
    private static readonly Lazy<LocalPlayer> _instance =
        new Lazy<LocalPlayer>(() => new LocalPlayer());

    public static LocalPlayer Instance
    {
        get
        {
            return _instance.Value;
        }
    }

    public delegate void LocalPlayerUpdateEventHandler(EventArgs args);
    public event LocalPlayerUpdateEventHandler LocalPlayerUpdateEvent;

    public Timer SessionUpdater;

    private Player _player = null;
    public Player Player {
        get
        {
            return _player;
        }
        set
        {
            if (this._player != null)
            {
                this._player.PlayerUpdateEvent -= this.OnPlayerUpdate; // if the player changes, stop tracking it
            }
            _player = value;
            this._player.PlayerUpdateEvent += this.OnPlayerUpdate; // reassign tracking to new player
            this._player.Character.CharacterUpdateEvent += this.OnCharacterUpdate;
            OnLocalPlayerUpdate(EventArgs.Empty);
        }
    }

    private LocalPlayer()
    {
        var autoEvent = new AutoResetEvent(false);
        var sessionUpdater = new SessionUpdater();
        this.SessionUpdater = new Timer(sessionUpdater.SendUpdate, autoEvent, 0, 30000);
        Application.quitting += OnApplicationQuit;
    }

    public void OnCharacterUpdate(CharacterEventArgs args)
    {
        OnLocalPlayerUpdate(EventArgs.Empty);
    }

    public void OnPlayerUpdate(PlayerEventArgs args)
    {
        this._player.Character.CharacterUpdateEvent += this.OnCharacterUpdate;
        OnLocalPlayerUpdate(EventArgs.Empty);
    }


    public void OnLocalPlayerUpdate(EventArgs args)
    {
        if(LocalPlayerUpdateEvent != null)
        {
            LocalPlayerUpdateEvent(args);
        }
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
            if(response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new BadResponseException("Bad response!");
            }
        } 
        catch(Exception e)
        {
            Debug.Log("Exception occured on logging out: " + e.StackTrace);
        }
    }

    void OnApplicationQuit()
    {
        SessionUpdater.Dispose();
        SendLogoutRequest();
    }


}
