using KiravRu.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KiravRu.Models.WebApi
{
    public class UserAccessEditApi
    {
        public UserApi User { get; set; }
        public RolesApi Roles { get; set; }

        public UserAccessEditApi(ChangeRoleViewModel roles)
        {
            User = new UserApi() { UserEmail = roles.UserEmail, UserId = roles.UserId };
            Roles = new RolesApi(roles);            
        }
    }

    public class UpdateAccessApi
    {
        public string UserId { get; set; }
        public List<string> Roles { get; set; }

    }

    public class UserApi
    {
        public string UserId { get; set; }
        public string UserEmail { get; set; }
    }
}
