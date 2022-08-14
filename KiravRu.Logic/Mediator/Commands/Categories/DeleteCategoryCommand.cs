using MediatR;

namespace KiravRu.Logic.Mediator.Commands.Categories
{
    public class DeleteCategoryCommand : IRequest<bool>
    {
        public int CategoryId { get; set; }
    }
}
