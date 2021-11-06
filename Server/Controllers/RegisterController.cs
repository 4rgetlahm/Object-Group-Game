using Microsoft.AspNetCore.Mvc;
using GameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterController : Controller
    {
        [HttpPost]
        public Tuple<int, Session> Post(string username, string password)
        {
            return Authenticator.GetAuthenticator().Register(username, password);
        }
    }
}
