using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xtzp.Linq.BatchEnumerable.Multiple
{
    /// <summary>
    /// Iterates multiple enumerables in batches
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public interface IMultipleBatchEnumerable<TModel> : IBatchEnumerable<TModel>
    {
    }

    /// <summary>
    /// Iterates multiple enumerable in batches, transforming it to the given type
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public interface IMultipleBatchEnumerable<TModel, TResult> : IBatchEnumerable<TModel, TResult>
    {
    }
}
