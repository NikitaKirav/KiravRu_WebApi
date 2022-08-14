using KiravRu.Logic.Domain.Users;
using System.Collections.Generic;

namespace KiravRu.Logic.Mediator.Queries.Roles
{
    public class GetRoleForEditingQueryResult
    {
        public UserAccessResult UserAccess { get; set; }
    }

    public class UserAccessResult
    {
        public UserResult User { get; set; }
        public RolesResult Roles { get; set; }
        public UserAccessResult(UserRolesModel roles)
        {
            User = new UserResult() { UserEmail = roles.UserEmail, UserId = roles.UserId };
            Roles = new RolesResult(roles);
        }
    }

    public class UserResult
    {
        public string UserId { get; set; }
        public string UserEmail { get; set; }
    }

    public class RolesResult
    {
        public List<RoleResult> AllRoles { get; set; }
        public List<string> UserRoles { get; set; }

        public RolesResult(UserRolesModel roles)
        {
            AllRoles = new List<RoleResult>();
            UserRoles = new List<string>();

            foreach (var role in roles.AllRoles)
            {
                AllRoles.Add(new RoleResult(role.Id, role.Name));
            }
            foreach (var roleName in roles.UserRoles)
            {
                UserRoles.Add(roleName);
            }
        }

        public RolesResult() { }
    }

    public class RoleResult
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public RoleResult(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public RoleResult() { }
    }

    public class UserRolesModel
    {
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public List<Role> AllRoles { get; set; }
        public IList<string> UserRoles { get; set; }
        public UserRolesModel()
        {
            AllRoles = new List<Role>();
            UserRoles = new List<string>();
        }
    }
}
