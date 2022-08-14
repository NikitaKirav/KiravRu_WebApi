using System.Collections.Generic;

namespace KiravRu.Logic.Mediator.Commands.Users
{
    public class AddUserCommandResult
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public List<string> Errors { get; set; } = null;
    }
}
