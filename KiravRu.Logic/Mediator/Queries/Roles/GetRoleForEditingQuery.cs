using MediatR;


namespace KiravRu.Logic.Mediator.Queries.Roles
{
    public class GetRoleForEditingQuery : IRequest<GetRoleForEditingQueryResult>
    {
        public string UserId { get; set; }
    }
}
