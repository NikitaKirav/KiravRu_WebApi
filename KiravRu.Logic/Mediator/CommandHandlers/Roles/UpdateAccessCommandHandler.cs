using KiravRu.Logic.Domain.Users;
using KiravRu.Logic.Mediator.Commands.Roles;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KiravRu.Logic.Mediator.CommandHandlers.Roles
{
    public class UpdateAccessCommandHandler : IRequestHandler<UpdateAccessCommand, bool>
    {
        private readonly UserManager<User> _userManager;

        public UpdateAccessCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<bool> Handle(UpdateAccessCommand request, CancellationToken cancellationToken)
        {
            try
            {
                User user = await _userManager.FindByIdAsync(request.UserId);
                if (user == null)
                {
                    throw new Exception("User don't exist with Id = " + request.UserId);
                }
                var userRoles = await _userManager.GetRolesAsync(user);
                var addedRoles = request.Roles.Except(userRoles);
                var removedRoles = userRoles.Except(request.Roles);

                await _userManager.AddToRolesAsync(user, addedRoles);

                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("There is a problem in UpdateAccessCommandHandler", ex);
            }
        }
    }
}