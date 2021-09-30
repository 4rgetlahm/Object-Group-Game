using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace object_group_game
{
    class Authenticator
    {
        private Authenticator authenticator = null;

        private Authenticator()
        {
           
        }

        public Authenticator GetAuthenticator()
        {
            if (this.authenticator == null)
            {
                this.authenticator = new Authenticator();
            }
            return this.authenticator;
        }

        public bool Login(Player player, string password)
        {
            //DB request
            return false;
        }

        public bool Register(Player player, string password)
        {
            //DB request
            return false;
        }
    }
}
