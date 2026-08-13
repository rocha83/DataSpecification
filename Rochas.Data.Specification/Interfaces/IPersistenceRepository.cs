using System.Collections.Generic;
using System.Threading.Tasks;
using Rochas.Data.Specification.Models;

namespace Rochas.Data.Specification.Interfaces
{
    /// <summary>
    /// Base contract for the Rochas data toolkit: pure persistence operations
    /// (write/read-by-key/count/raw-query). Query shaping (Search/Query builders)
    /// lives in derived contracts (IGenericRepository, IGenericBwoqRepository).
    /// </summary>
    public interface IPersistenceRepository<T> where T : class
    {
        void Initialize(string databaseFileName, string tableScript);

        // ── PERSISTÊNCIA ─────────────────────────────────────────────

        Task<int> Count(T filterEntity);
        int CountSync(T filterEntity);

        Task<int> Add(T entity, bool persistComposition = false);
        int AddSync(T entity, bool persistComposition = false);

        Task AddRange(IEnumerable<T> entities, bool persistComposition = false);
        void AddRangeSync(IEnumerable<T> entities, bool persistComposition = false);

        Task<int> Remove(T filterEntity);
        int RemoveSync(T filterEntity);

        Task<int> Update(T entity, T filterEntity, bool persistComposition = false);
        int UpdateSync(T entity, T filterEntity, bool persistComposition = false);

        // ── GET ──────────────────────────────────────────────────────

        Task<T> Get(object key, bool loadComposition = false);
        T GetSync(object key, bool loadComposition = false);

        Task<T> Get(T filter, bool loadComposition = false);
        T GetSync(T filter, bool loadComposition = false);

        // ── QUERY RAW ────────────────────────────────────────────────

        Task<ICollection<T>> QueryRaw(string sql, Dictionary<string, object> parameters);
        ICollection<T> QueryRawSync(string sql, Dictionary<string, object> parameters);

        Task<PaginatedResult<T>> QueryRaw(string sql, string countSql, Dictionary<string, object> parameters, int page = 1, int pageSize = 20);
        PaginatedResult<T> QueryRawSync(string sql, string countSql, Dictionary<string, object> parameters, int page = 1, int pageSize = 20);

        void Dispose();
    }
}
