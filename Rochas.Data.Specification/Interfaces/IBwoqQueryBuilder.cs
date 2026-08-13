using Rochas.Data.Specification.Enums;

namespace Rochas.Data.Specification.Interfaces
{
    /// <summary>
    /// BWOQ-style query composer. Methods mirror the BWOQ grammar (Q/W/G/O/OD) and
    /// resolve to the existing execution builders (IQueryBuilder, IQuerySyncBuilder,
    /// IQueryPaginatedBuilder). This interface only exposes the grammar surface
    /// (string expressions, DataAggregationType, DatabaseEngine); the grammar itself
    /// is interpreted by the BWOQ interpreter inside the concrete repository.
    /// </summary>
    public interface IBwoqQueryBuilder<T> where T : class
    {
        /// <summary>Projeção (Q): máscara raiz, opcionalmente com navegação &gt;ordinal:máscara.</summary>
        IBwoqQueryBuilder<T> Q(string selectExpression);

        /// <summary>Critério (W): mascara::valor[&amp;][operador]. Pode ser encadeado (AND entre cláusulas).</summary>
        IBwoqQueryBuilder<T> W(string whereExpression);

        /// <summary>Agrupamento (G): colunas agregadas (sufixo *^~+-) + colunas de grupo (by).</summary>
        IBwoqQueryBuilder<T> G(string groupExpression, string byExpression);

        /// <summary>Ordenação ascendente (O).</summary>
        IBwoqQueryBuilder<T> O(string orderExpression);

        /// <summary>Ordenação descendente (OD).</summary>
        IBwoqQueryBuilder<T> OD(string orderExpression);

        /// <summary>Resolve a expressão em um builder assíncrono executável (await).</summary>
        IQueryBuilder<T> ToQuery();

        /// <summary>Resolve a expressão em um builder síncrono.</summary>
        IQuerySyncBuilder<T> ToQuerySync();

        /// <summary>Resolve a expressão em um builder paginado assíncrono.</summary>
        IQueryPaginatedBuilder<T> ToQuery(int page, int pageSize);
    }
}