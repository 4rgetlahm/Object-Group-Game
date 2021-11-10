using GameLibrary;
using Server.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Authentication
{
    public class SessionManager
    {
        private static SessionManager instance;

        private Timer SessionHandlingTimer { get; set; }
        private Authentication.SessionHandler SessionHandler { get; set; }
        public Dictionary<Session, Player> Sessions { get; set; }
        public Dictionary<Session, DateTime> LastRequest { get; set; }

        private SessionManager()
        {
            Sessions = new Dictionary<Session, Player>();
            LastRequest = new Dictionary<Session, DateTime>();

            SessionHandler = new Authentication.SessionHandler(Int64.Parse(Configuration.GetInstance().Settings["sessionexpirytime"]));
            var autoEvent = new AutoResetEvent(false);

            SessionHandlingTimer = new Timer(SessionHandler.ExpireSessions, null, 0, Int64.Parse(Configuration.GetInstance().Settings["sessionexpirychecktime"]));
        }

        public static SessionManager GetInstance()
        {
            if(instance == null)
            {
                instance = new SessionManager();
            }
            return instance;
        }

        public Session CreateSession(Player player)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] id = new byte[64];
                rng.GetBytes(id);
                Session session = new Session(id);
                lock (this.Sessions)
                {
                    Sessions.Add(session, player);
                }
                lock (this.LastRequest)
                {
                    LastRequest.Add(session, DateTime.Now);
                }
                SessionHandler.SubscribeToPlayerUpdateEvent(player);
                return session;
            }
        }

        public void UpdateLastRequest(Session session, DateTime dateTime)
        {
            lock (this.LastRequest)
            {
                var findSession = LastRequest.Keys.FirstOrDefault(key => key.SessionID.SequenceEqual(session.SessionID));
                if(findSession != null){
                    LastRequest[session] = dateTime;
                    return;
                }
                throw new BadSessionException("Session doesn't exist");

            }
        }

        public void UpdateLastRequest(Player player, DateTime dateTime)
        {
            lock (this.Sessions)
            {
                if (!Sessions.ContainsValue(player))
                {
                    return;
                }

                Session session = Sessions.FirstOrDefault(x => x.Value == player).Key;
                if(session != null)
                {
                    LastRequest[session] = dateTime;
                }
            }
        }

        public bool DoesSessionExist(Session session)
        {
            lock (this.Sessions)
            {
                return this.Sessions.ContainsKey(session);
            }
        }

        public bool IsLoggedIn(Player player)
        {
            lock (this.Sessions)
            {
                return this.Sessions.ContainsValue(player);
            }
        }

        public void RemoveSession(Session session)
        {
            Sessions.Remove(session);
        }
    }
}
