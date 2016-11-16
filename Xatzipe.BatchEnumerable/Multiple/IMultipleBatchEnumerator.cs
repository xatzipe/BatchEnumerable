using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xatzipe.BatchEnumerable.Multiple
{
    /// <summary>
    /// Enumerator Interface for iterating a List of enumerables in batches
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public interface IMultipleBatchEnumerator<TModel> : IBatchEnumerator<TModel>
    {
    }

    /// <summary>
    /// Enumerator Interface for iterating a list of enumerable in batches
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public interface IMultipleBatchEnumerator<TModel, TResult> : IBatchEnumerator<TModel, TResult>
    {
    }
}
