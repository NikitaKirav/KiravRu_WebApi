using System;

namespace KiravRu.Logic.Domain.HistoryChanges
{
    public class HistoryChange : IIdentity
    {
        public int Id { get; set; }
        public string RemoteIpAddress { get; set; }
        public string ModuleChanged { get; set; }
        public string Operation { get; set; }
        public string Changes { get; set; }
        public DateTime DateChanges { get; set; }
    }
}
