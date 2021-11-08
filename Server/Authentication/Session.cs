using GameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Server
{
    public class Session
    {
        public byte[] SessionID { get; set; }
        public Session(byte[] sessionID)
        {
            SessionID = sessionID;
        }
    }
}
