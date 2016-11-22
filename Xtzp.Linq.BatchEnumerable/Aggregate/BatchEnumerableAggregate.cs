using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xtzp.Linq.BatchEnumerable.Aggregate
{
    /// <summary>
    /// implementation of IBatchEnumerableAggregate{TModel}
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class BatchEnumerableAggregate<TModel> : AbstractBaseBatchEnumerable<TModel>, IBatchEnumerableAggregate<TModel>
    {

        /// <summary>
        /// 
        /// </summary>
        protected IEnumerable<IBatchEnumerable<TModel>> Items;

        /// <summary>
        /// Batch Enumerable contructor
        /// </summary>
        /// <param name="items"></param>
        public BatchEnumerableAggregate (IEnumerable<IBatchEnumerable<TModel>> items)
        {
            Items = items;
        }

        /// <summary>
        /// SetEnumerator implementation based on AbstractBaseEnumerable abstract method
        /// </summary>
        /// <param name="enumerator"></param>
        protected override void SetEnumerator (out IBatchEnumerator<TModel> enumerator)
        {
            enumerator = new BatchEnumeratorAggregate<TModel>(Items);
        }
    }
}
