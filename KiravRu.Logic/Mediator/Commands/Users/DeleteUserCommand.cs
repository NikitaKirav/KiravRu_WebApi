using MediatR;

namespace KiravRu.Logic.Mediator.Commands.Users
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public string UserId { get; set; }
    }
}
