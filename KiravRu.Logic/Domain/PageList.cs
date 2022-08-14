using System.Collections.Generic;

namespace KiravRu.Logic.Domain
{
    public class PageList<T>
    {
        public IEnumerable<T> List { get; set; }
        public int TotalRecordCount { get; set; }

        public PageList(IEnumerable<T> list, int totalRecordCount)
        {
            List = list;
            TotalRecordCount = totalRecordCount;
        }
    }
}
