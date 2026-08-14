using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rochas.Data.Specification.Interfaces
{
    /// <summary>
    /// Base contract for write persistence: Initialize, Add, AddRange, Remove, Update, Dispose.
    /// Read operations (Get/GetSync) live in derived contracts (IGenericRepository, IGenericBwoqRepository).
    /// </summary>
    public interface IPersistenceRepository<T> where T : class
    {
        void Initialize(string databaseFileName, string tableScript);

        // ── WRITE ────────────────────────────────────────────────────

        Task<int> Add(T entity, bool persistComposition = false);
        int AddSync(T entity, bool persistComposition = false);

        Task AddRange(IEnumerable<T> entities, bool persistComposition = false);
        void AddRangeSync(IEnumerable<T> entities, bool persistComposition = false);

        Task<int> Remove(T filterEntity);
        int RemoveSync(T filterEntity);

        Task<int> Update(T entity, T filterEntity, bool persistComposition = false);
        int UpdateSync(T entity, T filterEntity, bool persistComposition = false);

        void Dispose();
    }
}
