using KiravRu.Logic.Domain.Categories;
using System.Collections.Generic;

namespace KiravRu.Logic.Mediator.Queries.Categories
{
    public class GetCategoryListQueryResult
    {
        public List<Category> Categories { get; set; }
        public int TotalCategoriesCount { get; set; }
    }
}

