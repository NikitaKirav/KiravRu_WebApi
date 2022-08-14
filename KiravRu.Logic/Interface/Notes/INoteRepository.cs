using KiravRu.Logic.Domain;
using KiravRu.Logic.Domain.Notes;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace KiravRu.Logic.Interface.Notes
{
    public interface INoteRepository : IRepository<Note>
    {
        Task<List<Note>> GetNotesAsync(CancellationToken ct);
        Task<List<Note>> GetFavNotesAsync(CancellationToken ct);
        Task<Note> GetNoteByIdAsync(int id, CancellationToken ct);
        PageList<Note> GetNotesWithFilter(NoteFilter noteFilter, List<NoteAccess> listOfNotes);
        void Update(Note article);
        Note GetNoteByIdWithAccess(int noteId, List<NoteAccess> listOfNotes);
    }
}
