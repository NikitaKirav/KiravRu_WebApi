using KiravRu.Logic.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;


namespace KiravRu.Logic.Mediator.Commands.Users
{
    public class ChangePasswordCommand : IRequest<ChangePasswordCommandResult>
    {
        public IPasswordValidator<User> PasswordValidator { get; set; }
        public IPasswordHasher<User> PasswordHasher { get; set; }
        public string UserId { get; set; }
        public string NewPassword { get; set; }
    }
}
