using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Xtzp.Linq.BatchEnumerable
{
    /// <summary>
    /// BatchEnumerator{TModel, TResult}
    /// </summary>
    public class BatchEnumerator<TModel, TResult> : AbstractBatchEnumerator<TModel, TResult>, IBatchEnumerator<TModel, TResult>
    {

        
        /// <summary>
        /// Batch enumerator contructor
        /// </summary>
        /// <param name="items"></param>
        /// <param name="response"></param>
        /// <param name="order"></param>
        /// <param name="filter"></param>
        /// <param name="batchSize"></param>
        public BatchEnumerator (
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
                throw new ArgumentNullException(nameof(items), "Items cannot be empty");
            }
            Items = items;
        }

        /// <summary>
        /// Move next IEnumerable implementation
        /// </summary>
        /// <returns></returns>
        public override bool MoveNext ()
        {
            CurrentBatchIndex++;
            CurrentItems = GetBatch(Items, Response, Order, Filter, CurrentBatchIndex, BatchSizeLocal);
            if (CurrentItems.Count() == 0) {
                return false;
            }
            return true;
        }
    }

    /// <summary>
    /// BatchEnumerator{TModel}
    /// </summary>
    public class BatchEnumerator<TModel> : BatchEnumerator<TModel, TModel>, IBatchEnumerator<TModel, TModel>
    {

        /// <summary>
        /// Batch enumerator contructor
        /// </summary>
        /// <param name="items"></param>
        /// <param name="response"></param>
        /// <param name="order"></param>
        /// <param name="filter"></param>
        /// <param name="batchSize"></param>
        public BatchEnumerator (
            IQueryable<TModel> items,
            Expression<Func<TModel, TModel>> response,
            Func<IQueryable<TModel>, IOrderedQueryable<TModel>> order = null,
            Expression<Func<TModel, bool>> filter = null,
            int batchSize = 10
            ) : base(
            items,
            response,
            order,
            filter,
            batchSize
        )
        {
        }
    }
}