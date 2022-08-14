using KiravRu.Logic.Domain.Notes;
using System;
using System.Globalization;
using System.Linq;

namespace KiravRu.Logic.Mediator.Queries.Notes
{
    public class GetNoteByIdQueryResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string ImageText { get; set; }
        public string DateCreate { get; set; }
        public string DateChange { get; set; }
        public string Text { get; set; }
        public string ShortDescription { get; set; }
        public int CategoryId { get; set; }
        public int DifficultyLevel { get; set; }

        public GetNoteByIdQueryResult(Note article)
        {
            if (article != null)
            {
                Id = article.Id;
                Name = article.Name;
                ImagePath = article.ImagePath;
                DateCreate = article.DateCreate.ToString("MMMM dd, yyyy", CultureInfo.GetCultureInfo("en-us"));
                DateChange = article.DateChange.ToString("MMMM dd, yyyy", CultureInfo.GetCultureInfo("en-us"));
                Text = article.Text;
                ShortDescription = article.ShortDescription;
                CategoryId = article.CategoryId;
                DifficultyLevel = article.DifficultyLevel;
            }
        }

        public override int GetHashCode()
        {
            return (Id << 2) ^ DifficultyLevel ^ Name.Count() ^ ShortDescription.Count() ^ DateCreate.Count();
        }

        public override bool Equals(Object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                GetNoteByIdQueryResult p = (GetNoteByIdQueryResult)obj;
                return (Id == p.Id) && (Name == p.Name) && (ImagePath == p.ImagePath) && (ImageText == p.ImageText)
                    && (DateCreate == p.DateCreate) && (DateChange == p.DateChange) && (Text == p.Text)
                    && (ShortDescription == p.ShortDescription) && (CategoryId == p.CategoryId)
                    && (DifficultyLevel == p.DifficultyLevel);
            }
        }
    }
}
