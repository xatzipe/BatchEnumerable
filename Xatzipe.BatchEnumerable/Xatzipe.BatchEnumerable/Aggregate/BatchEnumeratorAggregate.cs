using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xatzipe.BatchEnumerable.Aggregate
{
    /// <summary>
    /// Batch enumerator Aggregate
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class BatchEnumeratorAggregate<TResult> : IBatchEnumeratorAggregate<TResult>
    {

        /// <summary>
        /// 
        /// </summary>
        protected IBatchEnumerable<TResult>[] Enumerables;

        /// <summary>
        /// 
        /// </summary>
        protected int CurrentIndex;

        /// <summary>
        /// 
        /// </summary>
        protected int BatchNumberLocal;

        /// <summary>
        /// 
        /// </summary>
        protected int PaddingLocal;

        /// <summary>
        /// 
        /// </summary>
        public bool Disposed { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        public BatchEnumeratorAggregate (IEnumerable<IBatchEnumerable<TResult>> items)
        {
            if (null == items) {
                throw new ArgumentNullException(nameof(items), "Items list cannot be emptry");
            }

            if (0 == items.Count()) {
                throw new ArgumentNullException(nameof(items), "Items list cannot be emptry");
            }
            Enumerables = items.ToArray();
            CurrentIndex = 0;
        }

        /// <summary>
        /// returns the enumerable based on the current array index 
        /// </summary>
        protected IBatchEnumerable<TResult> CurrentEumerable {
            get { return Enumerables[CurrentIndex]; }
        }

        /// <summary>
        ///  returns true if the current enumerator is the last one in the array
        /// </summary>
        protected bool AllItemsPassed {
            get { return Enumerables.Count() == (CurrentIndex + 1); }
        }

        /// <summary>
        /// returns the current batch
        /// </summary>
        public IEnumerable<TResult> Current {
            get { return CurrentEumerable.GetEnumerator().Current; }
        }

        /// <summary>
        /// returns the current batch
        /// </summary>
        object IEnumerator.Current {
            get { return Current; }
        }

        /// <summary>
        /// returns the size of the batch
        /// </summary>
        public int BatchSize {
            get { return CurrentEumerable.BatchSize; }
        }

        /// <summary>
        /// returns the current batch number 
        /// </summary>
        public int BatchNumber {
            get { return BatchNumberLocal; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Padding {
            get {
                return PaddingLocal;
            }

            set {
                if(0 < CurrentIndex) {
                    throw new ArgumentException("Cannot set Padding after iteration has started");
                }
                PaddingLocal = value;
            }
        }

        /// <summary>
        /// returns true if there is another batch to return 
        /// </summary>
        /// <returns></returns>
        public bool MoveNext ()
        {
            var response = CurrentEumerable.GetEnumerator().MoveNext();
            if (true == response) {
                BatchNumberLocal++;
                return true;
            }
            //response is false
            if (true == AllItemsPassed) {
                return false;
            }
            CurrentIndex++;
            return MoveNext();
        }

        /// <summary>
        /// disposes the enumerator resources
        /// </summary>
        protected virtual void Dispose (bool disposing)
        {
            if (!this.Disposed) {
                if (disposing) {
                    CurrentIndex = 0;
                    foreach (var e in Enumerables) {
                        e.GetEnumerator().Dispose();
                    }
                    Enumerables = null;
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

        /// <summary>
        /// rewinds the enumeration process
        /// </summary>
        public void Reset ()
        {
            CurrentIndex = 0;
        }
    }
}
