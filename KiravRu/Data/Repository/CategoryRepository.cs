using KiravRu.Interfaces;
using KiravRu.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KiravRu.Data.Repository
{
    public class CategoryRepository : IArticlesCategory
    {
        private readonly ApplicationContext _db;

        public CategoryRepository(ApplicationContext db)
        {
            _db = db;
        }
        public IEnumerable<Category> AllCategories => _db.Categories.Include(c => c.NestingLevel).OrderBy(x => x.OrderItem);
        public IEnumerable<Category> GetVisibleCategories => _db.Categories.Where(x => x.Visible).Include(c => c.NestingLevel);
        public Category GetObjectCategory(int categoryId) => _db.Categories.FirstOrDefault(x => x.Id == categoryId);

        public Category AddCategory(Category category)
        {
            _db.Categories.Add(category);
            _db.SaveChanges();
            return category;
        }

        public bool UpdateCategories(Category category)
        {
            _db.Entry(category).State = EntityState.Modified;
            _db.SaveChanges();
            var result = _db.SaveChanges();
            return result > 0 ? true : false;
        }

        public void DeleteCategory(Category category)
        {
            _db.Entry(category).State = EntityState.Deleted;
            _db.SaveChanges();
        }

        public IEnumerable<Category> OrderAllCategory(int id)
        {
            var allCategories = AllCategories.ToList();
            List<Category> orderCategories = new List<Category>();
            foreach (var categoryLevel1 in allCategories)
            {
                if ((categoryLevel1.NestingLevelId == null) && (categoryLevel1.Id != id))
                {
                    orderCategories.Add(categoryLevel1);
                    foreach (var categoryLevel2 in allCategories)
                    {
                        if ((categoryLevel2.NestingLevelId == categoryLevel1.Id)&& (categoryLevel2.Id != id))
                        {
                            categoryLevel2.Name = "-" + categoryLevel2.Name;
                            orderCategories.Add(categoryLevel2);
                            foreach (var categoryLevel3 in allCategories)
                            {
                                if ((categoryLevel3.NestingLevelId == categoryLevel2.Id)&& (categoryLevel3.Id != id))
                                {
                                    categoryLevel3.Name = "--" + categoryLevel3.Name;
                                    orderCategories.Add(categoryLevel3);
                                    foreach (var categoryLevel4 in allCategories)
                                    {
                                        if ((categoryLevel4.NestingLevelId == categoryLevel3.Id)&& (categoryLevel4.Id != id))
                                        {
                                            categoryLevel4.Name = "---" + categoryLevel4.Name;
                                            orderCategories.Add(categoryLevel4);
                                            foreach (var categoryLevel5 in allCategories)
                                            {
                                                if ((categoryLevel5.NestingLevelId == categoryLevel4.Id)&& (categoryLevel5.Id != id))
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
