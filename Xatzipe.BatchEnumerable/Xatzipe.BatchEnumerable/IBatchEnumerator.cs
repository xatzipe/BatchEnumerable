using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xatzipe.BatchEnumerable
{
    /// <summary>
    /// Enumerator Interface for iterating and enumerable in batches
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public interface IBatchEnumerator<TModel> : IEnumerator<IEnumerable<TModel>>
    {
        /// <summary>
        /// returns the number of the batch item that can be used to iterate through
        /// </summary>
        /// <value>The page.</value>
        int BatchNumber { get; }

        /// <summary>
        /// returns the size of each batch
        /// </summary>
        /// <value>The size of the page.</value>
        int BatchSize { get; }

        /// <summary>
        /// Set, Get padding for the Enumeration
        /// </summary>
        int Padding { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool Disposed { get; }
    }

    /// <summary>
    /// Enumerator Interface for iterating and enumerable in batches
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TResult"></typeparam>
	public interface IBatchEnumerator<TModel, TResult> : IBatchEnumerator<TResult>
    {
    }
}
