using GameLibrary;
using GameLibrary.Database;
using Server;
using Server.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Server.Authentication
{
    public class Authenticator : IAuthenticator
    {
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

        public Tuple<int, Session> Login(string username, string password)
        {
            try
            {
                using (var db = new DataContext())
                {
                    Player player = db.Player.First(p => p.Name == username);
                    if (player == null) // if player doesn't exist
                    {
                        return new Tuple<int, Session>(-1, null); // -1 = player doesn't exist
                    }
                    
                    if (SessionManager.Instance.IsLoggedIn(player))
                    {
                        return new Tuple<int, Session>(-2, null); // -2 = player is logged in
                    }

                    string salt = (string) db.Entry(player).Property("Salt").CurrentValue;
                    string hash = (string) db.Entry(player).Property("Password").CurrentValue;
                    byte[] generatedHash = GenerateSaltedHash(Encoding.UTF8.GetBytes(password), Convert.FromBase64String(salt));

                    if(hash.Equals(Convert.ToBase64String(generatedHash)))
                    {
                        db.Entry(player).Reference(c => c.Character).Load();
                        db.Entry(player.Character).Collection(i => i.Items).Load();
                        db.Entry(player.Character).Collection(l => l.VisitedLocations).Load();
                        db.Entry(player.Character).Reference(e => e.Equipment).Load();
                        db.Entry(player.Character).Reference(e => e.Expedition).Load();
                        if (player.Character.Expedition != null)
                        {
                            db.Entry(player.Character.Expedition).Reference(m => m.Mission).Load();
                        }
                        return new Tuple<int, Session>(1, SessionManager.Instance.CreateSession(player)); // login
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + " " + e.StackTrace);
            }

            return new Tuple<int, Session>(0, null);
        }

        public Tuple<int, Session> Register(string username, string password, CharacterType characterType = CharacterType.MODEL_MALE_1)
        {
            Regex regex = new Regex(Configuration.GetInstance().Settings["usernameregex"]);
            if (!regex.IsMatch(username))
            {
                return new Tuple<int, Session>(-2, null);
            }

            try
            {
                using (var db = new DataContext())
                {
                    if (db.Player.Any(p => p.Name == username)) // if username already exists
                    {
                        return new Tuple<int, Session>(-1, null);
                    }

                    RNGCryptoServiceProvider rngCSP = new RNGCryptoServiceProvider();
                    var salt = new byte[32];
                    rngCSP.GetBytes(salt);

                    Player player = new Player(username);
                    player.Character = new Character(username, characterType);
                    db.Entry(player).Property("Salt").CurrentValue = Convert.ToBase64String(salt);
                    db.Entry(player).Property("Password").CurrentValue = Convert.ToBase64String(GenerateSaltedHash(Encoding.UTF8.GetBytes(password), salt));
                    db.Add(player);
                    db.SaveChanges();

                    return new Tuple<int, Session>(1, SessionManager.Instance.CreateSession(player));
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return new Tuple<int, Session>(0, null);
        }

        public int Logout(Session session)
        {
            try
            {
                Session realSession = SessionManager.Instance.GetRealSession(session);
                if(realSession != null)
                {
                    SessionManager.Instance.Sessions.Remove(realSession);
                    return 1;
                }
                throw new BadSessionException("Session " + Convert.ToBase64String(session.SessionID) + " doesn't exist!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception on logout: " + e.StackTrace);
            }
            return 0;
        }
    }
}
