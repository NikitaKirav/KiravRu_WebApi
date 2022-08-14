using KiravRu.Logic.Domain.HistoryChanges;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace KiravRu.Logic.Interface.HistoryChanges
{
    public interface IHistoryChangeRepository : IRepository<HistoryChange>
    {
        Task<List<HistoryChange>> GetHistoryChangesAsync(CancellationToken ct);
        Task<HistoryChange> GetHistoryChangeByIdAsync(int historyChangeId, CancellationToken ct);
        Task<DateTime> GetLastDateTimeByIpAddressAsync(string remoteIpAddress, CancellationToken ct);
    }
}
