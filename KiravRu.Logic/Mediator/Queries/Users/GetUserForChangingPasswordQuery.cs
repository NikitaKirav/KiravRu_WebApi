using MediatR;

namespace KiravRu.Logic.Mediator.Queries.Users
{
    public class GetUserForChangingPasswordQuery : IRequest<GetUserForChangingPasswordQueryResult>
    {
        public string UserId { get; set; }
    }
}
