using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KiravRu.DAL
{
    public interface IDbContext : IDisposable
    {
        public IQueryable<T> GetQuery<T>() where T : class;
        void AddToSet<T>(T entity) where T : class;
        Task SaveChanges(CancellationToken ct);
        void Delete<T>(T entity) where T : class;
        public DbSet<TEntity> Set<TEntity>() where TEntity : class;
        public EntityEntry<TEntity> Entry<TEntity>([NotNullAttribute] TEntity entity) where TEntity : class;
    }
}
