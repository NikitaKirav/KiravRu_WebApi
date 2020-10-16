using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KiravRu.Interfaces;
using KiravRu.Models;
using KiravRu.Models.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KiravRu.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesApiController : ControllerBase
    {
        private readonly IAllArticles _allArticles;
        private readonly IArticlesCategory _allCategories;

        public CategoriesApiController(IAllArticles allArticles, IArticlesCategory allCategories)
        {
            _allArticles = allArticles;
            _allCategories = allCategories;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("list")]
        public ActionResult List(string search)
        {
            var categories = _allCategories.AllCategories.ToList();
            return Ok(new { categories = categories, totalCategoriesCount = categories.Count });
        }


        [HttpPost]
        [Authorize(Roles = "admin")]
        [Route("create")]
        public ActionResult Create(Category category)
        {
            _allCategories.AddCategory(category);
            return Ok("Success");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("create")]
        public IActionResult Create()
        {
            return Ok(GetCategoryEditExport(0));
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [Route("create")]
        public ActionResult Create(ArticleEditImportApi articleApi)
        {
            if ((articleApi != null) && (articleApi.Article != null) && (articleApi.Roles != null))
            {
                var article = ArticleEditApi.ConvertToArticle(articleApi.Article);
                var newArticle = _allArticles.AddArticles(article, articleApi.Roles.ToList());
                if (newArticle != null)
                {
                    return Ok(GetCategoryEditExport(newArticle.Id));
                }
            }
            return Ok(articleApi);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("edit")]
        public ActionResult Edit(int id)
        {
            var category = GetCategoryEditExport(id);
            return Ok(category);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [Route("edit")]
        public ActionResult Edit(CategoryEditImportApi categoryApi)
        {
            if ((categoryApi != null) && (categoryApi.Category != null))
            {
                var category = CategoryEditApi.ConvertToCategory(categoryApi.Category);
                if (category.Id == 0)
                {
                    var newCategory = _allCategories.AddCategory(category);
                    if (newCategory != null)
                    {
                        return Ok(GetCategoryEditExport(newCategory.Id));
                    }
                }
                else
                {
                    var result = _allCategories.UpdateCategories(category);
                    if (result)
                    {
                        return Ok(GetCategoryEditExport(category.Id));
                    }
                }

            }
            return Ok(GetCategoryEditExport(categoryApi.Category.Id));
        }


        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("delete")]
        public ActionResult Delete(int id)
        {
            var category = _allCategories.GetObjectCategory(id);
            _allCategories.DeleteCategory(category);
            return Ok();
        }


        private CategoryEditExportApi GetCategoryEditExport(int categoryId)
        {
            var category = new Category();
            if (categoryId != 0)
            {
                category = _allCategories.GetObjectCategory(categoryId);
            }
            var listCategory = _allCategories.OrderAllCategory();

            return new CategoryEditExportApi(category, listCategory);
        }

    }
}
