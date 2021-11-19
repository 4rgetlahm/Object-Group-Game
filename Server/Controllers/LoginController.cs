using Microsoft.AspNetCore.Mvc;
using GameLibrary;
using GameLibrary.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Authentication;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticator _authenticator;
        public LoginController(IAuthenticator authenticator)
        {
            _authenticator = authenticator;
        }

        [HttpPost]
        public Tuple<int, Session> Post([FromBody] PlayerAuth playerAuth)
        {
            return _authenticator.Login(playerAuth.Username, playerAuth.Password);
        }
    }
}
