using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xatzipe.BatchEnumerable.Aggregate
{

    /// <summary>
    /// 
    /// </summary>
    public static class Enumerable
    {

        /// <summary>
        /// iterates all items in batches. each item has its own batch size
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static IBatchEnumerable<TResult> AggregateBatch<TModel, TResult> (
            params IBatchEnumerable<TModel, TResult>[] items
        )
        {
            return new BatchEnumerableAggregate<TResult>(items);
        }


        /// <summary>
        /// iterates all items in batches. each item has its own batch size
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static IBatchEnumerable<TModel> AggregateBatch<TModel> (
            params IBatchEnumerable<TModel>[] items
        )
        {
            return new BatchEnumerableAggregate<TModel>(items);
        }

    }
}
