using MediatR;


namespace KiravRu.Logic.Mediator.Queries.Users
{
    public class GetUserQuery : IRequest<GetUserQueryResult>
    {
        public GetUserQuery(string userId)
        {
            UserId = userId;
        }
        public string UserId { get; }
    }
}
