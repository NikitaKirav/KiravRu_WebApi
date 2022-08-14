using KiravRu.Logic.Domain;
using KiravRu.Logic.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KiravRu.DAL.Repository
{
    public class Repository<T> : IRepository<T> where T : class, IIdentity
    {
        public readonly IDbContext _dbContext;

        public Repository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddToSet(T item)
        {
            _dbContext.AddToSet(item);
        }

        public async Task SaveChanges(CancellationToken ct)
        {
            await _dbContext.SaveChanges(ct);
        }

        public void Save(T item)
        {
            if (item.Id == 0)
            {
                _dbContext.Set<T>().Add(item);
            }
            else
            {
                _dbContext.Set<T>().Attach(item);
                _dbContext.Entry(item).State = EntityState.Modified;
            }
        }

        public void Delete(T item)
        {
            _dbContext.Delete(item);
        }
    }
}
