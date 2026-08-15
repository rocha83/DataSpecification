using System.Collections.Generic;
using Rochas.Data.Specification.Enums;
using Rochas.Data.Specification.Models;
using Xunit;

namespace Rochas.Data.Specification.Test
{
    public class ModelTests
    {
        [Fact]
        public void GroupResult_ParameterlessCtor_InitializesEmptyItems()
        {
            var result = new GroupResult<int, object>();

            Assert.NotNull(result.Items);
            Assert.Empty(result.Items);
        }

        [Fact]
        public void GroupResult_FullCtor_SetsKeyItemsAndAggregates()
        {
            var items = new List<object> { new object(), new object() };
            var aggregates = new Dictionary<decimal, DataAggregationType> { [100.5m] = DataAggregationType.Sum };
            var result = new GroupResult<string, object>("Category", items, aggregates);

            Assert.Equal("Category", result.Key);
            Assert.Same(items, result.Items);
            Assert.Same(aggregates, result.Aggregates);
        }

        [Fact]
        public void GroupResult_FullCtor_WhenAggregatesOmitted_LeavesAggregatesNull()
        {
            var result = new GroupResult<int, object>(1, new List<object>());

            Assert.Equal(1, result.Key);
            Assert.Null(result.Aggregates);
        }

        [Fact]
        public void PaginatedResult_ParameterlessCtor_InitializesEmptyItems()
        {
            var result = new PaginatedResult<int>();

            Assert.NotNull(result.Items);
            Assert.Empty(result.Items);
            Assert.Equal(0, result.TotalCount);
            Assert.Equal(0, result.Page);
            Assert.Equal(0, result.PageSize);
        }

        [Fact]
        public void PaginatedResult_FullCtor_SetsAllProperties()
        {
            var items = new List<string> { "a", "b", "c" };
            var result = new PaginatedResult<string>(items, totalCount: 103, page: 3, pageSize: 10);

            Assert.Same(items, result.Items);
            Assert.Equal(103, result.TotalCount);
            Assert.Equal(3, result.Page);
            Assert.Equal(10, result.PageSize);
        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(25, 10, 3)]
        [InlineData(20, 10, 2)]
        [InlineData(5, 10, 1)]
        [InlineData(100, 20, 5)]
        public void PaginatedResult_PageCount_ComputesCeilingUpward(int totalCount, int pageSize, int expectedPageCount)
        {
            var result = new PaginatedResult<object>(new List<object>(), totalCount, 1, pageSize);

            Assert.Equal(expectedPageCount, result.PageCount);
        }

        [Fact]
        public void PaginatedResult_PageCount_WithZeroPageSize_ReturnsZero()
        {
            var result = new PaginatedResult<object>(new List<object>(), 10, 1, 0);

            Assert.Equal(0, result.PageCount);
        }
    }
}