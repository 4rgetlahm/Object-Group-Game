using GameLibrary;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Server.Authentication;
using Server.Logging;
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
                SessionManager.Instance.UpdateLastRequest(body, DateTime.Now);
            } catch (BadSessionException e){
                Logger.Log("Session doesn't exist! SessionID: " + Convert.ToBase64String(body.SessionID) 
                    + "\nException: " + e.StackTrace);
                return BadRequest();
            } 
            catch (Exception e){
                Logger.Log("Caught exception while updating state, sessionID: " + Convert.ToBase64String(body.SessionID) 
                    + "\nException: " + e.StackTrace);
                return BadRequest();
            }
            return Ok();
        }
    }
}
