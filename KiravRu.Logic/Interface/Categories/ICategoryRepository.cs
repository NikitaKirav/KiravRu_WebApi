using KiravRu.Logic.Domain.Categories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace KiravRu.Logic.Interface.Categories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<List<Category>> GetCategoriesAsync(CancellationToken ct);
        Task<List<Category>> GetVisibleCategoriesAsync(CancellationToken ct);
        Task<Category> GetCategoryByIdAsync(int categoryId, CancellationToken ct);

        Task<IEnumerable<Category>> OrderAllCategoryAsync(int id, CancellationToken ct);
    }
}
