using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace KiravRu.Models.WebApi
{
    public class ArticleApi
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

        public ArticleApi(Article article) 
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
    }
}
