using Findox.Api.Domain.Interfaces;
using Findox.Api.Domain.Requests;
using Findox.Api.Domain.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Findox.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Admin")]
    public class UserController : AuthenticatedUserControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int limit = 20, [FromQuery] int offset = 0)
        {
            IEnumerable<UserResponse> result = await userService.GetAll(limit, offset);
            return Ok(result);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetById(int id)
        {
            UserResponse? result = await userService.GetById(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserRequest request)
        {
            (int userId, string message) = await userService.Create(request, RequestedBy());

            if (userId == 0)
                return ValidationProblem(message);

            return Ok(new { userId, message });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserRequest request)
        {
            (int userId, string message) = await userService.Update(id, request, RequestedBy());

            if (userId == 0)
                return ValidationProblem(message);

            return Ok(new { userId, message });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            (bool deleted, string message) = await userService.Delete(id, RequestedBy());

            if (deleted == false)
                return ValidationProblem(message);

            return Ok();
        }
    }
}
