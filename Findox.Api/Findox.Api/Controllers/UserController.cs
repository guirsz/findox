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
        public async Task<IActionResult> GetAllPaginated([FromQuery] UserGetAllPaginatedRequest query)
        {
            IEnumerable<UserResponse> result = await userService.GetAllPaginatedAsync(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            UserResponse? result = await userService.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserCreateRequest request)
        {
            (int userId, string message) = await userService.CreateAsync(request, RequestedBy());

            if (userId == 0)
                return ValidationProblem(message);

            return Ok(new { userId, message });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserUpdateRequest request)
        {
            (int userId, string message) = await userService.UpdateAsync(id, request, RequestedBy());

            if (userId == 0)
                return ValidationProblem(message);

            return Ok(new { userId, message });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            (bool deleted, string message) = await userService.DeleteAsync(id, RequestedBy());

            if (deleted == false)
                return ValidationProblem(message);

            return Ok();
        }
    }
}
