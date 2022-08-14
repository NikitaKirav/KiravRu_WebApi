using MediatR;

namespace KiravRu.Logic.Mediator.Commands.Users
{
    public class AddUserCommand : IRequest<AddUserCommandResult>
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
