using System.Collections.Generic;

namespace KiravRu.Logic.Mediator.Commands.Users
{
    public class ChangePasswordCommandResult
    {
        public UserResult User { get; set; }
        public List<string> Errors { get; set; } = null;
    }

    public class UserResult
    {
        public string Email { get; set; }
        public string Id { get; set; }
    }
}
