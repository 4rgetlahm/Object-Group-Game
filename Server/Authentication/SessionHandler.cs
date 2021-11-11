using GameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Authentication
{
    public class SessionHandler
    {
        private long expiryTime;
        public SessionHandler(long expiryTime)
        {
            this.expiryTime = expiryTime;
        }

        // Subscribes to every player that has a session
        public void SubscribeToPlayerUpdateEvent(Player player)
        {
            player.PlayerUpdateEvent += this.OnPlayerUpdate;
        }
        // Updates last request time by any changes
        public void OnPlayerUpdate(PlayerEventArgs args)
        {
            SessionManager.Instance.UpdateLastRequest(args.Player, DateTime.Now);
        }

        public void ExpireSessions(Object stateInfo)
        {
            lock (SessionManager.Instance.LastRequest)
            {
                var timedOutSessions =
                    from entry in SessionManager.Instance.LastRequest
                    where (DateTime.Now - entry.Value).TotalMilliseconds >= expiryTime
                    select entry;

                lock (SessionManager.Instance.Sessions)
                {
                    foreach (var pair in timedOutSessions)
                    {
                        SessionManager.Instance.RemoveSession(pair.Key);
                    }
                }
            }
        } 
    }
}
