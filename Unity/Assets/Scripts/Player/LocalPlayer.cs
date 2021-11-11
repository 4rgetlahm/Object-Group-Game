using GameLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private double _health = 20.0;
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
    private double _mana = 5.0;
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
    private double _experience = 78.0;
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
    private double _gold = 85.0;
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

    }


    public void OnLocalPlayerUpdate(EventArgs args)
    {
        if(LocalPlayerUpdateEvent != null)
        {
            LocalPlayerUpdateEvent(args);
        }
    }


}
