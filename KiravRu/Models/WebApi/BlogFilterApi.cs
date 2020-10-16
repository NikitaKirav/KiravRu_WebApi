using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KiravRu.Models.WebApi
{
    public class BlogFilterApi
    {
        public int PageIndex { get; set; } = 1;
        public string Sort { get; set; } = "-DateChange";
        public string Search { get; set; } = "";
        public int PageSize { get; set; } = 10;
    }
}
