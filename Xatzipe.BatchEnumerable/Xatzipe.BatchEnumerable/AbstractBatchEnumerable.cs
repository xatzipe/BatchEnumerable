using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Xatzipe.BatchEnumerable
{
    /// <summary>
    /// Abstract Class that implements the base functionality to return items in batches
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public abstract class AbstractBatchEnumerable<TModel, TResult> : IBatchEnumerable<TModel, TResult>
    {

        /// <summary>
        /// the enumerator that object uses to iterate through in batches
        /// </summary>
        protected IBatchEnumerator<TResult> Enumerator;

        /// <summary>
        /// 
        /// </summary>
        protected IQueryable<TModel> Items;

        /// <summary>
        /// 
        /// </summary>
		protected Expression<Func<TModel, bool>> Filter;

        /// <summary>
        /// 
        /// </summary>
		protected Expression<Func<TModel, TResult>> Response;

        /// <summary>
        /// 
        /// </summary>
		protected Func<IQueryable<TModel>, IOrderedQueryable<TModel>> Order;

        /// <summary>
        /// AbstractBatchEnumerable  constructor
        /// </summary>
        /// <param name="items"></param>
        /// <param name="response"></param>
        /// <param name="order"></param>
        /// <param name="filter"></param>
        /// <param name="batchSize"></param>
        public AbstractBatchEnumerable (
            IQueryable<TModel> items,
            Expression<Func<TModel, TResult>> response,
            Func<IQueryable<TModel>, IOrderedQueryable<TModel>> order = null,
            Expression<Func<TModel, bool>> filter = null,
            int batchSize = 10
        )
        {
            Items = items;
            Response = response;
            Order = order;
            Filter = filter;
            SetEnumerator(out Enumerator, items, response, order, filter, batchSize);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumerator"></param>
        /// <param name="items"></param>
        /// <param name="response"></param>
        /// <param name="order"></param>
        /// <param name="filter"></param>
        /// <param name="batchSize"></param>
        protected abstract void SetEnumerator (
            out IBatchEnumerator<TResult> enumerator,
            IQueryable<TModel> items,
            Expression<Func<TModel, TResult>> response,
            Func<IQueryable<TModel>, IOrderedQueryable<TModel>> order = null,
            Expression<Func<TModel, bool>> filter = null,
            int batchSize = 10
        );

        /// <summary>
        /// returns the current batch number 
        /// </summary>
        public int BatchNumber {
            get { return Enumerator.BatchNumber; }
        }

        /// <summary>
        /// returns the size of the batch
        /// </summary>
        public int BatchSize {
            get { return Enumerator.BatchSize; }
        }

        /// <summary>
        /// returns the enumerator
        /// </summary>
        /// <returns></returns>
        public IEnumerator<IEnumerable<TResult>> GetEnumerator ()
        {
            if (Enumerator.Disposed) {
                SetEnumerator(out Enumerator, Items, Response, Order, Filter, BatchSize);
            }
            return Enumerator;
        }

        /// <summary>
        /// returns the enumerator
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator ()
        {
            return GetEnumerator();
        }
    }
}
