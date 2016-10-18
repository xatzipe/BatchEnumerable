using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Xatzipe.BatchEnumerable
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public class BatchEnumerable<TModel, TResult> : AbstractBatchEnumerable<TModel, TResult>, IBatchEnumerable<TModel, TResult>
    {

        /// <summary>
        /// BatchEnumerable contructor
        /// </summary>
        /// <param name="items"></param>
        /// <param name="response"></param>
        /// <param name="order"></param>
        /// <param name="filter"></param>
        /// <param name="pageSize"></param>
        public BatchEnumerable (
            IQueryable<TModel> items,
            Expression<Func<TModel, TResult>> response,
            Func<IQueryable<TModel>, IOrderedQueryable<TModel>> order = null,
            Expression<Func<TModel, bool>> filter = null,
            int pageSize = 10
        ) : base(
            items,
            response,
            order,
            filter,
            pageSize
        )
        {
        }

        /// <summary>
        /// abstract SetEnumeration Implementation
        /// </summary>
        /// <param name="enumerator"></param>
        /// <param name="items"></param>
        /// <param name="response"></param>
        /// <param name="order"></param>
        /// <param name="filter"></param>
        /// <param name="batchSize"></param>
        protected override void SetEnumerator (
            out IBatchEnumerator<TResult> enumerator,
            IQueryable<TModel> items,
            Expression<Func<TModel, TResult>> response,
            Func<IQueryable<TModel>, IOrderedQueryable<TModel>> order = null,
            Expression<Func<TModel, bool>> filter = null,
            int batchSize = 10
            )
        {
            enumerator = new BatchEnumerator<TModel, TResult>(items, response, order, filter, batchSize);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class BatchEnumerable<TModel> : AbstractBatchEnumerable<TModel, TModel>, IBatchEnumerable<TModel>
    {

        /// <summary>
        /// BatchEnumerable contructor
        /// </summary>
        /// <param name="items"></param>
        /// <param name="response"></param>
        /// <param name="order"></param>
        /// <param name="filter"></param>
        /// <param name="pageSize"></param>
        public BatchEnumerable (
            IQueryable<TModel> items,
            Func<IQueryable<TModel>, IOrderedQueryable<TModel>> order = null,
            Expression<Func<TModel, bool>> filter = null,
            int pageSize = 10
        ) : base(
            items,
            r => r,
            order,
            filter,
            pageSize
        )
        {
        }

        /// <summary>
        /// abstract SetEnumeration Implementation
        /// </summary>
        /// <param name="enumerator"></param>
        /// <param name="items"></param>
        /// <param name="response"></param>
        /// <param name="order"></param>
        /// <param name="filter"></param>
        /// <param name="batchSize"></param>
        protected override void SetEnumerator (
            out IBatchEnumerator<TModel> enumerator,
            IQueryable<TModel> items,
            Expression<Func<TModel, TModel>> response,
            Func<IQueryable<TModel>, IOrderedQueryable<TModel>> order = null,
            Expression<Func<TModel, bool>> filter = null,
            int batchSize = 10
            )
        {
            enumerator = new BatchEnumerator<TModel>(items, response, order, filter, batchSize);
        }
    }
}
