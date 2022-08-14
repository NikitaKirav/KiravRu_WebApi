using KiravRu.Logic.Domain.Constants;
using KiravRu.Logic.Interface.Constants;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KiravRu.DAL.Repository.Constants
{
    public class ConstantRepository : Repository<Constant>, IConstantRepository
    {
        public ConstantRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Constant>> GetConstantsAsync(CancellationToken ct)
        {
            return await _dbContext.GetQuery<Constant>().ToListAsync(ct);
        }

        public async Task<Constant> GetConstantByIdAsync(int Id, CancellationToken ct)
        {
            return await _dbContext.GetQuery<Constant>().FirstOrDefaultAsync(x => x.Id == Id, ct);
        }

        public async Task<int?> GetValueAsync(string name, CancellationToken ct)
        {
            try
            {
                var value = await _dbContext.GetQuery<Constant>().FirstOrDefaultAsync(x => x.Name == name, ct);
                if (value != null)
                {
                    return Convert.ToInt32(value.Value);
                }
            }
            catch { }
            return null;
        }
    }
}
