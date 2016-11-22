using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xtzp.Linq.BatchEnumerable.Tests
{
    [TestFixture]
    public class BatchEnumerableExtensionsWithSameTypeTests
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
            var iterator = Items.Batch<int>(
                qq => qq.OrderBy(q => q),
                null,
                5
            );
            Assert.AreEqual(5, iterator.BatchSize);
            foreach (var items in iterator) {
                var i = new[] {
                    (count * 5) + 1,
                    (count * 5) + 2,
                    (count * 5) + 3,
                    (count * 5) + 4,
                    (count * 5) + 5,
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
            var iterator = Items.Batch<int>(
                qq => qq.OrderBy(q => q),
                null,
                5
            );
            var enumerator = iterator.GetEnumerator();

            Assert.IsInstanceOf<BatchEnumerator<int>>(enumerator);
            Assert.AreEqual(5, ((BatchEnumerator<int>)enumerator).BatchSize);
            while (enumerator.MoveNext()) {
                var i = new[] {
                    (count * 5) + 1,
                    (count * 5) + 2,
                    (count * 5) + 3,
                    (count * 5) + 4,
                    (count * 5) + 5,
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
            var iterator = Items.Batch<int>(
                qq => qq.OrderBy(q => q),
                null,
                5
            );
            var baseEnumerable = (IEnumerable)iterator;
            var enumerator = baseEnumerable.GetEnumerator();
            while (enumerator.MoveNext()) {
                var i = new[] {
                    (count * 5) + 1,    
                    (count * 5) + 2,    
                    (count * 5) + 3,    
                    (count * 5) + 4,    
                    (count * 5) + 5,    
                };
                var items = (IEnumerable<int>)enumerator.Current;
                Assert.AreEqual(i, items);
                count++;
                Assert.AreEqual(count, iterator.BatchNumber);
            }
        }
    }
}
