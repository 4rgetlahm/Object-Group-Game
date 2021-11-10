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
        [HttpPost]
        public Tuple<int, Session> Post([FromBody] PlayerAuth playerAuth)
        {
            return Authenticator.GetAuthenticator().Register(playerAuth.Username, playerAuth.Password);
        }
    }
}
