using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace KiravRu.Logic.Domain.Users
{
    public class Role : IdentityRole
    {
        public Role(string roleName) : base(roleName)
        {
        }
        public Role()
        {
        }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
