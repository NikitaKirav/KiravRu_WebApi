using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KiravRu.Models
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
               // Database.EnsureCreated();
        }


        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ArticlesAccess> ArticlesAccesses { get; set; }
        public DbSet<HistoryChange> HistoryChanges { get; set; }
        public DbSet<Constant> Constants { get; set; }
    }
}
