using KiravRu.Logic.Domain.Users;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace KiravRu.Logic.Interface.Users
{
    public interface IRoleRepository
    {
        Task<string[]> GetRoleIdsByRoleNamesAsync(IList<string> roles, CancellationToken ct);
        Task<List<Role>> GetRolesAsync(CancellationToken ct);
    }
}
