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
    public string Username { get; set; }
    public string CharacterName { get; set; }
    public double Health { get; set; }
    public double Mana { get; set; }
    public double Experience { get; set; }
    public double Gold { get; set; }
    public double Dexterity { get; set; }
    public double Strength { get; set; }
    public double Intelligence { get; set; }

    public List<string> ItemNameList;

    private LocalPlayer()
    {

    }


}
