using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using KiravRu.Interfaces;
using KiravRu.Models;
using KiravRu.Models.WebApi;
using KiravRu.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KiravRu.Controllers
{
    [Route("api/blog")]
    [ApiController]
    public class ArticlesApiController : ControllerBase
    {
        private readonly IAllArticles _allArticles;
        private readonly IArticlesCategory _allCategories;
        private readonly IArticlesAccess _allArticlesAccess;
        private readonly IConstant _constants;
        RoleManager<IdentityRole> _roleManager;
        UserManager<ApplicationUser> _userManager;

        public ArticlesApiController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager,
            IAllArticles allArticles, IArticlesCategory allCategories, IArticlesAccess allArticlesAccess,
            IConstant constants)
        {
            _allArticles = allArticles;
            _allCategories = allCategories;
            _allArticlesAccess = allArticlesAccess;
            _constants = constants;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // POST: 
        [HttpPost]
        [Route("getArticles")]
        public IActionResult getArticles(BlogFilterApi blogFilter)
        {
            if (blogFilter.Search == null) { blogFilter.Search = ""; }
            var userRoles = GetCurrentRolesOfUser();
            var articles = _allArticles.Articles(userRoles, "Name", blogFilter);
            var articleApi = new ArticlesApi();
            articleApi.TotalCount = articles.TotalRecordCount;
            articleApi.AddItems(articles);
            return Ok(articleApi);
        }

        // GET: 
        [HttpGet]
        [Route("getArticle")]
        public IActionResult getArticle(int articleId)
        {
            var userRoles = GetCurrentRolesOfUser();
            var article = _allArticles.GetObjectArticleAccess(articleId, userRoles);
            var articleApi = new ArticleApi(article);
            return Ok(articleApi);
        }


        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("edit")]
        public IActionResult Edit(int articleId)
        {
            return Ok(GetArticalEditExport(articleId));
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [Route("edit")]
        public ActionResult EditArticle(ArticleEditImportApi articleApi)
        {
            if((articleApi != null) && (articleApi.Article != null) && (articleApi.Roles != null))
            {
                var article = ArticleEditApi.ConvertToArticle(articleApi.Article);
                if(article.Id == 0)
                {
                    var newArticle = _allArticles.AddArticles(article, articleApi.Roles.ToList());
                    if (newArticle != null)
                    {
                        return Ok(GetArticalEditExport(newArticle.Id));
                    }
                }
                else
                {
                    var result = _allArticles.UpdateArticle(article, articleApi.Roles.ToList());
                    if (result)
                    {
                        return Ok(GetArticalEditExport(article.Id));
                    }
                }

            }
            return Ok(articleApi);
        }

        //[HttpGet]
        //[Authorize(Roles = "admin")]
        //[Route("create")]
        //public IActionResult Create()
        //{
        //    return Ok(GetArticalEditExport(0));
        //}

        //[HttpPost]
        //[Authorize(Roles = "admin")]
        //[Route("create")]
        //public ActionResult Create(ArticleEditImportApi articleApi)
        //{
        //    if ((articleApi != null) && (articleApi.Article != null) && (articleApi.Roles != null))
        //    {
        //        var article = ArticleEditApi.ConvertToArticle(articleApi.Article);
        //        var newArticle = _allArticles.AddArticles(article, articleApi.Roles.ToList());
        //        if (newArticle != null)
        //        {
        //            return Ok(GetArticalEditExport(newArticle.Id));
        //        }
        //    }
        //    return Ok(articleApi);
        //}

        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("delete")]
        public ActionResult Delete(int id)
        {
            var article = _allArticles.GetObjectArticle(id);
            _allArticles.DeleteArticle(article);
            return Ok();
        }

        private ArticleEditExportApi GetArticalEditExport(int articleId)
        {
            var article = new Article();
            ChangeRoleViewModel model = GetArticleByNomber(articleId);
            var listRoles = model;
            if (articleId != 0)
            {  
                article = _allArticles.GetObjectArticle(articleId);
            }
            var listCategory = _allCategories.OrderAllCategory();

            return new ArticleEditExportApi(article, listCategory, listRoles);
        }


        private IList<string> GetCurrentRolesOfUser()
        {
            var rolesClaims = HttpContext.User.Claims
                .Where(x => x.Type == ClaimTypes.Role);
            List<string> roles = new List<string>();
            foreach (var item in rolesClaims)
            {
                roles.Add(item.Value);
            }
            //// default is an user
            if (roles.Count == 0) { return new List<string>() { "user" }; }
            return roles;
        }

        private ChangeRoleViewModel GetArticleByNomber(int id)
        {
            // получем список ролей пользователя
            var userRoles = _allArticlesAccess.GetRoles(id);
            var allRoles = _roleManager.Roles.ToList();
            ChangeRoleViewModel model = new ChangeRoleViewModel
            {
                UserRoles = userRoles,
                AllRoles = allRoles
            };
            return model;
        }
    }
}