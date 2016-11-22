using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Xtzp.Linq.BatchEnumerable.Multiple;

namespace Xtzp.Linq.BatchEnumerable.Tests.Multiple
{

    [TestFixture]
    public class MultipleBatchEnumerableTests
    {

        [Test]
        public void TestContructorThrowsExceptionUponNullItemList ()
        {
            Assert.Throws<ArgumentNullException>(() => {
                var en = new MultipleBatchEnumerator<int, int>(
                    null,
                    q => q
                );
            }, "Items list cannot be empty");
        }

        [Test]
        public void TestContructorThrowsExceptionUponEmptyItemList ()
        {
            Assert.Throws<ArgumentNullException>(() => {
                var en = new MultipleBatchEnumerator<int, int>(
                    new IQueryable<int>[0],
                    q => q
                );
            }, "Items list cannot be empty");
        }

        [Test]
        public void TestContructorThrowsExceptionUponNullSelectedResponse ()
        {
            Assert.Throws<ArgumentNullException>(() => {
                var en = new MultipleBatchEnumerator<int, int>(
                    new[] {
                        new [] {1 }.AsQueryable()
                    },
                    null
                );
            }, "Selected Response cannot be empty");
        }

        [Test]
        public void TestMultipleBatchEnumeratorFetchesCorrectBatchItems1 ()
        {
            var itemList = new[] {
                new [] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 }.AsQueryable(),
                new [] { 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22 }.AsQueryable()
            };
            var en = new MultipleBatchEnumerator<int, int>(
                itemList,
                q => q,
                qq => qq.OrderBy(q => q),
                null,
                5
            );
            Assert.AreEqual(0, en.BatchNumber);

            Assert.True(en.MoveNext());
            Assert.AreEqual(5, en.Current.Count());
            Assert.AreEqual(new[] { 1, 2, 3, 4, 5 }, en.Current);
            Assert.AreEqual(1, en.BatchNumber);

            Assert.True(en.MoveNext());
            Assert.AreEqual(5, en.Current.Count());
            Assert.AreEqual(new[] { 6, 7, 8, 9, 10 }, en.Current);
            Assert.AreEqual(2, en.BatchNumber);

            Assert.True(en.MoveNext());
            Assert.AreEqual(5, en.Current.Count());
            Assert.AreEqual(new[] { 11, 12, 12, 13, 14 }, en.Current);
            Assert.AreEqual(3, en.BatchNumber);

            Assert.True(en.MoveNext());
            Assert.AreEqual(5, en.Current.Count());
            Assert.AreEqual(new[] { 15, 16, 17, 18, 19, }, en.Current);
            Assert.AreEqual(4, en.BatchNumber);

            Assert.True(en.MoveNext());
            Assert.AreEqual(3, en.Current.Count());
            Assert.AreEqual(new[] { 20, 21, 22 }, en.Current);
            Assert.AreEqual(5, en.BatchNumber);
            Assert.False(en.MoveNext());
        }


        [Test]
        public void TestMultipleBatchEnumeratorFetchesCorrectBatchItems2 ()
        {
            var itemList = new[] {
                new [] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }.AsQueryable(),
                new [] { 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22 }.AsQueryable()
            };
            var en = new MultipleBatchEnumerator<int, int>(
                itemList,
                q => q,
                qq => qq.OrderBy(q => q),
                null,
                5
            );
            Assert.AreEqual(0, en.BatchNumber);

            Assert.True(en.MoveNext());
            Assert.AreEqual(5, en.Current.Count());
            Assert.AreEqual(new[] { 1, 2, 3, 4, 5 }, en.Current);
            Assert.AreEqual(1, en.BatchNumber);

            Assert.True(en.MoveNext());
            Assert.AreEqual(5, en.Current.Count());
            Assert.AreEqual(new[] { 6, 7, 8, 9, 10 }, en.Current);
            Assert.AreEqual(2, en.BatchNumber);

            Assert.True(en.MoveNext());
            Assert.AreEqual(5, en.Current.Count());
            Assert.AreEqual(new[] { 12, 13, 14, 15, 16 }, en.Current);
            Assert.AreEqual(3, en.BatchNumber);

            Assert.True(en.MoveNext());
            Assert.AreEqual(5, en.Current.Count());
            Assert.AreEqual(new[] { 17, 18, 19, 20, 21 }, en.Current);
            Assert.AreEqual(4, en.BatchNumber);

            Assert.True(en.MoveNext());
            Assert.AreEqual(1, en.Current.Count());
            Assert.AreEqual(new[] { 22 }, en.Current);
            Assert.AreEqual(5, en.BatchNumber);
            Assert.False(en.MoveNext());
        }

        [Test]
        public void TestMultipleBatchEnumeratorFetchesCorrectBatchItems3 ()
        {
            var itemList = new[] {
                new [] { 1, 2, 3 }.AsQueryable(),
                new [] { 4, 5, 6, }.AsQueryable(),
                new [] { 7, 8, 9, 10, 11, 12 }.AsQueryable(),
                new int[] { }.AsQueryable(),
                new [] { 12, 13, 14, 15, 16, 17, 18, }.AsQueryable(),
                new [] { 17, 18, 19, 20, 21, 22, 24 }.AsQueryable(),
                new int[] { }.AsQueryable(),
                new int[] { 42 }.AsQueryable(),
            };
            var en = new MultipleBatchEnumerator<int, int>(
                itemList,
                q => q,
                qq => qq.OrderBy(q => q),
                null,
                5
            );
            Assert.AreEqual(0, en.BatchNumber);

            Assert.True(en.MoveNext());
            Assert.AreEqual(5, en.Current.Count());
            Assert.AreEqual(new[] { 1, 2, 3, 4, 5 }, en.Current);
            Assert.AreEqual(1, en.BatchNumber);

            Assert.True(en.MoveNext());
            Assert.AreEqual(5, en.Current.Count());
            Assert.AreEqual(new[] { 6, 7, 8, 9, 10 }, en.Current);
            Assert.AreEqual(2, en.BatchNumber);

            Assert.True(en.MoveNext());
            Assert.AreEqual(5, en.Current.Count());
            Assert.AreEqual(new[] { 11, 12, 12, 13, 14 }, en.Current);
            Assert.AreEqual(3, en.BatchNumber);

            Assert.True(en.MoveNext());
            Assert.AreEqual(5, en.Current.Count());
            Assert.AreEqual(new[] { 15, 16, 17, 18, 17 }, en.Current);
            Assert.AreEqual(4, en.BatchNumber);

            Assert.True(en.MoveNext());
            Assert.AreEqual(5, en.Current.Count());
            Assert.AreEqual(new[] { 18, 19, 20, 21, 22 }, en.Current);
            Assert.AreEqual(5, en.BatchNumber);

            Assert.True(en.MoveNext());
            Assert.AreEqual(2, en.Current.Count());
            Assert.AreEqual(new[] { 24, 42 }, en.Current);
            Assert.AreEqual(6, en.BatchNumber);

            Assert.False(en.MoveNext());
        }
    }
}
