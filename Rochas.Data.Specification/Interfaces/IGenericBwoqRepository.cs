using System.Threading.Tasks;

namespace Rochas.Data.Specification.Interfaces
{
    /// <summary>
    /// Repository contract that exposes write persistence (IPersistenceRepository) +
    /// read (Get/GetSync) + BWOQ-style query composition (Q/W/G/O/OD).
    /// Reduced surface compared to IGenericRepository — no Search/Query/OrderBy/GroupBy/Count/QueryRaw.
    /// </summary>
    public interface IGenericBwoqRepository<T> : IPersistenceRepository<T> where T : class
    {
        // ── READ ─────────────────────────────────────────────────────

        Task<T> Get(object key, bool loadComposition = false);
        T GetSync(object key, bool loadComposition = false);

        Task<T> Get(T filter, bool loadComposition = false);
        T GetSync(T filter, bool loadComposition = false);

        // ── BWOQ QUERY ───────────────────────────────────────────────

        IBwoqQueryBuilder<T> QueryBwoq();
    }
}
