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
    public class GetUserForChangingPasswordQueryHandler : IRequestHandler<GetUserForChangingPasswordQuery, GetUserForChangingPasswordQueryResult>
    {
        private readonly UserManager<User> _userManager;

        public GetUserForChangingPasswordQueryHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<GetUserForChangingPasswordQueryResult> Handle(GetUserForChangingPasswordQuery request, CancellationToken cancellationToken)
        {
            User user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                throw new KeyNotFoundException("System don't have user with Id = " + request.UserId);
            }
            UserInfo model = new UserInfo { Id = user.Id, Email = user.Email };
            return new GetUserForChangingPasswordQueryResult { User = model };
        }
    }
}
