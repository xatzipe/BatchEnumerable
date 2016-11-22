using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xtzp.Linq.BatchEnumerable.Aggregate
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
