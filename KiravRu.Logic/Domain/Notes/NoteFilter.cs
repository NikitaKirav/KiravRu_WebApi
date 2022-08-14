using System;

namespace KiravRu.Logic.Domain.Notes
{
    public class NoteFilter
    {
        public int PageIndex { get; set; } = 1;
        public string Sort { get; set; } = "-DateChange";
        public string Search { get; set; } = "";
        public int PageSize { get; set; } = 10;

        public override int GetHashCode()
        {
            return (PageIndex << 2) ^ PageSize ^ Sort.Length ^ Search.Length;
        }

        public override bool Equals(Object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                NoteFilter p = (NoteFilter)obj;
                return (PageIndex == p.PageIndex) && (Sort == p.Sort) && (Search == p.Search) && (PageSize == p.PageSize);
            }
        }
    }
}
