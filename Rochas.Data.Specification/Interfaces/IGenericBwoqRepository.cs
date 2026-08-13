namespace Rochas.Data.Specification.Interfaces
{
    /// <summary>
    /// Repository contract that exposes persistence plus BWOQ-style query composition,
    /// allowing callers to shape queries with the BWOQ grammar (Q/W/G/O/OD) directly
    /// against the repository — alongside the persistence annotations of the entity.
    /// </summary>
    public interface IGenericBwoqRepository<T> : IPersistenceRepository<T> where T : class
    {
        IBwoqQueryBuilder<T> QueryBwoq();
    }
}