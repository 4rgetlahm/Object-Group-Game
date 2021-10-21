using object_group_game.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
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


        static byte[] GenerateSaltedHash(byte[] plainText, byte[] salt)
        {
            HashAlgorithm algorithm = new SHA256Managed();

            byte[] plainTextWithSaltBytes = new byte[plainText.Length + salt.Length];

            for (int i = 0; i < plainText.Length; i++)
            {
                plainTextWithSaltBytes[i] = plainText[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                plainTextWithSaltBytes[plainText.Length + i] = salt[i];
            }

            return algorithm.ComputeHash(plainTextWithSaltBytes);
        }

        public Tuple<int, Player> Login(string username, string password)
        {
            try
            {
                using (var db = new DataContext())
                {
                    Player player = db.Player.First(p => p.Name == username);
                    if (player == null) // if player doesn't exist
                    {
                        return new Tuple<int, Player>(-1, null); // -1 = player doesn't exist
                    }

                    string salt = (string) db.Entry(player).Property("Salt").CurrentValue;
                    string hash = (string) db.Entry(player).Property("Password").CurrentValue;
                    byte[] generatedHash = GenerateSaltedHash(Encoding.UTF8.GetBytes(password), Convert.FromBase64String(salt));

                    if(hash.Equals(Convert.ToBase64String(generatedHash)))
                    {
                        db.Entry(player).Reference(c => c.Character).Load();
                        db.Entry(player.Character).Collection(i => i.Items).Load();
                        db.Entry(player.Character).Collection(l => l.VisitedLocations).Load();
                        return new Tuple<int, Player>(1, player); // login
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return new Tuple<int, Player>(0, null);
        }

        public Tuple<int, Player> Register(string username, string password)
        {
            Regex regex = new Regex(Configuration.GetInstance().Settings["usernameregex"]);
            if (!regex.IsMatch(username))
            {
                return new Tuple<int, Player>(-2, null);
            }

            try
            {
                using (var db = new DataContext())
                {
                    if (db.Player.Any(p => p.Name == username)) // if username already exists
                    {
                        return new Tuple<int, Player>(-1, null);
                    }

                    RNGCryptoServiceProvider rngCSP = new RNGCryptoServiceProvider();
                    var salt = new byte[32];
                    rngCSP.GetBytes(salt);

                    Player player = new Player(username);
                    player.Character = new Character(name: username, gold: 50.0);
                    db.Entry(player).Property("Salt").CurrentValue = Convert.ToBase64String(salt);
                    db.Entry(player).Property("Password").CurrentValue = Convert.ToBase64String(GenerateSaltedHash(Encoding.UTF8.GetBytes(password), salt));
                    db.Add(player);
                    db.SaveChanges();

                    return new Tuple<int, Player>(1, player);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return new Tuple<int, Player>(0, null);
        }

        public bool Logout(Player player)
        {
            return true;
        }
    }
}
