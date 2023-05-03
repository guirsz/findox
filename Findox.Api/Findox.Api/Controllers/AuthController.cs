using Findox.Api.Domain.Interfaces;
using Findox.Api.Domain.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Findox.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AuthRequest request, [FromServices] IAuthService services)
        {
            if (request == null)
            {
                return BadRequest();
            }

            try
            {
                var result = await services.Authenticate(request);

                if (result != null)
                {
                    return Ok(result);
                }

                return BadRequest("Wrong email or password.");
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
