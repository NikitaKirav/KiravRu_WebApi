using KiravRu.Logic.Domain.Categories;
using KiravRu.Logic.Domain.Notes;
using KiravRu.Logic.Domain.Users;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace KiravRu.Logic.Mediator.Queries.Notes
{
    public class GetNoteForEditingByIdQueryResult
    {
        public NoteForEditing Article { get; set; }
        public List<CategoryApi> Categories { get; set; }
        public RolesApi Roles { get; set; }

        public GetNoteForEditingByIdQueryResult(Note note, IEnumerable<Category> categories, NoteRoles roles)
        {
            Article = new NoteForEditing(note);
            Roles = new RolesApi(roles);
            Categories = new List<CategoryApi>();

            foreach (var category in categories)
            {
                Categories.Add(new CategoryApi(category));
            }
        }
    }

    public class NoteRoles
    {
        public List<Role> AllRoles { get; set; }
        public IList<string> UserRoles { get; set; }
        public NoteRoles()
        {
            AllRoles = new List<Role>();
            UserRoles = new List<string>();
        }
    }

    public class NoteForEditing
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

        public NoteForEditing(Note article)
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

        public NoteForEditing()
        {

        }

        static public Note ConvertToNote(NoteForEditing article)
        {
            return new Note()
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
                NoteForEditing p = (NoteForEditing)obj;
                return (Id == p.Id) && (Name == p.Name) && (ImagePath == p.ImagePath) && (ImageText == p.ImageText)
                    && (DateCreate == p.DateCreate) && (DateChange == p.DateChange) && (Text == p.Text)
                    && (ShortDescription == p.ShortDescription) && (CategoryId == p.CategoryId)
                    && (DifficultyLevel == p.DifficultyLevel) && (IsFavorite == p.IsFavorite) && (Visible == p.Visible);
            }

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

        public RolesApi(NoteRoles roles)
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
