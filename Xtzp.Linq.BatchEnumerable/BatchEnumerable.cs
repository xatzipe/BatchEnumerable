using System;
using System.Linq;
using System.Linq.Expressions;

namespace Xtzp.Linq.BatchEnumerable
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
        /// <param name="batchSize"></param>
        public BatchEnumerable (
            IQueryable<TModel> items,
            Expression<Func<TModel, TResult>> response,
            Func<IQueryable<TModel>, IOrderedQueryable<TModel>> order = null,
            Expression<Func<TModel, bool>> filter = null,
            int batchSize = 10
        ) : base(
            response,
            order,
            filter,
            batchSize
        )
        {
            if (null == items) {
                throw new ArgumentNullException("Items cannot be empty");
            }
            Items = items;
        }

        /// <summary>
        /// abstract SetEnumeration Implementation
        /// </summary>
        /// <param name="enumerator"></param>
        protected override void SetEnumerator (out IBatchEnumerator<TResult> enumerator)
        {
            enumerator = new BatchEnumerator<TModel, TResult>(Items, Response, Order, Filter, BatchSizeLocal);
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
        /// <param name="order"></param>
        /// <param name="filter"></param>
        /// <param name="pageSize"></param>
        public BatchEnumerable (
            IQueryable<TModel> items,
            Func<IQueryable<TModel>, IOrderedQueryable<TModel>> order = null,
            Expression<Func<TModel, bool>> filter = null,
            int pageSize = 10
        ) : base(
            r => r,
            order,
            filter,
            pageSize
        )
        {
            if (null == items) {
                throw new ArgumentNullException("Items cannot be empty");
            }
            Items = items;
        }

        /// <summary>
        /// abstract SetEnumeration Implementation
        /// </summary>
        /// <param name="enumerator"></param>
        protected override void SetEnumerator (out IBatchEnumerator<TModel> enumerator)
        {
            enumerator = new BatchEnumerator<TModel>(Items, Response, Order, Filter, BatchSizeLocal);
        }
    }
}
