using KiravRu.Logic.Domain.Users;
using KiravRu.Logic.Mediator.Commands.Roles;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace KiravRu.Logic.Mediator.CommandHandlers.Roles
{
    public class AddRoleCommandHandler : IRequestHandler<AddRoleCommand, AddRoleCommandResult>
    {
        private readonly RoleManager<Role> _roleManager;

        public AddRoleCommandHandler(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }
                public async Task<AddRoleCommandResult> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Name))
                {
                    throw new Exception("Role name can't be empty.");
                }

                IdentityResult result = await _roleManager.CreateAsync(new Role(request.Name));
                if (result.Succeeded)
                {
                    return new AddRoleCommandResult { Role = new RoleResult { Name = request.Name } };
                }
                else
                {
                    List<string> errors = new List<string>();
                    foreach (var errorText in result.Errors)
                    {
                        errors.Add(errorText.Description);
                    }
                    return new AddRoleCommandResult { Errors = errors };
                }

            }
            catch (Exception ex)
            {
                throw new Exception("There is a problem in AddRoleCommandHandler", ex);
            }
        }
    }
}