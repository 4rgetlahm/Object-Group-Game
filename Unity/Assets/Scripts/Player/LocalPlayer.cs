using GameLibrary.Exceptions;
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

public class PlayerData
{
    public string Username { get; set; }
    public string CharacterName { get; set; }
    public double Health { get; set; }
    public double Mana { get; set; }
    public double Experience { get; set; }
    public double Gold { get; set; }
    public double Dexterity { get; set; }
    public double Strength { get; set; }
    public double Intelligence { get; set; }
    public PlayerRole PlayerRole { get; set; }

    public List<string> ItemNameList;
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

    private Timer sessionUpdater;

    private string _username = "";
    public string Username { 
        get
        {
            return _username;
        }
        set
        {
            _username = value;
            OnLocalPlayerUpdate(EventArgs.Empty);
        }
    }
    private string _characterName = "";
    public string CharacterName
    {
        get
        {
            return _characterName;
        }
        set
        {
            _characterName = value;
            OnLocalPlayerUpdate(EventArgs.Empty);
        }
    }
    private double _health = 0.0;
    public double Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            OnLocalPlayerUpdate(EventArgs.Empty);
        }
    }
    private double _mana = 0.0;
    public double Mana
    {
        get
        {
            return _mana;
        }
        set
        {
            _mana = value;
            OnLocalPlayerUpdate(EventArgs.Empty);
        }
    }
    private double _experience = 0.0;
    public double Experience
    {
        get
        {
            return _experience;
        }
        set
        {
            _experience = value;
            OnLocalPlayerUpdate(EventArgs.Empty);
        }
    }
    private double _gold = 0.0;
    public double Gold
    {
        get
        {
            return _gold;
        }
        set
        {
            _gold = value;
            OnLocalPlayerUpdate(EventArgs.Empty);
        }
    }
    private double _dexterity = 0.0;
    public double Dexterity
    {
        get
        {
            return _dexterity;
        }
        set
        {
            _dexterity = value;
            OnLocalPlayerUpdate(EventArgs.Empty);
        }
    }
    private double _strength = 0.0;
    public double Strength
    {
        get
        {
            return _strength;
        }
        set
        {
            _strength = value;
            OnLocalPlayerUpdate(EventArgs.Empty);
        }
    }
    private double _intelligence = 0.0;
    public double Intelligence
    {
        get
        {
            return _intelligence;
        }
        set
        {
            _intelligence = value;
            OnLocalPlayerUpdate(EventArgs.Empty);
        }
    }

    private PlayerRole _playerRole = PlayerRole.Default;
    public PlayerRole PlayerRole
    {
        get
        {
            return _playerRole;
        }
        set
        {
            _playerRole = value;
            OnLocalPlayerUpdate(EventArgs.Empty);
        }
    }

    private List<string> _itemNameList = new List<string>();
    public List<string> ItemNameList
    {
        get
        {
            return _itemNameList;
        }
        set
        {
            _itemNameList = value;
            OnLocalPlayerUpdate(EventArgs.Empty);
        }
    }

    private LocalPlayer()
    {
        var autoEvent = new AutoResetEvent(false);
        var sessionUpdater = new SessionUpdater();
        this.sessionUpdater = new Timer(sessionUpdater.SendUpdate, autoEvent, 0, 30000);
        Application.quitting += OnApplicationQuit;
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
        sessionUpdater.Dispose();
        SendLogoutRequest();
    }


}
