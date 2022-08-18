using KiravRu.Logic.Domain.Users;
using KiravRu.Logic.Mediator.Commands.Roles;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace KiravRu.Logic.Mediator.CommandHandlers.Roles
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, bool>
    {
        private readonly RoleManager<Role> _roleManager;

        public DeleteRoleCommandHandler(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            Role role = await _roleManager.FindByIdAsync(request.RoleId);
            if (role == null)
            {
                throw new KeyNotFoundException("Role don't exist with Id = " + request.RoleId);
            }
            await _roleManager.DeleteAsync(role);

            return true;
        }
    }
}