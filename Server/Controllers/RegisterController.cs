using Microsoft.AspNetCore.Mvc;
using GameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Authentication;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterController : Controller
    {
        private readonly IAuthenticator _authenticator;
        public RegisterController(IAuthenticator authenticator)
        {
            _authenticator = authenticator;
        }

        [HttpPost]
        public Tuple<int, Session> Post([FromBody] PlayerAuth playerAuth)
        {
            return _authenticator.Register(playerAuth.Username, playerAuth.Password);
        }
    }
}
