using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace KiravRu.Logic.Domain.Users
{
    public class User : IdentityUser
    {
        public virtual List<UserRole> UserRoles { get; set; }
    }

}
