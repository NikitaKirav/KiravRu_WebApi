using KiravRu.Logic.Domain.Notes;
using KiravRu.Logic.Domain.Users;
using KiravRu.Logic.Interface.Notes;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KiravRu.DAL.Repository.Notes
{
    public class NoteAccessRepository : Repository<NoteAccess>, INoteAccessRepository
    {
        public NoteAccessRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IList<string>> GetRolesAsync(int noteId, CancellationToken ct)
        {
            if (noteId == 0) { return new List<string>(); }
            var roles = await _dbContext.GetQuery<NoteAccess>().Include(x => x.Role).Where(x => x.NoteId == noteId).ToListAsync(ct);
            IList<string> rolesList = new List<string>();
            foreach (var role in roles)
            {
                rolesList.Add(role.Role.Name);
            }
            return rolesList;
        }

        public async Task<List<NoteAccess>> GetListOfNotesByRoleIds(string[] rolesIdLast, CancellationToken ct)
        {
            return await _dbContext.GetQuery<NoteAccess>().Include(x => x.Note).Where(x => rolesIdLast.Any(c => x.RoleId == c)).ToListAsync(ct);
        }

        public void ChangeRolesOfNote(int noteId, List<string> roles)
        {
            var articleAccess = new NoteAccess();
            var rolesIdLast = _dbContext.GetQuery<Role>().Where(x => roles.Any(c => c == x.Name)).Select(x => x.Id.ToString()).ToArray();
            var userRoles = _dbContext.GetQuery<NoteAccess>().Include(x => x.Role)
                                    .Where(x => x.NoteId == noteId)
                                    .Select(x => x.Role.Id).ToList();
            // получаем список ролей, которые были добавлены
            var addedRoles = rolesIdLast.Except(userRoles);
            // получаем роли, которые были удалены
            var removedRoles = userRoles.Except(rolesIdLast);

            // Add new Roles
            foreach (var role in addedRoles)
            {
                _dbContext.AddToSet(new NoteAccess()
                {
                    NoteId = noteId,
                    RoleId = role
                });
            }
            // Delete old roles
            foreach (var role in removedRoles)
            {
                var access = _dbContext.GetQuery<NoteAccess>().FirstOrDefault(x => x.RoleId.ToString() == role && x.NoteId == noteId);
                if (access != null)
                {
                    _dbContext.Entry(access).State = EntityState.Deleted;
                }
            }
        }
    }
}