using System.Threading;
using System.Threading.Tasks;
using KiravRu.Controllers.v1.Model;
using KiravRu.Logic.Mediator.Commands.Roles;
using KiravRu.Logic.Mediator.Queries.Roles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KiravRu.Controllers.v1
{
    [ApiVersion("1.0")]
    //[Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/roles")]
    [ApiController]
    public class RolesApiController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RolesApiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetRoles(CancellationToken ct)
        {
            var query = new GetRolesQuery();

            var result = await _mediator.Send(query, ct);

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateRole(CreateRoleRequestModel role, CancellationToken ct)
        {
            var command = new AddRoleCommand()
            {
                Name = role.Name
            };

            var result = await _mediator.Send(command, ct);

            return Ok(result);
        }

        [HttpDelete]
        [Authorize(Roles = "admin")]
        [Route("{id}")]
        public async Task<IActionResult> Delete(string id, CancellationToken ct)
        {
            var command = new DeleteRoleCommand()
            {
                RoleId = id
            };

            await _mediator.Send(command, ct);

            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("{userId}")]
        public async Task<IActionResult> GetRoleForEditing(string userId, CancellationToken ct)
        {
            var query = new GetRoleForEditingQuery()
            {
                UserId = userId
            };

            var result = await _mediator.Send(query, ct);

            return Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateAccess(UpdateAccessRequestModel request, CancellationToken ct)
        {
            var command = new UpdateAccessCommand()
            {
                UserId = request.UserId,
                Roles = request.Roles
            };

            var result = await _mediator.Send(command, ct);

            return Ok(result);
        }
    }
}