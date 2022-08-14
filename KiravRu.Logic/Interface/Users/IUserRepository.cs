using KiravRu.Logic.Domain.Users;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace KiravRu.Logic.Interface.Users
{
    public interface IUserRepository
    {
        Task<User> GetById(string id, CancellationToken ct);
        Task<List<User>> GetAll(CancellationToken ct);
    }
}
