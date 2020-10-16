using KiravRu.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace KiravRu.Models.WebApi
{

    public class ArticleEditExportApi
    {
        public ArticleEditApi Article { get; set; }
        public List<CategoryApi> Categories { get; set; }
        public RolesApi Roles { get; set; }

        public ArticleEditExportApi(Article article, IEnumerable<Category> categories, ChangeRoleViewModel roles) 
        {
            Article = new ArticleEditApi(article);
            Roles = new RolesApi(roles);
            Categories = new List<CategoryApi>();

            foreach (var category in categories)
            {
                Categories.Add(new CategoryApi(category));
            }
        }
    }

    public class ArticleEditImportApi
    {
        public ArticleEditApi Article { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
    public class ArticleEditApi
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
        public bool IsFavorite { get; set; }
        public bool Visible { get; set; }

        public ArticleEditApi(Article article)
        {
            if (article != null)
            {
                Id = article.Id;
                Name = article.Name;
                ImagePath = article.ImagePath;
                DateCreate = article.DateCreate.ToString("dd.MM.yyyy, HH:mm:ss", CultureInfo.GetCultureInfo("en-us"));
                DateChange = article.DateChange.ToString("dd.MM.yyyy, HH:mm:ss", CultureInfo.GetCultureInfo("en-us"));
                Text = article.Text;
                ShortDescription = article.ShortDescription;
                CategoryId = article.CategoryId;
                DifficultyLevel = article.DifficultyLevel;
                IsFavorite = article.IsFavorite;
                Visible = article.Visible;
            }
        }

        public ArticleEditApi()
        {

        }

        static public Article ConvertToArticle(ArticleEditApi article)
        {
            return new Article()
            {
                Id = article.Id,
                Name = article.Name,
                ImagePath = article.ImagePath,
                ImageText = article.ImageText,
                Text = article.Text,
                ShortDescription = article.ShortDescription,
                CategoryId = article.CategoryId,
                IsFavorite = article.IsFavorite,
                Visible = article.Visible,
                DifficultyLevel = article.DifficultyLevel
            };
        }
    }

    public class CategoryApi
    {
        public int Value { get; set; }
        public int? OrderItem { get; set; } // Порядковый номер (при необходимости)
        public int? NestingLevelId { get; set; } // Уровень вложенности (Id номер объекта в котором размещен этот объект)
        public string Label { get; set; }

        public CategoryApi(Category category)
        {
            Value = category.Id;
            OrderItem = category.OrderItem;
            NestingLevelId = category.NestingLevelId;
            Label = category.Name;
        }
    }

    public class RolesApi
    {
        public List<RoleApi> AllRoles { get; set; }
        public List<string> UserRoles { get; set; }

        public RolesApi(ChangeRoleViewModel roles)
        {
            AllRoles = new List<RoleApi>();
            UserRoles = new List<string>();

            foreach (var role in roles.AllRoles)
            {
                AllRoles.Add(new RoleApi(role.Id, role.Name));
            }
            foreach (var roleName in roles.UserRoles)
            {
                UserRoles.Add(roleName);
            }
        }

        public RolesApi() { }
    }

    public class RoleApi
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public RoleApi(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public RoleApi() { }
    }
}