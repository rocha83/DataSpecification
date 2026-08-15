using System;
using System.Linq;
using Rochas.Data.Specification.Annotations;
using Rochas.Data.Specification.Enums;
using Rochas.Data.Specification.Interfaces;
using Xunit;

namespace Rochas.Data.Specification.Test
{
    public class AnnotationTests
    {
        private sealed class FakeCacheProvider : ICacheProvider
        {
            public object Get(object cacheKey) => null;
            public void Put(object cacheKey, object cacheItem) { }
            public void Del(object cacheKey, bool deleteAll = false) { }
            public void Clear() { }
        }

        private sealed class FakeNonCacheProvider
        {
        }

        [Fact]
        public void CacheableAttribute_ParameterlessCtor_LeavesProviderTypeNull()
        {
            var attribute = new CacheableAttribute();

            Assert.Null(attribute.CacheProviderType);
        }

        [Fact]
        public void CacheableAttribute_WithProviderType_StoresProviderType()
        {
            var attribute = new CacheableAttribute(typeof(FakeCacheProvider));

            Assert.Equal(typeof(FakeCacheProvider), attribute.CacheProviderType);
        }

        [Fact]
        public void CacheableAttribute_WithNullProviderType_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new CacheableAttribute(null));
        }

        [Fact]
        public void CacheableAttribute_WithTypeThatDoesNotImplementICacheProvider_ThrowsArgumentException()
        {
            var exception = Assert.Throws<ArgumentException>(
                () => new CacheableAttribute(typeof(FakeNonCacheProvider)));

            Assert.Contains("ICacheProvider", exception.Message);
        }

        [Fact]
        public void RelatedEntityAttribute_Defaults_HaveExpectedValues()
        {
            var attribute = new RelatedEntityAttribute();

            Assert.Equal(default(RelationCardinality), attribute.Cardinality);
            Assert.Null(attribute.ForeignKeyAttribute);
            Assert.Null(attribute.IntermediaryEntity);
            Assert.Null(attribute.IntermediaryKeyAttribute);
        }

        [Fact]
        public void RelatedEntityAttribute_GetRelationCardinality_ReturnsConfiguredCardinality()
        {
            var attribute = new RelatedEntityAttribute { Cardinality = RelationCardinality.ManyToMany };

            Assert.Equal(RelationCardinality.ManyToMany, attribute.GetRelationCardinality());
        }

        [Fact]
        public void RelatedEntityAttribute_GetIntermediaryEntity_ReturnsConfiguredType()
        {
            var attribute = new RelatedEntityAttribute { IntermediaryEntity = typeof(FakeNonCacheProvider) };

            Assert.Equal(typeof(FakeNonCacheProvider), attribute.GetIntermediaryEntity());
        }

        [Fact]
        public void RelatedEntityAttribute_GetIntermediaryKeyAttribute_ReturnsConfiguredName()
        {
            var attribute = new RelatedEntityAttribute { IntermediaryKeyAttribute = "CategoryId" };

            Assert.Equal("CategoryId", attribute.GetIntermediaryKeyAttribute());
        }

        [Fact]
        public void RelatedEntityAttribute_ExposesRelatedEntityContractMethods()
        {
            var attribute = new RelatedEntityAttribute { Cardinality = RelationCardinality.OneToMany };
            var interfaceNames = typeof(RelatedEntityAttribute).GetInterfaces().Select(i => i.Name);

            Assert.Contains("IRelatedEntity", interfaceNames);
            Assert.Equal(RelationCardinality.OneToMany, attribute.GetRelationCardinality());
        }

        [Fact]
        public void RelationalColumn_GetColumnName_ReturnsConfiguredName()
        {
            var column = new RelationalColumn { ColumnName = "Description" };

            Assert.Equal("Description", column.GetColumnName());
        }

        [Fact]
        public void RelationalColumn_Defaults_HaveExpectedValues()
        {
            var column = new RelationalColumn();

            Assert.Null(column.TableName);
            Assert.Null(column.IntermediaryColumnName);
            Assert.Null(column.ColumnName);
            Assert.Null(column.ColumnAlias);
            Assert.Null(column.KeyColumn);
            Assert.Null(column.ForeignKeyColumn);
            Assert.Null(column.IntermediaryColumnKey);
            Assert.False(column.Filterable);
        }

        [Fact]
        public void IndexableAttribute_Section_DefaultsToTitle()
        {
            var attribute = new IndexableAttribute();

            Assert.Equal(IndexSection.Title, attribute.Section);
        }

        [Fact]
        public void IndexableAttribute_Section_CanBeChangedToBody()
        {
            var attribute = new IndexableAttribute { Section = IndexSection.Body };

            Assert.Equal(IndexSection.Body, attribute.Section);
        }

        [Fact]
        public void RangeFilterAttribute_LinkedRangeProperty_CanBeSetAndRead()
        {
            var attribute = new RangeFilterAttribute { LinkedRangeProperty = "EndDate" };

            Assert.Equal("EndDate", attribute.LinkedRangeProperty);
        }

        [Fact]
        public void DataAggregationColumn_Fields_CanBeSetAndRead()
        {
            var column = new DataAggregationColumn
            {
                ColumnName = "Total",
                AggregationType = DataAggregationType.Sum
            };

            Assert.Equal("Total", column.ColumnName);
            Assert.Equal(DataAggregationType.Sum, column.AggregationType);
        }

        [Fact]
        public void AutoGeneratedAttribute_WithoutCustomUsage_UsesDefaultAllTargets()
        {
            var type = typeof(AutoGeneratedAttribute);
            var usage = (AttributeUsageAttribute)Attribute.GetCustomAttribute(type, typeof(AttributeUsageAttribute));

            Assert.NotNull(usage);
            Assert.Equal(AttributeTargets.All, usage.ValidOn);
            Assert.False(usage.AllowMultiple);
            Assert.True(usage.Inherited);
        }

        [Fact]
        public void ListableFilterableAreDecorators_WithoutCustomUsage_UsesDefaultAllTargets()
        {
            var listable = (AttributeUsageAttribute)Attribute.GetCustomAttribute(
                typeof(ListableAttribute), typeof(AttributeUsageAttribute));
            var filterable = (AttributeUsageAttribute)Attribute.GetCustomAttribute(
                typeof(FilterableAttribute), typeof(AttributeUsageAttribute));

            Assert.Equal(AttributeTargets.All, listable.ValidOn);
            Assert.Equal(AttributeTargets.All, filterable.ValidOn);
        }
    }
}