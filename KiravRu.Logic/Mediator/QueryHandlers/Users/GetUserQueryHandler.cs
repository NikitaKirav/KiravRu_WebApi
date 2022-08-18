using KiravRu.Logic.Interface.Users;
using KiravRu.Logic.Mediator.Dto;
using KiravRu.Logic.Mediator.Queries.Users;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KiravRu.Logic.Mediator.QueryHandlers.Users
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUserQueryResult>
    {
        private readonly IUserRepository _userRepository;

        public GetUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetUserQueryResult> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(request.UserId, cancellationToken);
            var result = new GetUserQueryResult
            {
                Id = user.Id,
                UserName = user.UserName,
                Roles = user.UserRoles.ConvertAll(r => new IdentityModel<string>
                {
                    Id = r.RoleId,
                    Name = r.Role.Name
                })
            };

            return result;
        }
    }
}
