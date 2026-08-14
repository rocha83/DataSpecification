using System.Collections.Generic;
using System.Threading.Tasks;
using Rochas.Data.Specification.Enums;
using Rochas.Data.Specification.Models;

namespace Rochas.Data.Specification.Interfaces
{
    /// <summary>
    /// Full generic repository: write persistence (IPersistenceRepository) +
    /// read (Get/GetSync) + query builders (Search/Query/OrderBy/GroupBy/Count/QueryRaw).
    /// </summary>
    public interface IGenericRepository<T> : IPersistenceRepository<T> where T : class
    {
        // ── READ ─────────────────────────────────────────────────────

        Task<T> Get(object key, bool loadComposition = false);
        T GetSync(object key, bool loadComposition = false);

        Task<T> Get(T filter, bool loadComposition = false);
        T GetSync(T filter, bool loadComposition = false);

        // ── COUNT / RAW ──────────────────────────────────────────────

        Task<int> Count(T filterEntity);
        int CountSync(T filterEntity);

        Task<ICollection<T>> QueryRaw(string sql, Dictionary<string, object> parameters);
        ICollection<T> QueryRawSync(string sql, Dictionary<string, object> parameters);

        Task<PaginatedResult<T>> QueryRaw(string sql, string countSql, Dictionary<string, object> parameters, int page = 1, int pageSize = 20);
        PaginatedResult<T> QueryRawSync(string sql, string countSql, Dictionary<string, object> parameters, int page = 1, int pageSize = 20);

        // ── SEARCH (builder) ─────────────────────────────────────────

        IQueryBuilder<T> Search(object criteria, bool loadComposition = false, bool filterConjunction = false);
        IQuerySyncBuilder<T> SearchSync(object criteria, bool loadComposition = false, bool filterConjunction = false);

        IQueryPaginatedBuilder<T> Search(object criteria, int page, int pageSize, bool loadComposition = false, bool filterConjunction = false);
        IQueryPaginatedBuilder<T> SearchSync(object criteria, int page, int pageSize, bool loadComposition = false, bool filterConjunction = false);

        ICollection<T> BulkSearch(object[] criterias, bool loadComposition = false, int recordsLimit = 0, string sortAttributes = null, bool orderDescending = false);
        ICollection<T> BulkSearchSync(object[] criterias, bool loadComposition = false, int recordsLimit = 0, string sortAttributes = null, bool orderDescending = false);

        // ── QUERY (builder) ──────────────────────────────────────────

        IQueryBuilder<T> Query(T filter, bool loadComposition = false, bool filterConjunction = false);
        IQueryPaginatedBuilder<T> Query(T filter, int page, int pageSize, bool loadComposition = false, bool filterConjunction = false);

        IQuerySyncBuilder<T> QuerySync(T filter, bool loadComposition = false, bool filterConjunction = false);
        IQueryPaginatedBuilder<T> QuerySync(T filter, int page, int pageSize, bool loadComposition = false, bool filterConjunction = false);

        // ── ORDER BY (builder) ───────────────────────────────────────

        IQueryBuilder<T> OrderBy(params string[] sortAttributes);
        IQueryBuilder<T> OrderByDescending(params string[] sortAttributes);

        // ── GROUP BY (builder) ───────────────────────────────────────

        IQueryBuilder<T> GroupBy(string[] groupAttributes, Dictionary<string, DataAggregationType> aggregates = null);
    }
}
