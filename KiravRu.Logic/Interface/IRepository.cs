using KiravRu.Logic.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace KiravRu.Logic.Interface
{
    public interface IRepository<T> where T : IIdentity
    {
        void AddToSet(T item);
        Task SaveChanges(CancellationToken ct);
        void Save(T item);
        void Delete(T item);
    }
}
