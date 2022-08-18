using KiravRu.Logic.Domain.Users;
using KiravRu.Logic.Mediator.Queries.Roles;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KiravRu.Logic.Mediator.QueryHandlers.Roles
{
    public class GetRoleForEditingQueryHandler : IRequestHandler<GetRoleForEditingQuery, GetRoleForEditingQueryResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public GetRoleForEditingQueryHandler(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<GetRoleForEditingQueryResult> Handle(GetRoleForEditingQuery request, CancellationToken cancellationToken)
        {
            User user = await _userManager.FindByIdAsync(request.UserId);
            if(user == null)
            {
                throw new KeyNotFoundException("User dosn't exist with Id = " + request.UserId);
            }
            var userRoles = await _userManager.GetRolesAsync(user);
            var allRoles = _roleManager.Roles.ToList();
            UserRolesModel model = new UserRolesModel
            {
                UserId = user.Id,
                UserEmail = user.Email,
                UserRoles = userRoles,
                AllRoles = allRoles
            };
            return new GetRoleForEditingQueryResult { UserAccess = new UserAccessResult(model) };
        }
    }
}