using KiravRu.Logic.Domain.Users;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KiravRu.Logic.Domain.Notes
{
    [Table("ArticlesAccess")]
    ///List of roles which have access to a article
    public class NoteAccess : IIdentity
    {
        public int Id { get; set; }
        public string RoleId { get; set; }
        public virtual Role Role { get; set; }
        [Column("ArticleId")]
        public int NoteId { get; set; }
        public virtual Note Note { get; set; }

        public override int GetHashCode()
        {
            return (Id << 2) ^ NoteId ^ RoleId.Length;
        }

        public override bool Equals(Object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                NoteAccess p = (NoteAccess)obj;
                return (Id == p.Id) && (RoleId == p.RoleId) && (NoteId == p.NoteId);
            }
        }
    }
}
