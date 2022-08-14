using KiravRu.Logic.Domain.Users;
using KiravRu.Logic.Mediator.Queries.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KiravRu.Logic.Mediator.QueryHandlers.Users
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, GetUsersQueryResult>
    {
        private readonly UserManager<User> _userManager;

        public GetUsersQueryHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<GetUsersQueryResult> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var users = _userManager.Users.ToList();

                return new GetUsersQueryResult() { Users = users };
            }
            catch (Exception ex)
            {
                throw new Exception("There are problems in GetUsersQueryHandler", ex);
            }
        }
    }
}