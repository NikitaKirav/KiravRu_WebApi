using KiravRu.Interfaces;
using KiravRu.Models;
using KiravRu.Models.WebApi;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Linq;


namespace KiravRu.Data.Repository
{
    public class ArticleRepository : IAllArticles
    {
        private readonly ApplicationContext _db;

        public ArticleRepository(ApplicationContext db)
        {
            _db = db;
        }
        public IEnumerable<Article> ArticlesAll => _db.Articles.Include(c => c.Category);
        public IEnumerable<Article> GetFavArticles => _db.Articles.Where(x => x.IsFavorite).Include(c => c.Category);
        public Article GetObjectArticle(int articleId)
        {
            if (articleId == 0) { return new Article();  }
            return _db.Articles.FirstOrDefault(x => x.Id == articleId);
        }

        public Article AddArticles(Article article, List<string> roles)
        {
            if (article.CategoryId == 0) { return null;  }
            article.DateCreate = DateTime.Now;
            article.DateChange = DateTime.Now;
            article.Number = _db.Articles.Max(x => x.Number) + 1;
            _db.Articles.Add(article);
            _db.SaveChanges();
            ChangeRolesOfArticle(article.Id, roles);
            _db.SaveChanges();
            return article;

        }

        public PagingList<Article> Articles(int page, int articlesOnPage, string sort, string defaultSort)
        {
            // get a list of article
            var articles = _db.Articles.Include(c => c.Category).AsEnumerable().OrderBy(x => x.DateChange);
            int pageSize = (int)Math.Ceiling((double)articles.Count() / articlesOnPage);
            var model = PagingList.Create(articles, articlesOnPage, page, sort, defaultSort);

            return model;
        }

        /// <summary>
        /// Get a list of articles which have user access level
        /// </summary>
        /// <param name="roles">Names roles of user</param>
        /// <returns></returns>
        public PagingList<Article> Articles(IList<string> roles, string defaultSort, BlogFilterApi blogFilter)
        {
            if (blogFilter.Search == null) { return null; }
            // get Ids of roles of current user
            var rolesIdLast = _db.Roles.Where(x => roles.Any(c => c == x.Name)).Select(x => x.Id).ToArray();
            // get a list of Id article
            var listOfArticles = _db.ArticlesAccesses.Where(x => rolesIdLast.Any(c => x.RoleId == c)).ToList();
            // get a list of article
            var articles = _db.Articles.Include(c => c.Category).AsEnumerable()
                .Where(x =>
                (x.Name != null && x.Name.ToLower().Contains(blogFilter.Search.ToLower())) ||
                (x.Text != null && x.Text.ToLower().Contains(blogFilter.Search.ToLower())) ||
                (x.ShortDescription != null && x.ShortDescription.ToLower().Contains(blogFilter.Search.ToLower())))
                            .Join(listOfArticles, 
                            leftItem => leftItem.Id, 
                            rightItem => rightItem.ArticleId, 
                            (leftItem, rightItem) => leftItem).OrderBy(x => x.DateChange);
            var model = PagingList.Create(articles, blogFilter.PageSize, blogFilter.PageIndex, blogFilter.Sort, defaultSort);

            return model;
        }

        public Article GetObjectArticleAccess(int articleId, IList<string> roles)
        {
            // get Ids of roles of current user
            var rolesIdLast = _db.Roles.Where(x => roles.Any(c => c == x.Name)).Select(x => x.Id).ToArray();
            // get a list of Id article
            var listOfArticles = _db.ArticlesAccesses.Where(x => rolesIdLast.Any(c => x.RoleId == c)).ToList();
            // get a list of article
            var articles = _db.Articles.Include(c => c.Category).AsEnumerable()
                            .Join(listOfArticles,
                            leftItem => leftItem.Id,
                            rightItem => rightItem.ArticleId,
                            (leftItem, rightItem) => leftItem).OrderBy(x => x.DateChange);

            var article = articles.FirstOrDefault(x => x.Id == articleId);
            if (article == null)
            {
                article = _db.Articles.FirstOrDefault(x => x.Name == "Error");
            }
            return article;
        }

        public bool UpdateArticle(Article article, List<string> roles)
        {            
            ChangeRolesOfArticle(article.Id, roles);            
            article.DateChange = DateTime.Now;
            article.DateCreate = _db.Articles.AsNoTracking().FirstOrDefault(x => x.Id == article.Id).DateCreate;
            _db.Entry(article).State = EntityState.Modified;
            var result = _db.SaveChanges();
            return result > 0; 
        }

        /// <summary>
        /// Delete Article and Roles
        /// </summary>
        /// <param name="article"></param>
        public void DeleteArticle(Article article)
        {
            _db.Entry(article).State = EntityState.Deleted;
            DeleteRolesOfArticle(article.Id);
            _db.SaveChanges();
        }

        private void ChangeRolesOfArticle(int articleId, List<string> roles)
        {
            var articleAccess = new ArticlesAccess();
            var rolesIdLast = _db.Roles.Where(x => roles.Any(c => c == x.Name)).Select(x => x.Id).ToArray();
            var userRoles = _db.ArticlesAccesses.Include(x => x.Role)
                                    .Where(x => x.ArticleId == articleId)
                                    .Select(x => x.Role.Id).ToList();
            // получаем список ролей, которые были добавлены
            var addedRoles = rolesIdLast.Except(userRoles);
            // получаем роли, которые были удалены
            var removedRoles = userRoles.Except(rolesIdLast);

            // Add new Roles
            foreach (var role in addedRoles)
            {
                _db.ArticlesAccesses.Add(new ArticlesAccess()
                {
                    ArticleId = articleId,
                    RoleId = role
                });
            }
            // Delete old roles
            foreach (var role in removedRoles)
            {
                var access = _db.ArticlesAccesses.FirstOrDefault(x => x.RoleId == role && x.ArticleId == articleId);
                if (access != null)
                {
                    _db.Entry(access).State = EntityState.Deleted;
                }
            }
        }

        /// <summary>
        /// Delete Roles in ArticleAccess
        /// </summary>
        /// <param name="articleId"></param>
        private void DeleteRolesOfArticle(int articleId)
        {
            var articleAccess = new ArticlesAccess();
            var userRoles = _db.ArticlesAccesses.Include(x => x.Role)
                                    .Where(x => x.ArticleId == articleId)
                                    .Select(x => x.Role.Id).ToList();

            // Delete old roles
            foreach (var role in userRoles)
            {
                var access = _db.ArticlesAccesses.FirstOrDefault(x => x.RoleId == role && x.ArticleId == articleId);
                if (access != null)
                {
                    _db.Entry(access).State = EntityState.Deleted;
                }
            }
        }
    }
}
