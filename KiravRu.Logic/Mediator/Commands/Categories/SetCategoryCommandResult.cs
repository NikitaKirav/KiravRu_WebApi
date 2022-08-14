using KiravRu.Logic.Domain.Categories;
using KiravRu.Logic.Mediator.Queries.Categories;
using System.Collections.Generic;

namespace KiravRu.Logic.Mediator.Commands.Categories
{
    public class SetCategoryCommandResult
    {
        public CategoryForEditing Category { get; set; }
        public List<ConciseCategory> Categories { get; set; }

        public SetCategoryCommandResult()
        { }

        public SetCategoryCommandResult(Category category, IEnumerable<Category> categories)
        {
            Category = new CategoryForEditing(category);
            Categories = new List<ConciseCategory>();

            foreach (var categoryItem in categories)
            {
                Categories.Add(new ConciseCategory(categoryItem));
            }
        }
    }
}
