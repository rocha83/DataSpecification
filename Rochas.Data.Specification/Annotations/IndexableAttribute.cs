using System;
using Rochas.Data.Specification.Enums;

namespace Rochas.Data.Specification.Annotations
{
    /// <summary>
    /// Marca uma propriedade da entidade como texto indexável por um indexador
    /// lexical (ex.: Rochas.CacheIndexer). Somente propriedades anotadas são
    /// consideradas no índice.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class IndexableAttribute : Attribute
    {
        public IndexSection Section { get; set; } = IndexSection.Title;
    }
}