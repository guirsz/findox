using Findox.Api.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Findox.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InitController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> InitializeDatabase([FromServices] IInitDatabaseService service)
        {
            await service.RunAsync();
            return Ok();
        }
    }
}
