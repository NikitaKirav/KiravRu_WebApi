using MediatR;

namespace KiravRu.Logic.Mediator.Queries.Users
{
    public class GetUserForEditingQuery : IRequest<GetUserForEditingQueryResult>
    {
        public string UserId { get; set; }
    }
}
