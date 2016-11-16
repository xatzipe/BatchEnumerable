using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Xatzipe.BatchEnumerable.Multiple
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public class MultipleBatchEnumerable<TModel, TResult> : AbstractMultipleBatchEnumerable<TModel, TResult>, IMultipleBatchEnumerable<TModel, TResult>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemList"></param>
        /// <param name="response"></param>
        /// <param name="order"></param>
        /// <param name="filter"></param>
        /// <param name="batchSize"></param>
        public MultipleBatchEnumerable (
            IEnumerable<IQueryable<TModel>> itemList,
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
            ItemList = itemList.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumerator"></param>
        protected override void SetEnumerator (out IBatchEnumerator<TResult> enumerator)
        {
            enumerator = new MultipleBatchEnumerator<TModel, TResult>(ItemList, Response, Order, Filter, BatchSizeLocal);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class MultipleBatchEnumerable<TModel> : AbstractMultipleBatchEnumerable<TModel, TModel>, IMultipleBatchEnumerable<TModel>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <param name="filter"></param>
        /// <param name="batchSize"></param>
        public MultipleBatchEnumerable (
            Func<IQueryable<TModel>, IOrderedQueryable<TModel>> order = null,
            Expression<Func<TModel, bool>> filter = null,
            int batchSize = 10
        ) : base(
            r => r,
            order,
            filter,
            batchSize
        )
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumerator"></param>
        protected override void SetEnumerator (out IBatchEnumerator<TModel> enumerator)
        {
            enumerator = new MultipleBatchEnumerator<TModel>(ItemList, Response, Order, Filter, BatchSizeLocal);
        }
    }
}
