using System.Collections;
using System.Collections.Generic;

namespace Xtzp.Linq.BatchEnumerable
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public abstract class AbstractBaseBatchEnumerable<TResult> : IBatchEnumerable<TResult>
    {

        /// <summary>
        /// the enumerator that object uses to iterate through in batches
        /// </summary>
        protected IBatchEnumerator<TResult> Enumerator;

        /// <summary>
        /// returns the current batch number 
        /// </summary>
        public int BatchNumber {
            get { return ((IBatchEnumerator<TResult>)GetEnumerator()).BatchNumber; }
        }

        /// <summary>
        /// returns the size of the batch
        /// </summary>
        public int BatchSize {
            get { return ((IBatchEnumerator<TResult>)GetEnumerator()).BatchSize; }
        }

        /// <summary>
        /// returns the enumerator
        /// </summary>
        /// <returns></returns>
        public IEnumerator<IEnumerable<TResult>> GetEnumerator ()
        {
            if (null == Enumerator || true == Enumerator.Disposed) {
                SetEnumerator(out Enumerator);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumerator"></param>
        protected abstract void SetEnumerator (out IBatchEnumerator<TResult> enumerator);

    }
}
