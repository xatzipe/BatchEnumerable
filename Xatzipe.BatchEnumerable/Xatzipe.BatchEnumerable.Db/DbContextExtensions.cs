using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Xatzipe.BatchEnumerable.Db
{
    /// <summary>
    /// Extention methods for DbContext
    /// </summary>
    public static class DbContextExtensions
    {

        /// <summary>
        /// Batch the specified cntx, orderBy, selectedColumns, batchSize and filter
        /// </summary>
        /// <param name="cntx">Cntx</param>
        /// <param name="orderBy">Order by</param>
        /// <param name="selectedColumns">Selected columns</param>
        /// <param name="batchSize">Batch size</param>
        /// <param name="filter">Filter</param>
        /// <typeparam name="TEntity">The 1st type parameter</typeparam>
        /// <typeparam name="TResult">The 2nd type parameter</typeparam>
        public static IBatchEnumerable<TEntity, TResult> Batch<TEntity, TResult> (
            this DbContext cntx,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            Expression<Func<TEntity, TResult>> selectedColumns,
            Expression<Func<TEntity, bool>> filter = null,
            int batchSize = 10
        ) where TEntity : class
        {
            return new BatchEnumerable<TEntity, TResult>(cntx.Set<TEntity>(), selectedColumns, orderBy, filter, batchSize);
        }

        /// <summary>
        /// Batch the specified cntx, orderBy, selectedColumns, batchSize and filter
        /// </summary>
        /// <param name="cntx">Cntx.</param>
        /// <param name="orderBy">Order by</param>
        /// <param name="batchSize">Batch size</param>
        /// <param name="filter">Filter</param>
        /// <typeparam name="TEntity">The 1st type parameter</typeparam>
        public static IBatchEnumerable<TEntity> Batch<TEntity> (
            this DbContext cntx,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            Expression<Func<TEntity, bool>> filter = null,
            int batchSize = 10
        ) where TEntity : class
        {
            return new BatchEnumerable<TEntity>(cntx.Set<TEntity>(), orderBy, filter, batchSize);
        }
    }
}
