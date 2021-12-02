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
    public double Stamina { get; set; }
    public int Gold { get; set; }
    public int Dexterity { get; set; }
    public int Strength { get; set; }
    public int Intelligence { get; set; }
    public int Constitution { get; set; }
    public int Endurance { get; set; }

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
    private double _stamina = 0.0;
    public double Stamina
    {
        get
        {
            return _stamina;
        }
        set
        {
            _stamina = value;
            OnLocalPlayerUpdate(EventArgs.Empty);
        }
    }
    private int _gold = 0;
    public int Gold
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
    private int _dexterity = 0;
    public int Dexterity
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
    private int _strength = 0;
    public int Strength
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
    private int _intelligence = 0;
    public int Intelligence
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
    private int _constitution = 0;
    public int Constitution
    {
        get {
            return _constitution;
        }
        set {
            _constitution = value;
            OnLocalPlayerUpdate(EventArgs.Empty);
        }
    }
    private int _endurance = 0;
    public int Endurance
    {
        get {
            return _endurance;
        }
        set {
            _endurance = value;
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
