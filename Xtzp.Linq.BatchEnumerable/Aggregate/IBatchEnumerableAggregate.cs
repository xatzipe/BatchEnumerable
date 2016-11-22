using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xtzp.Linq.BatchEnumerable.Aggregate
{
    /// <summary>
    /// Iterates a List of IBatchEnumerables{TModel}
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public interface IBatchEnumerableAggregate<TModel> : IBatchEnumerable<TModel>
    {
    }

}
