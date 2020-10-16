using KiravRu.Interfaces;
using KiravRu.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KiravRu.Data.Repository
{
    public class ArticlesAccessRepository : IArticlesAccess
    {
        private readonly ApplicationContext _db;

        public ArticlesAccessRepository(ApplicationContext db)
        {
            _db = db;
        }

        public IList<string> GetRoles(int articleId)
        {
            if (articleId == 0) { return new List<string>(); }
            var roles = _db.ArticlesAccesses.Include(x => x.Role).Where(x => x.ArticleId == articleId).ToList();
            IList<string> rolesList = new List<string>();
            foreach (var role in roles)
            {
                rolesList.Add(role.Role.Name);
            }
            return rolesList;
        }
    }
}
