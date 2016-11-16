using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xatzipe.BatchEnumerable
{
    /// <summary>
    /// Iterates enumerable in batches
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public interface IBatchEnumerable<TResult> : IEnumerable<IEnumerable<TResult>>
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

    }

    /// <summary>
    /// Iterates the enumerable in batches, transforming it to the given type
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public interface IBatchEnumerable<TModel, TResult> : IBatchEnumerable<TResult>
    {
    }
}
