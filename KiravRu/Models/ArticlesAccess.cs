using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KiravRu.Models
{
    [Table("ArticlesAccess")]
    ///List of roles which have access to a article
    public class ArticlesAccess
    {
        public int Id { get; set; }
        public string RoleId { get; set; }
        public virtual IdentityRole Role { get; set; }
        public int ArticleId { get; set; }
        public virtual Article Article { get; set; }
    }
}
