using KiravRu.Logic.Domain.Notes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace KiravRu.Logic.Mediator.Queries.Notes
{
    public class GetNotesQueryResult
    {
        public List<ConciseNote> Items { get; set; }
        public int TotalCount { get; set; }
        public string Errors { get; set; }

        public GetNotesQueryResult()
        {
            Items = new List<ConciseNote>();
        }

        public void AddItems(IEnumerable<Note> articles)
        {
            foreach (var article in articles)
            {
                Items.Add(new ConciseNote
                {
                    Id = article.Id,
                    Title = article.Name,
                    Description = article.ShortDescription,
                    DateCreate = article.DateCreate.ToString("MMMM dd, yyyy", CultureInfo.GetCultureInfo("en-us")),
                    DifficultyLevel = article.DifficultyLevel
                });
            }
        }
    }

    public class ConciseNote
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string DateCreate { get; set; }
        public int DifficultyLevel { get; set; }

        public override int GetHashCode()
        {
            return (Id << 2) ^ DifficultyLevel ^ Title.Count() ^ Description.Count() ^ DateCreate.Count();
        }

        public override bool Equals(Object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                ConciseNote p = (ConciseNote)obj;
                return (Id == p.Id) && (Title == p.Title) && (Description == p.Description)
                    && (DateCreate == p.DateCreate) && (DifficultyLevel == p.DifficultyLevel);
            }
        }
    }
}
