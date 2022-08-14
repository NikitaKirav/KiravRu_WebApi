using MediatR;

namespace KiravRu.Logic.Mediator.Queries.Categories
{
    public class GetCategoryForEditingQuery : IRequest<GetCategoryForEditingQueryResult>
    {
        public int CategoryId { get; set; }
    }
}
