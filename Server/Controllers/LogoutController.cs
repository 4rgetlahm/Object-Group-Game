using Microsoft.AspNetCore.Mvc;
using Server.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogoutController : ControllerBase
    {
        private readonly IAuthenticator _authenticator;
        public LogoutController(IAuthenticator authenticator)
        {
            _authenticator = authenticator;
        }
        [HttpPost]
        public IActionResult Post([FromBody] Session body)
        {
            if (body == null)
            {
                return BadRequest();
            }
            if(_authenticator.Logout(body) == 1){
                return Ok();
            }


            return BadRequest();
        }
    }
}
