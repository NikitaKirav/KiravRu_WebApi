using KiravRu.Logic.Domain.Users;
using KiravRu.Logic.Mediator.Queries.Roles;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KiravRu.Logic.Mediator.QueryHandlers.Roles
{
    public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, GetRolesQueryResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public GetRolesQueryHandler(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<GetRolesQueryResult> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var result = _roleManager.Roles.ToList();
            return new GetRolesQueryResult() { Roles = result };
        }
    }
}