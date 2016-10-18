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
    public class BatchEnumerableTests
    {

        public int[] ItemsList = new[] {
            1, 2, 3, 4, 5,
            6, 7, 8, 9, 10,
            11, 12, 13, 14, 15,
            16, 17, 18, 19, 20,
            21, 22, 23, 24, 25,
        };

        [Test]
        public void TestBatchEnumerableReturnsBatchSize ()
        {
            var batchEnumerable = new BatchEnumerable<int, int>(
                ItemsList.AsQueryable(),
                i => i,
                null,
                null,
                5
            );
            Assert.AreEqual(5, batchEnumerable.BatchSize);
        }

        [Test]
        public void TestBatchEnumerableReturnsCorrectNumber ()
        {

            var batchEnumerable = new BatchEnumerable<int, int>(
                ItemsList.AsQueryable(),
                i => i,
                null,
                null,
                5
            );
            Assert.AreEqual(0, batchEnumerable.BatchNumber);
        }

        [Test]
        public void TestBatchEnumerableReturnsCorrectValuesUponIteration ()
        {
            var batchEnumerable = new BatchEnumerable<int, int>(
                ItemsList.AsQueryable(),
                i => i,
                null,
                null,
                5
            );
            var count = 0;
            foreach (var items in batchEnumerable) {

                var i = new[] {
                    (count * 5) + 1,
                    (count * 5) + 2,
                    (count * 5) + 3,
                    (count * 5) + 4,
                    (count * 5) + 5,
                };
                Assert.AreEqual(i, items);
                count++;
                Assert.AreEqual(count, batchEnumerable.BatchNumber);
            }
        }

        [Test]
        public void TestBatchEnumerableReturnsCorrectValuesWithBaseEnumeratorIteration ()
        {
            var batchEnumerable = new BatchEnumerable<int, int>(
                ItemsList.AsQueryable(),
                i => i,
                null,
                null,
                5
            );

            Assert.AreEqual(5, batchEnumerable.Count());

            var count = 0;
            var baseEnumerable = (IEnumerable)batchEnumerable;
            var enumerator = baseEnumerable.GetEnumerator();

            while (enumerator.MoveNext()) {
                var i = new[] {
                    (count * 5) + 1,
                    (count * 5) + 2,
                    (count * 5) + 3,
                    (count * 5) + 4,
                    (count * 5) + 5,
                };
                Assert.AreEqual(i, enumerator.Current);
                count++;
                Assert.AreEqual(count, batchEnumerable.BatchNumber);
            }
        }

        [Test]
        public void TestBatchEnumerableFiltersItems ()
        {
            var batchEnumerable = new BatchEnumerable<int, int>(
                ItemsList.AsQueryable(),
                i => i,
                null,
                i => i < 4,
                5
            );

            Assert.AreEqual(1, batchEnumerable.Count());
            Assert.AreEqual(new[] { 1, 2, 3 }, batchEnumerable.First());
        }
    }
}
