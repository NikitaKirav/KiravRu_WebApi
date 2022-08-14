using System.Collections.Generic;


namespace KiravRu.Logic.Mediator.Commands.Users
{
    public class UpdateUserCommandResult
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public List<string> Errors { get; set; } = null;
    }
}
