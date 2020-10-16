using KiravRu.Models;
using KiravRu.Models.WebApi;
using Microsoft.AspNetCore.Identity;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KiravRu.Interfaces
{
    public interface IAllArticles
    {
        IEnumerable<Article> ArticlesAll { get; }
        IEnumerable<Article> GetFavArticles { get; }
        Article GetObjectArticle(int articleId);
        Article AddArticles(Article article, List<string> roles);
        bool UpdateArticle(Article article, List<string> roles);
        void DeleteArticle(Article article);

        //PagingList<Article> Articles(IList<string> roles, int page, string sort, string defaultSort);
        PagingList<Article> Articles(int page, int articlesOnPage, string sort, string defaultSort);
        PagingList<Article> Articles(IList<string> roles, string defaultSort, BlogFilterApi blogFilter);
        Article GetObjectArticleAccess(int articleId, IList<string> roles);
    }
}
