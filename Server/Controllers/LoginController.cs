using Microsoft.AspNetCore.Mvc;
using GameLibrary;
using GameLibrary.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {

        [HttpPost]
        public Tuple<int, Session> Post(string username, string password)
        {
            return Authenticator.GetAuthenticator().Login(username, password);
        }
    }
}
