using KiravRu.Logic.Domain.Users;
using KiravRu.Logic.Interface.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace KiravRu.DAL.Repository.Users
{
    public class UserRepository : IUserRepository
    {
        public readonly IDbContext _dbContext;

        public UserRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<User> GetById(string id, CancellationToken ct)
        {
            var user = await _dbContext.GetQuery<User>()
                .Include(s => s.UserRoles)
                .ThenInclude(r => r.Role)
                .FirstOrDefaultAsync(s => s.Id == id, ct);

            return user;
        }

        public async Task<List<User>> GetAll(CancellationToken ct)
        {
            var users = await _dbContext.GetQuery<User>()
                .Include(s => s.UserRoles)
                .ThenInclude(r => r.Role)
                .ToListAsync(ct);

            return users;
        }

    }
}