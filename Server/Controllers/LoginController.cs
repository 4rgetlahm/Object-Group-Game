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
        public Tuple<int, Session> Post([FromBody] PlayerAuth playerAuth)
        {
            return Authenticator.Instance.Login(playerAuth.Username, playerAuth.Password);
        }
    }
}
