using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;
using RestSharp;
using GameLibrary;
using System;

public class Network
{
    private static readonly Lazy<Network> _instance =
    new Lazy<Network>(() => new Network());

    public static Network Instance
    {
        get
        {
            return _instance.Value;
        }
    }

    public readonly RestClient restClient;
    private Network(){
        restClient = new RestClient("http://localhost:5000");
    }
}
