using Microsoft.AspNetCore.Mvc;
using GameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Authentication;

namespace Server.Controllers
{

    public class PlayerRegisterRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int CharacterType { get; set; }
    }

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
        public Tuple<int, Session> Post([FromBody] PlayerRegisterRequest playerRegisterRequest)
        {
            return _authenticator.Register(playerRegisterRequest.Username, playerRegisterRequest.Password, (CharacterType) playerRegisterRequest.CharacterType);
        }
    }
}
