using KiravRu.Logic.Domain.Users;
using KiravRu.Logic.Mediator.Commands.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace KiravRu.Logic.Mediator.CommandHandlers.Users
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly UserManager<User> _userManager;

        public DeleteUserCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            User user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                throw new KeyNotFoundException("System don't have user with Id = " + request.UserId);
            }
            await _userManager.DeleteAsync(user);

            return true;
        }
    }
}