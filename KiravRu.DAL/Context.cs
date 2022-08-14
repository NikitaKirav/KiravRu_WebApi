using KiravRu.Logic.Domain.Categories;
using KiravRu.Logic.Domain.Constants;
using KiravRu.Logic.Domain.HistoryChanges;
using KiravRu.Logic.Domain.Notes;
using KiravRu.Logic.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KiravRu.DAL
{
    public class Context : IdentityDbContext<User, Role, string,
    IdentityUserClaim<string>, // TUserClaim
    UserRole,                  // TUserRole,
    IdentityUserLogin<string>, // TUserLogin
    IdentityRoleClaim<string>, // TRoleClaim
    IdentityUserToken<string>>, IDbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        public DbSet<Note> Notes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<NoteAccess> NoteAccesses { get; set; }
        public DbSet<HistoryChange> HistoryChanges { get; set; }
        public DbSet<Constant> Constants { get; set; }

        public IQueryable<T> GetQuery<T>() where T : class
        {
            return Set<T>();
        }

        public void AddToSet<T>(T entity) where T : class
        {
            Set<T>().Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            Set<T>().Remove(entity);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var assemblyWithConfigurations = GetType().Assembly; //get whatever assembly you want
            modelBuilder.ApplyConfigurationsFromAssembly(assemblyWithConfigurations);
        }

        public async Task SaveChanges(CancellationToken ct)
        {
            await SaveChangesAsync(ct);
        }
    }
}
