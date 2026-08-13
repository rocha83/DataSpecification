namespace Rochas.Data.Specification.Enums
{
    /// <summary>
    /// Seção do documento indexável a que uma propriedade [Indexable] pertence.
    /// Title costuma ter peso maior no ranking; Body complementa o conteúdo.
    /// </summary>
    public enum IndexSection
    {
        Title = 1,
        Body = 2
    }
}