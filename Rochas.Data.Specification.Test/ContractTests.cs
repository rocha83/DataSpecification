using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rochas.Data.Specification.Interfaces;
using Rochas.Data.Specification.Models;
using Xunit;

namespace Rochas.Data.Specification.Test
{
    public class ContractTests
    {
        [Fact]
        public void IPersistenceRepository_IsBaseOf_IGenericRepository()
        {
            Assert.True(typeof(IPersistenceRepository<object>).IsAssignableFrom(typeof(IGenericRepository<object>)));
        }

        [Fact]
        public void IPersistenceRepository_IsBaseOf_IGenericBwoqRepository()
        {
            Assert.True(typeof(IPersistenceRepository<object>).IsAssignableFrom(typeof(IGenericBwoqRepository<object>)));
        }

        [Fact]
        public void IGenericBwoqRepository_QueryBwoq_Returns_BwoqBuilder()
        {
            var builderMethod = typeof(IGenericBwoqRepository<object>).GetMethod(nameof(IGenericBwoqRepository<object>.QueryBwoq));
            Assert.NotNull(builderMethod);
            Assert.Equal(typeof(IBwoqQueryBuilder<object>), builderMethod.ReturnType);
        }

        [Fact]
        public void IBwoqQueryBuilder_ExposesGrammarMethods()
        {
            var type = typeof(IBwoqQueryBuilder<object>);
            Assert.NotNull(type.GetMethod("Q", new[] { typeof(string) }));
            Assert.NotNull(type.GetMethod("W", new[] { typeof(string) }));
            Assert.NotNull(type.GetMethod("G", new[] { typeof(string), typeof(string) }));
            Assert.NotNull(type.GetMethod("O", new[] { typeof(string) }));
            Assert.NotNull(type.GetMethod("OD", new[] { typeof(string) }));
            Assert.NotNull(type.GetMethod("ToQuery", Type.EmptyTypes));
            Assert.NotNull(type.GetMethod("ToQuerySync", Type.EmptyTypes));
            Assert.NotNull(type.GetMethod("ToQuery", new[] { typeof(int), typeof(int) }));
        }
    }
}