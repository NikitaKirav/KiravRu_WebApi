using KiravRu.Logic.Domain.Categories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace KiravRu.Logic.Domain.Notes
{
    [Table("Articles")]
    public class Note : IIdentity
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
        public List<NoteAccess> NoteAccesses { get; set; }

        public override int GetHashCode()
        {
            return (Id << 2) ^ DifficultyLevel ^ Name.Count() ^ Text.Count() ^ ShortDescription.Count();
        }

        public override bool Equals(Object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Note p = (Note)obj;
                return (Id == p.Id) && (Name == p.Name) && (ImagePath == p.ImagePath) && (ImageText == p.ImageText)
                    && (DateCreate == p.DateCreate) && (DateChange == p.DateChange) && (DifficultyLevel == p.DifficultyLevel)
                    && (Text == p.Text) && (ShortDescription == p.ShortDescription) && (CategoryId == p.CategoryId)
                    && (IsFavorite == p.IsFavorite) && (Visible == p.Visible) && (Number == p.Number);
            }
        }
    }
}
