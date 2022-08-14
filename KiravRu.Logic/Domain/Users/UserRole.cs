using Microsoft.AspNetCore.Identity;

namespace KiravRu.Logic.Domain.Users
{
    public class UserRole : IdentityUserRole<string>
    {
        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}
