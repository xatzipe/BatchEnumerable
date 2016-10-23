using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Xatzipe.BatchEnumerable.Multiple
{

    /// <summary>
    ///  Abstract Class that implements the base functionality to take multiple Enumerables and return it int batches
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public abstract class AbstractMultipleBatchEnumerable<TModel, TResult> : AbstractBatchEnumerable<TModel, TResult>, IMultipleBatchEnumerable<TModel, TResult>
    {
        /// <summary>
        /// 
        /// </summary>
        protected IQueryable<TModel>[] ItemList;

        /// <summary>
        /// AbstractMultipleBatchEnumerable contructor
        /// </summary>
        /// <param name="response"></param>
        /// <param name="order"></param>
        /// <param name="filter"></param>
        /// <param name="batchSize"></param>
        public AbstractMultipleBatchEnumerable (
            Expression<Func<TModel, TResult>> response,
            Func<IQueryable<TModel>, IOrderedQueryable<TModel>> order = null,
            Expression<Func<TModel, bool>> filter = null,
            int batchSize = 10
        ) : base(response, order, filter, batchSize)
        {
        }
    }
}
