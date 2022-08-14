using System.Threading;
using System.Threading.Tasks;
using KiravRu.Controllers.v1.Model;
using KiravRu.Logic.Domain.Users;
using KiravRu.Logic.Mediator.Commands.Users;
using KiravRu.Logic.Mediator.Queries.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KiravRu.Controllers.v1
{
    [ApiVersion("1.0")]
    //[Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/users")]
    [ApiController]
    public class UsersApiController : ControllerBase
    {

        private readonly IMediator _mediator;

        public UsersApiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetUsers(CancellationToken ct)
        {
            var query = new GetUsersQuery();

            var result = await _mediator.Send(query, ct);

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create(CreateUserRequestModel model, CancellationToken ct)
        {
            var command = new AddUserCommand()
            {
                Email = model.Email,
                UserName = model.UserName,
                Password = model.Password
            };

            var result = await _mediator.Send(command, ct);

            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("{id}")]
        public async Task<IActionResult> GetUserForEditing(string id, CancellationToken ct)
        {
            var query = new GetUserForEditingQuery()
            {
                UserId = id
            };

            var result = await _mediator.Send(query, ct);

            return Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateUser(UpdateUserRequestModel model, CancellationToken ct)
        {
            var command = new UpdateUserCommand()
            {
                Id = model.Id,
                Email = model.Email,
                UserName = model.UserName
            };

            var result = await _mediator.Send(command, ct);

            return Ok(result);
        }

        [HttpDelete]
        [Authorize(Roles = "admin")]
        [Route("{id}")]
        public async Task<ActionResult> Delete(string id, CancellationToken ct)
        {
            var command = new DeleteUserCommand()
            {
                UserId = id
            };

            await _mediator.Send(command, ct);

            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("changePassword/{id}")]
        public async Task<IActionResult> GetUserForChangingPassword(string id, CancellationToken ct)
        {
            var query = new GetUserForChangingPasswordQuery()
            {
                UserId = id
            };

            var result = await _mediator.Send(query, ct);

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [Route("changePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequestModel model, CancellationToken ct)
        {
            var _passwordValidator =
                HttpContext.RequestServices.GetService(typeof(IPasswordValidator<User>)) as IPasswordValidator<User>;
            var _passwordHasher =
                HttpContext.RequestServices.GetService(typeof(IPasswordHasher<User>)) as IPasswordHasher<User>;

            var command = new ChangePasswordCommand()
            {
                PasswordValidator = _passwordValidator,
                PasswordHasher = _passwordHasher,
                UserId = model.Id,
                NewPassword = model.NewPassword
            };

            var result = await _mediator.Send(command, ct);

            return Ok(result);
        }
    }
}