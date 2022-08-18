using KiravRu.Logic.Domain.Users;
using KiravRu.Logic.Mediator.Queries.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace KiravRu.Logic.Mediator.QueryHandlers.Users
{
    public class GetUserForEditingQueryHandler : IRequestHandler<GetUserForEditingQuery, GetUserForEditingQueryResult>
    {
        private readonly UserManager<User> _userManager;

        public GetUserForEditingQueryHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<GetUserForEditingQueryResult> Handle(GetUserForEditingQuery request, CancellationToken cancellationToken)
        {
            if (request.UserId == "0")
            {
                return new GetUserForEditingQueryResult() { User = new UserInfo() };
            }
            User user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                throw new KeyNotFoundException("System don't have user with Id = " + request.UserId);
            }
            UserInfo model = new UserInfo { Id = user.Id, Email = user.Email, UserName = user.UserName };
            return new GetUserForEditingQueryResult() { User = model };

        }
    }
}