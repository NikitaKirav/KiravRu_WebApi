using MediatR;

namespace KiravRu.Logic.Mediator.Commands.Categories
{
    public class SetCategoryCommand : IRequest<SetCategoryCommandResult>
    {
        public int Id { get; set; }
        public int? OrderItem { get; set; } 
        public int? NestingLevelId { get; set; } 
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string ImageText { get; set; }
        public string Description { get; set; }
        public bool Visible { get; set; }
    }
}
