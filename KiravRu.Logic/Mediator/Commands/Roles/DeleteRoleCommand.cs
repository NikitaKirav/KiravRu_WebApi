using MediatR;

namespace KiravRu.Logic.Mediator.Commands.Roles
{
    public class DeleteRoleCommand : IRequest<bool>
    {
        public string RoleId { get; set; }
    }
}
