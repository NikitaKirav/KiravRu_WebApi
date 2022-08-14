using KiravRu.Logic.Domain.Notes;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace KiravRu.Logic.Interface.Notes
{
    public interface INoteAccessRepository : IRepository<NoteAccess>
    {
        Task<IList<string>> GetRolesAsync(int articleId, CancellationToken ct);
        Task<List<NoteAccess>> GetListOfNotesByRoleIds(string[] rolesIdLast, CancellationToken ct);
        void ChangeRolesOfNote(int noteId, List<string> roles);
    }
}
