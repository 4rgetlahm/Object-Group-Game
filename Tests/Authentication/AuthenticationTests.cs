using GameLibrary;
using GameLibrary.Database;
using NUnit.Framework;
using Server.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Authentication
{
    class AuthenticationTests
    {
        private IAuthenticator _authenticator;
        private SessionManager sessionManager;
        private string username;
        private string password;
        private Session savedSession;
        
        [OneTimeSetUp]
        public void FirstTimeSetUp()
        {
            _authenticator = new Authenticator();
            username = "testuser";
            password = "password12@c";
            sessionManager = SessionManager.Instance;
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            using(var context = new DataContext())
            {
                Player player = context.Player.Where(p => p.Name == username).FirstOrDefault();
                if(player != null)
                {
                    context.Remove(player);
                    context.SaveChanges();
                }
            }
        }

        [Test, Order(1)]
        public void Register_User()
        {
            Tuple<int, Session> result = _authenticator.Register(username, password, CharacterType.MODEL_MALE_1);
            savedSession = result.Item2;
            Assert.Multiple(() =>
            {
                Assert.AreEqual(result.Item1, 1);
                Assert.NotNull(result.Item2);
            });
        }

        [Test, Order(2)]
        public void Check_Session_Exists()
        {
            Session session = sessionManager.GetRealSession(savedSession);
            Console.WriteLine(Convert.ToBase64String(session.SessionID));
            Assert.NotNull(session);
        }

        [Test, Order(3)]
        public void Logout_Registered_User()
        {
            _authenticator.Logout(savedSession);
            Assert.Null(sessionManager.GetRealSession(savedSession));
        }

        [Test, Order(4)]
        public void Login_User()
        {
            Tuple<int, Session> result = _authenticator.Login(username, password);
            savedSession = result.Item2;
            Assert.Multiple(() =>
            {
                Assert.AreEqual(result.Item1, 1);
                Assert.NotNull(result.Item2);
            });
        }

        [Test, Order(5)]
        public void Check_Automated_Logout()
        {
            sessionManager.LastRequest[sessionManager.GetRealSession(savedSession)] = sessionManager.LastRequest[sessionManager.GetRealSession(savedSession)].AddDays(-1);
            sessionManager.SessionHandler.ExpireSessions();
            Assert.Null(sessionManager.GetRealSession(savedSession));
        }

    }
}
