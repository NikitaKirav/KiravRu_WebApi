using KiravRu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KiravRu.Interfaces
{
    public interface IArticlesAccess
    {
        IList<string> GetRoles(int articleId);
    }
}
