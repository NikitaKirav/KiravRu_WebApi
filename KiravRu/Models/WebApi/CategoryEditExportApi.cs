using KiravRu.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KiravRu.Models.WebApi
{
    public class CategoryEditExportApi
    {
        public CategoryEditApi Category { get; set; }
        public List<CategoryApi> Categories { get; set; }

        public CategoryEditExportApi()
        {  }

        public CategoryEditExportApi(Category category, IEnumerable<Category> categories)
        {
            Category = new CategoryEditApi(category);
            Categories = new List<CategoryApi>();

            foreach (var categoryItem in categories)
            {
                Categories.Add(new CategoryApi(categoryItem));
            }
        }
    }

    public class CategoryEditImportApi
    {
        public CategoryEditApi Category { get; set; }
    }

    public class CategoryEditApi
    {
        public int Id { get; set; }
        public int? OrderItem { get; set; } // Порядковый номер (при необходимости)
        public int? NestingLevelId { get; set; } // Уровень вложенности (Id номер объекта в котором размещен этот объект)
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string ImageText { get; set; }
        public string Description { get; set; }
        public bool Visible { get; set; }

        public CategoryEditApi() { }

        public CategoryEditApi(Category category)
        {
            Id = category.Id;
            OrderItem = category.OrderItem;
            NestingLevelId = category.NestingLevelId;
            Name = category.Name;
            ImagePath = category.ImagePath;
            ImageText = category.ImageText;
            Description = category.Description;
            Visible = category.Visible;
        }

        static public Category ConvertToCategory(CategoryEditApi category)
        {
            return new Category()
            {
                Id = category.Id,
                OrderItem = category.OrderItem,
                NestingLevelId = category.NestingLevelId,
                Name = category.Name,
                ImagePath = category.ImagePath,
                ImageText = category.ImageText,
                Description = category.Description,
                Visible = category.Visible
            };
        }
    }
}
