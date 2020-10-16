using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KiravRu.Models
{
    public class HistoryChange
    {
        public int Id { get; set; }
        public string RemoteIpAddress { get; set; }
        public string ModuleChanged { get; set; }
        public string Operation { get; set; }
        public string Changes { get; set; }
        public DateTime DateChanges { get; set; }
    }
}
