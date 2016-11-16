using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xatzipe.BatchEnumerable.Tests
{
    [TestFixture]
    public class BatchEnumerableExtensionsWithDifferentTypesTests
    {

        public IEnumerable<int> Items = new[] {
            1, 2, 3, 4, 5,
            6, 7, 8, 9, 10,
            11, 12, 13, 14, 15,
            16, 17, 18, 19, 20
        };

        [Test]
        public void TestIteratorReturnsCorrectValuesUponIteration ()
        {
            var count = 0;
            var iterator = Items.Batch<int, string>(
                q => q.ToString(),
                qq => qq.OrderBy(q => q),
                null,
                5
            );
            foreach (var items in iterator) {
                var i = new[] {
                    ((count * 5) + 1).ToString(),
                    ((count * 5) + 2).ToString(),
                    ((count * 5) + 3).ToString(),
                    ((count * 5) + 4).ToString(),
                    ((count * 5) + 5).ToString(),
                };
                Assert.AreEqual(i, items);
                count++;
                Assert.AreEqual(count, iterator.BatchNumber);
            }
        }

        [Test]
        public void TestIteratorReturnsCorrectValuesWithEnumeratorIteration ()
        {
            var count = 0;
            var iterator = Items.Batch<int, string>(
                q => q.ToString(),
                qq => qq.OrderBy(q => q),
                null,
                5
            );
            var enumerator = iterator.GetEnumerator();
            while (enumerator.MoveNext()) {
                var i = new[] {
                    ((count * 5) + 1).ToString(),
                    ((count * 5) + 2).ToString(),
                    ((count * 5) + 3).ToString(),
                    ((count * 5) + 4).ToString(),
                    ((count * 5) + 5).ToString(),
                };
                var items = enumerator.Current;
                Assert.AreEqual(i, items);
                count++;
                Assert.AreEqual(count, iterator.BatchNumber);
            }
        }

        [Test]
        public void TestIteratorReturnsCorrectValuesWithBaseEnumeratorIteration ()
        {
            var count = 0;
            var iterator = Items.Batch<int, string>(
                q => q.ToString(),
                qq => qq.OrderBy(q => q),
                null,
                5
            );
            var baseEnumerable = (IEnumerable)iterator;
            var enumerator = baseEnumerable.GetEnumerator();
            while (enumerator.MoveNext()) {
                var i = new[] {
                    ((count * 5) + 1).ToString(),
                    ((count * 5) + 2).ToString(),
                    ((count * 5) + 3).ToString(),
                    ((count * 5) + 4).ToString(),
                    ((count * 5) + 5).ToString(),
                };
                var items = (IEnumerable<string>)enumerator.Current;
                Assert.AreEqual(i, items);
                count++;
                Assert.AreEqual(count, iterator.BatchNumber);
            }
        }
    }
}
