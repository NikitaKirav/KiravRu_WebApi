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
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ChangePasswordCommandResult>
    {
        private readonly UserManager<User> _userManager;

        public ChangePasswordCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ChangePasswordCommandResult> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            User user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                throw new KeyNotFoundException("System don't have user with Id = " + request.UserId);
            }
            IdentityResult result =
                    await request.PasswordValidator.ValidateAsync(_userManager, user, request.NewPassword);
            if (result.Succeeded)
            {
                user.PasswordHash = request.PasswordHasher.HashPassword(user, request.NewPassword);
                await _userManager.UpdateAsync(user);
                var responce = new UserResult { Id = user.Id, Email = user.Email };
                return new ChangePasswordCommandResult { User = responce };
            }
            else
            {
                List<string> errors = new List<string>();
                foreach (var errorText in result.Errors)
                {
                    errors.Add(errorText.Description);
                }
                return new ChangePasswordCommandResult { Errors = errors };
            }
        }
    }
}