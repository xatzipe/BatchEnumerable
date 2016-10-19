using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Xatzipe.BatchEnumerable
{
    /// <summary>
    /// IEnumerable extension to iterate in batches
    /// </summary>
    public static class Enumerable
    {

        /// <summary>
        /// Batch the specified items, selectedColumns, orderBy, batchSize and filter
        /// </summary>
        /// <param name="items">Items</param>
        /// <param name="response">Selected columns</param>
        /// <param name="order">Order by</param>
        /// <param name="batchSize">Batch size</param>
        /// <param name="filter">Filter</param>
        /// <typeparam name="TModel">The Input type parameter</typeparam>
        /// <typeparam name="TResult">The Output type parameter</typeparam>
        public static IBatchEnumerable<TModel, TResult> Batch<TModel, TResult> (
            this IEnumerable<TModel> items,
            Expression<Func<TModel, TResult>> response,
            Func<IQueryable<TModel>, IOrderedQueryable<TModel>> order = null,
            Expression<Func<TModel, bool>> filter = null,
            int batchSize = 10
        )
        {
            return new BatchEnumerable<TModel, TResult>(items.AsQueryable(), response, order, filter, batchSize);
        }

        /// <summary>
        /// Batch the specified items, orderBy, batchSize and filter.
        /// </summary>
        /// <param name="items">Items.</param>
        /// <param name="order">Order by.</param>
        /// <param name="batchSize">Batch size.</param>
        /// <param name="filter">Filter.</param>
        /// <typeparam name="TResult">The 1st type parameter.</typeparam>
        public static IBatchEnumerable<TResult> Batch<TResult> (
            this IEnumerable<TResult> items,
            Func<IQueryable<TResult>, IOrderedQueryable<TResult>> order = null,
            Expression<Func<TResult, bool>> filter = null,
            int batchSize = 10
        )
        {
            return new BatchEnumerable<TResult>(items.AsQueryable(), order, filter, batchSize);
        }
    }
}
