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
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, AddUserCommandResult>
    {
        private readonly UserManager<User> _userManager;

        public AddUserCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AddUserCommandResult> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            User user = new User { Email = request.Email, UserName = request.UserName };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                return new AddUserCommandResult() { Email = request.Email, UserName = request.UserName };
            }
            else
            {
                List<string> errors = new List<string>();
                foreach (var error in result.Errors)
                {
                    errors.Add(error.Description);
                }
                return new AddUserCommandResult() { Errors = errors };
            }
        }
    }
}