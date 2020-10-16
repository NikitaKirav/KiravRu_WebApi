using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace KiravRu.Models
{
    public class ArticlesApi
    {
        public List<ArticleShortApi> Items { get; set; }
        public int TotalCount { get; set; }
        public string Errors { get; set; }

        public ArticlesApi()
        {
            Items = new List<ArticleShortApi>();
        }

        public void AddItems(List<Article> articles)
        {
            foreach (var article in articles)
            {
                Items.Add(new ArticleShortApi
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

    public class ArticleShortApi
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string DateCreate { get; set; }
        public int DifficultyLevel { get; set; }
    }
}
