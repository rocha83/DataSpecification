using System.Collections.Generic;
using System.Threading.Tasks;
using Rochas.Data.Specification.Enums;
using Rochas.Data.Specification.Models;

namespace Rochas.Data.Specification.Interfaces
{
    public interface IGenericRepository<T> : IPersistenceRepository<T> where T : class
    {
        // ── SEARCH (builder) ─────────────────────────────────────────

        IQueryBuilder<T> Search(object criteria, bool loadComposition = false, bool filterConjunction = false);
        IQuerySyncBuilder<T> SearchSync(object criteria, bool loadComposition = false, bool filterConjunction = false);
        IQueryPaginatedBuilder<T> Search(object criteria, int page, int pageSize, bool loadComposition = false, bool filterConjunction = false);
        IQueryPaginatedBuilder<T> SearchSync(object criteria, int page, int pageSize, bool loadComposition = false, bool filterConjunction = false);

        // ── BULK SEARCH ─────────────────────────────────────────────

        ICollection<T> BulkSearch(object[] criterias, bool loadComposition = false, int recordsLimit = 0, string sortAttributes = null, bool orderDescending = false);
        ICollection<T> BulkSearchSync(object[] criterias, bool loadComposition = false, int recordsLimit = 0, string sortAttributes = null, bool orderDescending = false);

        // ── QUERY (builder) ──────────────────────────────────────────

        IQueryBuilder<T> Query(T filter, bool loadComposition = false, bool filterConjunction = false);
        IQueryPaginatedBuilder<T> Query(T filter, int page, int pageSize, bool loadComposition = false, bool filterConjunction = false);
        IQueryBuilder<T> OrderBy(params string[] sortAttributes);
        IQueryBuilder<T> OrderByDescending(params string[] sortAttributes);
        IQueryBuilder<T> GroupBy(string[] groupAttributes, Dictionary<string, DataAggregationType> aggregates = null);

        // ── QUERY (sync builder) ─────────────────────────────────────

        IQuerySyncBuilder<T> QuerySync(T filter, bool loadComposition = false, bool filterConjunction = false);
        IQueryPaginatedBuilder<T> QuerySync(T filter, int page, int pageSize, bool loadComposition = false, bool filterConjunction = false);
    }
}