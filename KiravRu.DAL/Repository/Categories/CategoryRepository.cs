using KiravRu.Logic.Domain.Categories;
using KiravRu.Logic.Interface.Categories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KiravRu.DAL.Repository.Categories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Category>> GetCategoriesAsync(CancellationToken ct)
        {
            return await _dbContext.GetQuery<Category>()
                .Include(c => c.NestingLevel)
                .OrderBy(x => x.OrderItem)
                .ToListAsync(ct);
        }

        public async Task<List<Category>> GetVisibleCategoriesAsync(CancellationToken ct)
        {
            return await _dbContext.GetQuery<Category>()
                .Include(c => c.NestingLevel)
                .Where(x => x.Visible)                
                .ToListAsync(ct);
        }

        public async Task<Category> GetCategoryByIdAsync(int categoryId, CancellationToken ct)
        {
            return await _dbContext.GetQuery<Category>()
                .FirstOrDefaultAsync(x => x.Id == categoryId, ct);
        }

        public async Task<IEnumerable<Category>> OrderAllCategoryAsync(int id, CancellationToken ct)
        {
            var allCategories = await GetCategoriesAsync(ct);
            List<Category> orderCategories = new List<Category>();
            foreach (var categoryLevel1 in allCategories)
            {
                if ((categoryLevel1.NestingLevelId == null) && (categoryLevel1.Id != id))
                {
                    orderCategories.Add(categoryLevel1);
                    foreach (var categoryLevel2 in allCategories)
                    {
                        if ((categoryLevel2.NestingLevelId == categoryLevel1.Id) && (categoryLevel2.Id != id))
                        {
                            categoryLevel2.Name = "-" + categoryLevel2.Name;
                            orderCategories.Add(categoryLevel2);
                            foreach (var categoryLevel3 in allCategories)
                            {
                                if ((categoryLevel3.NestingLevelId == categoryLevel2.Id) && (categoryLevel3.Id != id))
                                {
                                    categoryLevel3.Name = "--" + categoryLevel3.Name;
                                    orderCategories.Add(categoryLevel3);
                                    foreach (var categoryLevel4 in allCategories)
                                    {
                                        if ((categoryLevel4.NestingLevelId == categoryLevel3.Id) && (categoryLevel4.Id != id))
                                        {
                                            categoryLevel4.Name = "---" + categoryLevel4.Name;
                                            orderCategories.Add(categoryLevel4);
                                            foreach (var categoryLevel5 in allCategories)
                                            {
                                                if ((categoryLevel5.NestingLevelId == categoryLevel4.Id) && (categoryLevel5.Id != id))
                                                {
                                                    categoryLevel5.Name = "----" + categoryLevel5.Name;
                                                    orderCategories.Add(categoryLevel5);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
            }
            return orderCategories;
        }
    }
}

