using GameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Authentication
{
    public interface IAuthenticator
    {
        public Tuple<int, Session> Login(string username, string password);
        public Tuple<int, Session> Register(string username, string password, CharacterType characterType);
        public int Logout(Session session);
    }
}
