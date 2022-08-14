using MediatR;

namespace KiravRu.Logic.Mediator.Commands.Users
{
    public class UpdateUserCommand : IRequest<UpdateUserCommandResult>
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}
