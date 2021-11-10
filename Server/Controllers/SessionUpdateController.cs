using GameLibrary;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Server.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [ApiController]
    [Route("session/update")]
    public class SessionUpdateController : Controller
    {
        [HttpPost]
        public IActionResult Post([FromBody] Session body)
        {
            try{
                SessionManager.GetInstance().UpdateLastRequest(body, DateTime.Now);
            } catch (BadSessionException e){
                Console.WriteLine("Session doesn't exist! SessionID: " + Convert.ToBase64String(body.SessionID));
                return BadRequest();
            } 
            catch (Exception e){
                Console.WriteLine("Caught exception while updating state, sessionID: " + Convert.ToBase64String(body.SessionID));
                return BadRequest();
            }
            return Ok();
        }
    }
}
