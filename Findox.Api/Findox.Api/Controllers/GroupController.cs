using Findox.Api.Domain.Interfaces.Services.Group;
using Findox.Api.Domain.Requests;
using Findox.Api.Domain.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Findox.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : AuthenticatedUserControllerBase
    {
        [Authorize("RegularUser")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] IGroupGetAllService service)
        {
            IEnumerable<GroupResponse> result = await service.RunAsync();
            return Ok(result);
        }

        [Authorize("RegularUser")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id,
            [FromServices] IGroupGetByIdService service)
        {
            GroupResponse? result = await service.RunAsync(id);

            if (result == null || result?.GroupId == 0)
                return NotFound();

            return Ok(result);
        }

        [Authorize("Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GroupCreateRequest request,
            [FromServices] IGroupCreateService service)
        {
            (int groupId, string message) = await service.RunAsync(request, RequestedBy());

            if (groupId == 0)
                return ValidationProblem(message);

            return Ok(new { groupId, message });
        }

        [Authorize("Admin")]
        [HttpPut()]
        public async Task<IActionResult> Update([FromBody] GroupUpdateRequest request,
            [FromServices] IGroupUpdateService service)
        {
            (int groupId, string message) = await service.RunAsync(request, RequestedBy());

            if (groupId == 0)
                return ValidationProblem(message);

            return Ok(new { groupId, message });
        }

        [Authorize("Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id,
            [FromServices] IGroupDeleteService service)
        {
            (bool deleted, string message) = await service.RunAsync(id, RequestedBy());

            if (deleted == false)
                return ValidationProblem(message);

            return Ok();
        }
    }
}
