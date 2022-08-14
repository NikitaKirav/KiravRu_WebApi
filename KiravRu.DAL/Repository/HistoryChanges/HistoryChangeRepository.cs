using KiravRu.Logic.Domain.HistoryChanges;
using KiravRu.Logic.Interface.HistoryChanges;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KiravRu.DAL.Repository.HistoryChanges
{
    public class HistoryChangeRepository : Repository<HistoryChange>, IHistoryChangeRepository
    {
        public HistoryChangeRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<HistoryChange>> GetHistoryChangesAsync(CancellationToken ct)
        {
            return await _dbContext.GetQuery<HistoryChange>().OrderBy(x => x.DateChanges).ToListAsync(ct);
        }

        public async Task<HistoryChange> GetHistoryChangeByIdAsync(int historyChangeId, CancellationToken ct)
        {
            return await _dbContext.GetQuery<HistoryChange>().FirstOrDefaultAsync(x => x.Id == historyChangeId, ct);
        }

        public async Task<DateTime> GetLastDateTimeByIpAddressAsync(string remoteIpAddress, CancellationToken ct)
        {
            var historyChanges = await _dbContext.GetQuery<HistoryChange>().Where(x => x.RemoteIpAddress == remoteIpAddress)
                .OrderByDescending(t => t.DateChanges).FirstOrDefaultAsync(ct);
            if (historyChanges == null) { return DateTime.Now.AddDays(-1); }
            return historyChanges.DateChanges;
        }

    }
}