using KiravRu.Logic.Domain.Categories;
using System.Collections.Generic;

namespace KiravRu.Logic.Mediator.Queries.Categories
{
    public class GetCategoryForEditingQueryResult
    {
        public CategoryForEditing Category { get; set; }
        public List<ConciseCategory> Categories { get; set; }

        public GetCategoryForEditingQueryResult()
        { }

        public GetCategoryForEditingQueryResult(Category category, IEnumerable<Category> categories)
        {
            Category = new CategoryForEditing(category);
            Categories = new List<ConciseCategory>();

            foreach (var categoryItem in categories)
            {
                Categories.Add(new ConciseCategory(categoryItem));
            }
        }
    }

    public class ConciseCategory
    {
        public int Value { get; set; }
        public int? OrderItem { get; set; } // Порядковый номер (при необходимости)
        public int? NestingLevelId { get; set; } // Уровень вложенности (Id номер объекта в котором размещен этот объект)
        public string Label { get; set; }

        public ConciseCategory(Category category)
        {
            Value = category.Id;
            OrderItem = category.OrderItem;
            NestingLevelId = category.NestingLevelId;
            Label = category.Name;
        }
    }

    public class CategoryForEditing
    {
        public int Id { get; set; }
        public int? OrderItem { get; set; } // Порядковый номер (при необходимости)
        public int? NestingLevelId { get; set; } // Уровень вложенности (Id номер объекта в котором размещен этот объект)
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string ImageText { get; set; }
        public string Description { get; set; }
        public bool Visible { get; set; }

        public CategoryForEditing() { }

        public CategoryForEditing(Category category)
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

        static public Category ConvertToCategory(CategoryForEditing category)
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
