using KiravRu.Logic.Domain.Constants;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace KiravRu.Logic.Interface.Constants
{
    public interface IConstantRepository : IRepository<Constant>
    {
        Task<List<Constant>> GetConstantsAsync(CancellationToken ct);
        Task<Constant> GetConstantByIdAsync(int Id, CancellationToken ct);
        Task<int?> GetValueAsync(string name, CancellationToken ct);
    }
}
