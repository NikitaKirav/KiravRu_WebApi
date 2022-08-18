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
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdateUserCommandResult>
    {
        private readonly UserManager<User> _userManager;

        public UpdateUserCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UpdateUserCommandResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            User user = await _userManager.FindByIdAsync(request.Id);
            if (user == null)
            {
                throw new KeyNotFoundException("System don't have user with Id = " + request.Id);
            }
            user.Email = request.Email;
            user.UserName = request.UserName;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new UpdateUserCommandResult() { Email = request.Email, UserName = request.UserName };
            }
            else
            {
                List<string> errors = new List<string>();
                foreach (var errorText in result.Errors)
                {
                    errors.Add(errorText.Description);
                }
                return new UpdateUserCommandResult() { Errors = errors };
            }
        }
    }
}
/*
User user = await _userManager.FindByIdAsync(model.Id);
if (user != null)
{
    user.Email = model.Email;
    user.UserName = model.UserName;

    var result = await _userManager.UpdateAsync(user);
    if (result.Succeeded)
    {
        return Ok(model);
    }
    else
    {
        List<string> errors = new List<string>();
        foreach (var errorText in result.Errors)
        {
            errors.Add(errorText.Description);
        }
        return Ok(new { errors = errors });
    }
}*/