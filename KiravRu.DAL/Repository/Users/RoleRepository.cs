using KiravRu.Logic.Domain.Users;
using KiravRu.Logic.Interface.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KiravRu.DAL.Repository.Users
{
    public class RoleRepository : IRoleRepository
    {
        public readonly IDbContext _dbContext;

        public RoleRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string[]> GetRoleIdsByRoleNamesAsync(IList<string> roles, CancellationToken ct)
        {
            try
            {
                return await _dbContext.GetQuery<Role>().Where(x => roles.Any(c => c == x.Name)).Select(x => x.Id).ToArrayAsync(ct);
            } catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        public async Task<List<Role>> GetRolesAsync(CancellationToken ct)
        {
            return await _dbContext.GetQuery<Role>().ToListAsync(ct);
        }
    }
}
