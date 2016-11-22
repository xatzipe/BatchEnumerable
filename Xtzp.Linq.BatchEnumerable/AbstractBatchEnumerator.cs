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
    /// Batch enumerator
    /// </summary>
    public abstract class AbstractBatchEnumerator<TModel, TResult> : IBatchEnumerator<TModel, TResult>
    {

        /// <summary>
        /// 
        /// </summary>
		protected int BatchSizeLocal;

        /// <summary>
        /// 
        /// </summary>
        protected int PaddingLocal;

        /// <summary>
        /// 
        /// </summary>
		protected int CurrentBatchIndex;

        /// <summary>
        /// 
        /// </summary>
		protected IEnumerable<TResult> CurrentItems;

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
        /// 
        /// </summary>
        public bool Disposed { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        /// <param name="order"></param>
        /// <param name="filter"></param>
        /// <param name="batchSize"></param>
        /// <exception cref="ArgumentNullException">if items or selectedResponse are null</exception>"
		public AbstractBatchEnumerator (
            Expression<Func<TModel, TResult>> response,
            Func<IQueryable<TModel>, IOrderedQueryable<TModel>> order = null,
            Expression<Func<TModel, bool>> filter = null,
            int batchSize = 10
        )
        {
            if (null == response) {
                throw new ArgumentNullException(nameof(response), "Selected Response cannot be empty");
            }
            CurrentBatchIndex = 0;
            Order = order;
            Response = response;
            Filter = filter;
            BatchSizeLocal = batchSize;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Padding {
            get {
                return PaddingLocal;
            }
            set {
                if (CurrentBatchIndex > 0) {
                    throw new ArgumentException("Padding cannot change value after iteration has started");
                }
                PaddingLocal = value;
            }
        }

        /// <summary>
        /// returns the current batch number 
        /// </summary>
		public int BatchNumber {
            get { return CurrentBatchIndex; }
        }

        /// <summary>
        /// returns the size of the batch
        /// </summary>
		public int BatchSize {
            get { return BatchSizeLocal; }
        }

        /// <summary>
        /// returns the current batch
        /// </summary>
		public IEnumerable<TResult> Current {
            get { return CurrentItems; }
        }

        /// <summary>
        /// returns the current batch
        /// </summary>
		object IEnumerator.Current {
            get { return Current; }
        }

        /// <summary>
        /// returns true if there is another batch to return 
        /// </summary>
        /// <returns></returns>
		public abstract bool MoveNext ();

        /// <summary>
        /// retrieves a batch
        /// </summary>
        /// <param name="items"></param>
        /// <param name="response"></param>
        /// <param name="order"></param>
        /// <param name="filter"></param>
        /// <param name="batchNumber"></param>
        /// <param name="batchSize"></param>
        /// <returns></returns>
        protected IQueryable<TResult> GetBatch (
            IQueryable<TModel> items,
            Expression<Func<TModel, TResult>> response,
            Func<IQueryable<TModel>, IOrderedQueryable<TModel>> order,
            Expression<Func<TModel, bool>> filter,
            int batchNumber,
            int batchSize
        )
        {
            var skip = BatchSizeLocal * (batchNumber - 1);
            var take = BatchSizeLocal;
            var result = DoGetBatch(items, response, order, filter, skip, take);
            return result;
        }



        /// <summary>
        /// retrieves a batch using a padding
        /// the first batch returns only {padding} number of items
        /// </summary>
        /// <param name="items"></param>
        /// <param name="response"></param>
        /// <param name="order"></param>
        /// <param name="filter"></param>
        /// <param name="batchNumber"></param>
        /// <param name="batchSize"></param>
        /// <param name="padding"></param>
        /// <returns></returns>
        protected IQueryable<TResult> GetBatchWithPadding (
            IQueryable<TModel> items,
            Expression<Func<TModel, TResult>> response,
            Func<IQueryable<TModel>, IOrderedQueryable<TModel>> order,
            Expression<Func<TModel, bool>> filter,
            int batchNumber,
            int batchSize,
            int padding
        )
        {
            if (0 == padding) {
                return GetBatch(items, response, order, filter, batchNumber, batchSize);
            }
            int skip = 0;
            int take = 0;
            if (1 == batchNumber) {
                skip = 0;
                take = padding;
            } else {
                skip = (batchSize * (batchNumber - 2)) + padding;
                take = batchSize;
            }
            var result = DoGetBatch(items, response, order, filter, skip, take);
            return result;
        }

        /// <summary>
        /// transforms the queryable using ordering,
        /// filtering, selectedResponse and pagination
        /// </summary>
        /// <param name="items">the main set to iterate through in batches</param>
        /// <param name="response">the action to transform to requested response</param>
        /// <param name="order">applies the ordering to the items</param>
        /// <param name="filter"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        protected IQueryable<TResult> DoGetBatch (
            IQueryable<TModel> items,
            Expression<Func<TModel, TResult>> response,
            Func<IQueryable<TModel>, IOrderedQueryable<TModel>> order,
            Expression<Func<TModel, bool>> filter,
            int skip,
            int take
        )
        {
            var query = items;

            if (null != filter) {
                query = query.Where(filter);
            }

            if (null != order) {
                query = order(query);
            }

            query = query.Skip(skip).Take(take);
            var result = query.Select(response);
            return result;
        }

        /// <summary>
        /// rewinds the enumeration process
        /// </summary>
		public virtual void Reset ()
        {
            CurrentBatchIndex = 0;
            CurrentItems = null;
        }

        /// <summary>
        /// disposes the enumerator resources
        /// </summary>
        protected virtual void Dispose (bool disposing)
        {
            if (!this.Disposed) {
                if (disposing) {
                    Items = null;
                    Order = null;
                    Response = null;
                    CurrentBatchIndex = 0;
                    CurrentItems = null;
                    Filter = null;
                }
            }
            Disposed = true;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose ()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
