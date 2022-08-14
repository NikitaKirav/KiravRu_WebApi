using MediatR;


namespace KiravRu.Logic.Mediator.Commands.Roles
{
    public class AddRoleCommand : IRequest<AddRoleCommandResult>
    {
        public string Name { get; set; }
    }
}
