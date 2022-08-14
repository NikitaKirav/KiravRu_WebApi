using KiravRu.Logic.Domain.Notes;
using System.Collections.Generic;


namespace KiravRu.Logic.Domain.Categories
{
    public class Category : IIdentity
    {
        public int Id { get; set; }
        public int? OrderItem { get; set; } // Порядковый номер (при необходимости)
        public int? NestingLevelId { get; set; } // Уровень вложенности (Id номер объекта в котором размещен этот объект)
        public virtual Category NestingLevel { get; set; } 
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string ImageText { get; set; }
        public string Description { get; set; }
        public bool Visible { get; set; }
        public List<Note> Notes { get; set; }
    }
}
