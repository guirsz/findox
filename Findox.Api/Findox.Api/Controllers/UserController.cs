using Findox.Api.Domain.Interfaces.Services.User;
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
        [HttpGet]
        public async Task<IActionResult> GetAllPaginated([FromQuery] UserGetAllPaginatedRequest query,
            [FromServices] IUserGetAllPaginatedService service)
        {
            IEnumerable<UserResponse> result = await service.RunAsync(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id,
            [FromServices] IUserGetByIdService service)
        {
            UserResponse result = await service.RunAsync(id);

            if (result == null || result.UserId == 0)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserCreateRequest request,
            [FromServices] IUserCreateService service)
        {
            (int userId, string message) = await service.RunAsync(request, RequestedBy());

            if (userId == 0)
                return ValidationProblem(message);

            return Ok(new { userId, message });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserUpdateRequest request,
            [FromServices] IUserUpdateService service)
        {
            (int userId, string message) = await service.RunAsync(id, request, RequestedBy());

            if (userId == 0)
                return ValidationProblem(message);

            return Ok(new { userId, message });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id,
            [FromServices] IUserDeleteService service)
        {
            (bool deleted, string message) = await service.RunAsync(id, RequestedBy());

            if (deleted == false)
                return ValidationProblem(message);

            return Ok();
        }
    }
}
