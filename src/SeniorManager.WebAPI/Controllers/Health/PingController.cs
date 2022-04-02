using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeniorManager.WebAPI.Controllers.Health
{
    [Route("api/[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public IActionResult Ping() => Ok("Pong");


        [Authorize]
        [HttpGet("Autorizado")]
        public async Task<IActionResult> Autorizado()
        {
            var token = await HttpContext.GetTokenAsync("Bearer", "access_token");

            return Ok(new
            {
                Name = User.Identity.Name,
                IsAuthenticated = User.Identity.IsAuthenticated,
                token,
                //Claims =  User.Claims,
            });
        }
    }
}
