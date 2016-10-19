using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Xatzipe.BatchEnumerable.Multiple
{

    /// <summary>
    /// AbstractMultipleBatchEnumerator class
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public abstract class AbstractMultipleBatchEnumerator<TModel, TResult> : AbstractBatchEnumerator<TModel, TResult>
    {

        /// <summary>
        /// 
        /// </summary>
        protected IQueryable<TModel>[] ItemList;

        /// <summary>
        /// tmp storage durring CurrentBatch creation
        /// </summary>
        protected IEnumerable<TResult> PreCurrentBatch = System.Linq.Enumerable.Empty<TResult>();

        /// <summary>
        /// 
        /// </summary>
        protected int CurrentListItem;

        /// <summary>
        /// Stores the batch number for the current batch list enumeration
        /// </summary>
        protected int CurrentBatchNumberLocal;

        /// <summary>
        /// AbstractMultipleBatchEnumerator contructor
        /// </summary>
        /// <param name="itemList"></param>
        /// <param name="response"></param>
        /// <param name="order"></param>
        /// <param name="filter"></param>
        /// <param name="batchSize"></param>
        public AbstractMultipleBatchEnumerator (
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
            if (null == itemList) {
                throw new ArgumentNullException(nameof(itemList), "Items list cannot be empty");
            }

            if (0 == itemList.Count()) {
                throw new ArgumentNullException(nameof(itemList), "Items list cannot be empty");
            }

            ItemList = itemList.ToArray();
        }

        /// <summary>
        ///  returns true if the current enumerator is the last one in the array
        /// </summary>
        protected bool AllItemsPassed {
            get { return ItemList.Count() == (CurrentListItem + 1); }
        }

        /// <summary>
        /// 
        /// </summary>
        protected IQueryable<TModel> CurrentBatchItems {
            get { return ItemList[CurrentListItem]; }
        }
    }
}
