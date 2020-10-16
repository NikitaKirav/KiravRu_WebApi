using KiravRu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KiravRu.Interfaces
{
    public interface IArticlesCategory
    {
        IEnumerable<Category> AllCategories { get; }

        IEnumerable<Category> GetVisibleCategories { get; }
        Category GetObjectCategory(int categoryId);

        Category AddCategory(Category category);

        bool UpdateCategories(Category category);

        void DeleteCategory(Category category);

        IEnumerable<Category> OrderAllCategory(int id = 0);
    }
}
