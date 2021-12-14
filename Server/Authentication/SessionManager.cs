using GameLibrary;
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
        private static readonly Lazy<SessionManager> _instance =
            new Lazy<SessionManager>(() => new SessionManager());

        public static SessionManager Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        private Timer SessionHandlingTimer { get; set; }
        private Authentication.SessionHandler SessionHandler { get; set; }
        public Dictionary<Session, Player> Sessions { get; set; }
        public Dictionary<Session, DateTime> LastRequest { get; set; }

        private SessionManager()
        {
            Sessions = new Dictionary<Session, Player>();
            LastRequest = new Dictionary<Session, DateTime>();

            SessionHandler = new SessionHandler(Int64.Parse(Configuration.GetInstance().Settings["sessionexpirytime"]));
            var autoEvent = new AutoResetEvent(false);

            SessionHandlingTimer = new Timer(SessionHandler.ExpireSessions, null, 0, Int64.Parse(Configuration.GetInstance().Settings["sessionexpirychecktime"]));
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
                if (LastRequest.ContainsKey(session))
                {
                    LastRequest[session] = dateTime;
                }
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

        public Session GetRealSession(Session session)
        {
            lock (this.Sessions)
            { 
                Session realSession = this.Sessions.Keys.FirstOrDefault(s => s.SessionID.SequenceEqual(session.SessionID));
                return realSession;
            }
        }

        public Session GetRealSession(byte[] session)
        {
            lock (this.Sessions)
            {
                Session realSession = this.Sessions.Keys.FirstOrDefault(s => s.SessionID.SequenceEqual(session));
                return realSession;
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
