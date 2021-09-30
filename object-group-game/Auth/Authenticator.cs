using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace object_group_game
{
    class Authenticator
    {
        private static Authenticator authenticator = null;

        private Authenticator()
        {
           
        }

        public static Authenticator GetAuthenticator()
        {
            if (authenticator == null)
            {
                authenticator = new Authenticator();
            }
            return authenticator;
        }


        private static byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
            {
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
            }
        }

        private static string HashSHA256(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString)) {
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }

        public Tuple<int, Player> Login(string username, string password)
        {
            MessageBox.Show(HashSHA256(password));
            //DB request
            Player player = new Player(username, new Character("thelegend27"), new List<Item>(), new List<Location>());
            return new Tuple<int, Player>(1, player);
        }

        public bool Register(string username, string password)
        {
            //DB request
            return false;
        }

        public bool Logout(Player player)
        {
            return true;
        }
    }
}
