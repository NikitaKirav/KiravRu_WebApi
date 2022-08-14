using System.Collections.Generic;

namespace KiravRu.Logic.Mediator.Commands.Roles
{
    public class AddRoleCommandResult
    {
        public RoleResult Role { get; set; }
        public List<string> Errors { get; set; } = null;
    }

    public class RoleResult
    {
        public string Name { get; set; }
    }
}
