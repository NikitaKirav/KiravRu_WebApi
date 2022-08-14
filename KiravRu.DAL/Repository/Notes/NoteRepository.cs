using KiravRu.Logic.Domain;
using KiravRu.Logic.Domain.Notes;
using KiravRu.Logic.Interface.Notes;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KiravRu.DAL.Repository.Notes
{
    public class NoteRepository : Repository<Note>, INoteRepository
    {
        public NoteRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Note>> GetNotesAsync(CancellationToken ct)
        {
            return await _dbContext.GetQuery<Note>().Include(c => c.Category).OrderBy(x => x.DateChange).ToListAsync(ct);
        }

        public async Task<List<Note>> GetFavNotesAsync(CancellationToken ct)
        {
            return await _dbContext.GetQuery<Note>().Where(x => x.IsFavorite).Include(c => c.Category).ToListAsync(ct);
        }

        public async Task<Note> GetNoteByIdAsync(int id, CancellationToken ct)
        {
            return await _dbContext.GetQuery<Note>().FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public PageList<Note> GetNotesWithFilter(NoteFilter noteFilter, List<NoteAccess> listOfNotes)
        {
            if (noteFilter.Search == "")
            {
                var totalRecordCount = _dbContext.GetQuery<Note>().Include(c => c.Category).AsEnumerable()
                    .Join(listOfNotes,
                    leftItem => leftItem.Id,
                    rightItem => rightItem.NoteId,
                    (leftItem, rightItem) => leftItem).Count();
                var notes = _dbContext.GetQuery<Note>().Include(c => c.Category).AsEnumerable()
                    .Join(listOfNotes,
                    leftItem => leftItem.Id,
                    rightItem => rightItem.NoteId,
                    (leftItem, rightItem) => leftItem)
                    .OrderByDescending(x => x.DateChange)
                    .Skip((noteFilter.PageIndex - 1) * noteFilter.PageSize)
                    .Take(noteFilter.PageSize);
                return new PageList<Note>(notes, totalRecordCount);
            }
            else
            {
                var notes = _dbContext.GetQuery<Note>().Include(c => c.Category).AsEnumerable()
                    .Where(x =>
                    (x.Name != null && x.Name.ToLower().Contains(noteFilter.Search.ToLower())) ||
                    (x.Text != null && x.Text.ToLower().Contains(noteFilter.Search.ToLower())) ||
                    (x.ShortDescription != null && x.ShortDescription.ToLower().Contains(noteFilter.Search.ToLower())))
                                .Join(listOfNotes,
                                leftItem => leftItem.Id,
                                rightItem => rightItem.NoteId,
                                (leftItem, rightItem) => leftItem)
                    .OrderByDescending(x => x.DateChange)
                    .Skip((noteFilter.PageIndex - 1) * noteFilter.PageSize)
                    .Take(noteFilter.PageSize);
                var totalRecordCount = _dbContext.GetQuery<Note>().Include(c => c.Category).AsEnumerable()
                    .Where(x =>
                    (x.Name != null && x.Name.ToLower().Contains(noteFilter.Search.ToLower())) ||
                    (x.Text != null && x.Text.ToLower().Contains(noteFilter.Search.ToLower())) ||
                    (x.ShortDescription != null && x.ShortDescription.ToLower().Contains(noteFilter.Search.ToLower())))
                                .Join(listOfNotes,
                                leftItem => leftItem.Id,
                                rightItem => rightItem.NoteId,
                                (leftItem, rightItem) => leftItem).Count();
                return new PageList<Note>(notes, totalRecordCount);
            }
        }

        public Note GetNoteByIdWithAccess(int noteId, List<NoteAccess> listOfNotes)
        {
                return _dbContext.GetQuery<Note>().Include(c => c.Category).AsEnumerable()
                    .Join(listOfNotes,
                    leftItem => leftItem.Id,
                    rightItem => rightItem.NoteId,
                    (leftItem, rightItem) => leftItem).OrderBy(x => x.DateChange)
                    .FirstOrDefault(x => x.Id == noteId);
        }

        public new void AddToSet(Note note)
        {
            note.DateCreate = DateTime.Now;
            note.DateChange = DateTime.Now;
            _dbContext.AddToSet(note);
        }

        public void Update(Note article)
        {
            article.DateChange = DateTime.Now;
            article.DateCreate = _dbContext.GetQuery<Note>().AsNoTracking().FirstOrDefault(x => x.Id == article.Id).DateCreate;
            _dbContext.Entry(article).State = EntityState.Modified;
        }

    }
}