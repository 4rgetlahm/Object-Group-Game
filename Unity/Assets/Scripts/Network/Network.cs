using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;
using RestSharp;
using GameLibrary;

public class Network
{
    private static Network instance = null;
    public readonly RestClient restClient;
    private Network(){
        restClient = new RestClient("http://localhost:5000");
    }

    public static Network GetInstance(){
        if(instance == null){
            instance = new Network();
        }
        return instance;
    }
}
