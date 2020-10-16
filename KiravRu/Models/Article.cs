using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KiravRu.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string ImageText { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateChange { get; set; }
        public string Text { get; set; }
        public string ShortDescription { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public bool IsFavorite { get; set; }
        public bool Visible { get; set; }
        public int? Number { get; set; } // Order number (This is you can use for numeration of article, but now I don't use)
        public int DifficultyLevel { get; set; } 
    }
}
