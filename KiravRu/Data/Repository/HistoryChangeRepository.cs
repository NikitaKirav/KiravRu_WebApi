using KiravRu.Interfaces;
using KiravRu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KiravRu.Data.Repository
{
    public class HistoryChangeRepository : IHistoryChange
    {
        private readonly ApplicationContext _db;

        public HistoryChangeRepository(ApplicationContext db)
        {
            _db = db;
        }
        public IEnumerable<HistoryChange> AllHistoryChanges => _db.HistoryChanges.OrderBy(x => x.DateChanges);        
        public HistoryChange GetHistoryChange(int historyChangeId) => _db.HistoryChanges.FirstOrDefault(x => x.Id == historyChangeId);

        public void AddHistoryChange(HistoryChange historyChange)
        {
            _db.HistoryChanges.Add(historyChange);
            _db.SaveChanges();
        }

        public DateTime GetLastDateTimeUsingIpAddress(string remoteIpAddress)
        {
            var historyChanges = _db.HistoryChanges.Where(x => x.RemoteIpAddress == remoteIpAddress)
                .OrderByDescending(t => t.DateChanges).FirstOrDefault();
            if (historyChanges == null) { return DateTime.Now.AddDays(-1); }
            return historyChanges.DateChanges;
        }

    }
}
