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
    public class MultipleBatchEnumerator<TModel, TResult> : AbstractMultipleBatchEnumerator<TModel, TResult>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemList"></param>
        /// <param name="response"></param>
        /// <param name="order"></param>
        /// <param name="filter"></param>
        /// <param name="batchSize"></param>
        public MultipleBatchEnumerator (
            IEnumerable<IQueryable<TModel>> itemList,
            Expression<Func<TModel, TResult>> response,
            Func<IQueryable<TModel>, IOrderedQueryable<TModel>> order = null,
            Expression<Func<TModel, bool>> filter = null,
            int batchSize = 10
        ) : base(
            itemList,
            response,
            order,
            filter,
            batchSize
        )
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool MoveNext ()
        {
            CurrentBatchIndex++;
            CurrentBatchNumberLocal++;
            var response = SetΝextBatch();
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected bool SetΝextBatch ()
        {

            TResult[] p;
            if (0 == Padding) {
                p = GetBatch(CurrentBatchItems, Response, Order, Filter, CurrentBatchNumberLocal, BatchSizeLocal).ToArray();
            } else {
                p = GetBatchWithPadding(CurrentBatchItems, Response, Order, Filter, CurrentBatchNumberLocal, BatchSizeLocal, Padding).ToArray();
            }
            PreCurrentBatch = PreCurrentBatch.Concat(p).ToArray();
            var count = PreCurrentBatch.Count();
            if (count == BatchSizeLocal) {
                CurrentItems = PreCurrentBatch;
                PreCurrentBatch = System.Linq.Enumerable.Empty<TResult>().ToArray();
                return true;
            }

            if (0 == count && true == AllItemsPassed) {
                return false;
            }

            if (count < BatchSizeLocal && AllItemsPassed) {
                //return true one last time

                CurrentItems = PreCurrentBatch;
                PreCurrentBatch = System.Linq.Enumerable.Empty<TResult>().ToArray();
                return true;
            }

            CurrentListItem++;
            CurrentBatchNumberLocal = 1;
            PaddingLocal = BatchSizeLocal - count;
            return SetΝextBatch();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose (bool disposing)
        {
            ItemList = null;
            PreCurrentBatch = System.Linq.Enumerable.Empty<TResult>();
            CurrentListItem = 0;
            CurrentBatchNumberLocal = 0;
            base.Dispose(disposing);
        }
    }
}
