﻿using GameLibrary;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Server.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Controllers
{
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


    [ApiController]
    [Route("session/player")]
    public class PlayerSessionController : Controller
    {
        [HttpPost]
        public PlayerData Post([FromBody] Session body)
        {
            if(body == null){
                return null;
            }   
            foreach(var session in SessionManager.Instance.Sessions){
                if(session.Key.SessionID.SequenceEqual(body.SessionID)){
                    Player player = SessionManager.Instance.Sessions[session.Key];
                    PlayerData response = new PlayerData{
                        Username = player.Name,
                        CharacterName = player.Character.Name,
                        Health = player.Character.Health,
                        Mana = player.Character.Mana,
                        Experience = player.Character.Experience,
                        Gold = player.Character.Gold,
                        Strength = player.Character.GetStrength(),
                        Dexterity = player.Character.GetDexterity(),
                        Intelligence = player.Character.GetIntelligence(),
                        ItemNameList = player.Character.getItemNames()
                    };

                    /*var serialized = JsonConvert.SerializeObject(SessionManager.Instance.Sessions[session.Key]);
                    Console.WriteLine(serialized);
                    var deserialized = JsonConvert.DeserializeObject<Player>(serialized);
                    Console.WriteLine(deserialized.PlayerID);
                    Console.WriteLine(deserialized.Name);
                    Console.WriteLine(deserialized.Character.Mana);*/
                    return response;
                }
            }
            return null;
            /*
            Session session = SessionManager.Instance.Sessions.Keys.Where(key => key.SessionID == body.SessionID).FirstOrDefault();
            if (session == null)
            {
                Console.WriteLine("no session");
                return null;
            }
            Console.WriteLine(SessionManager.Instance.Sessions[session].Name);
            Console.WriteLine(SessionManager.Instance.Sessions[session].Character.Gold);
            return SessionManager.Instance.Sessions[session];
            */
        }
    }
}