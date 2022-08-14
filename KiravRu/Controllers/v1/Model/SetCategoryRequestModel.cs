
namespace KiravRu.Controllers.v1.Model
{
    public class SetCategoryRequestModel
    {
        public CategoryRequestModel Category { get; set; }
    }

    public class CategoryRequestModel
    {
        public int Id { get; set; }
        public int? OrderItem { get; set; } // Порядковый номер (при необходимости)
        public int? NestingLevelId { get; set; } // Уровень вложенности (Id номер объекта в котором размещен этот объект)
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string ImageText { get; set; }
        public string Description { get; set; }
        public bool Visible { get; set; }
    }
}
