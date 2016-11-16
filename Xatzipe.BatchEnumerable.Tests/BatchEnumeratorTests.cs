using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Xatzipe.BatchEnumerable.Tests
{

    [TestFixture]
    public class BatchEnumeratorTests
    {

        public IEnumerable<int> Items = new[] {
            1, 2, 3, 4, 5,
            6, 7, 8, 9, 10,
            11, 12, 13, 14, 15,
            16, 17, 18, 19, 20,
            21
        };

        [Test]
        public void TestEnumeratorFetchesCorrectBatchItems ()
        {
            var en = new BatchEnumerator<int, int>(
                Items.AsQueryable(),
                q => q,
                null,
                null,
                5
            );
            Assert.AreEqual(0, en.BatchNumber);

            Assert.True(en.MoveNext());
            Assert.AreEqual(new[] { 1, 2, 3, 4, 5 }, en.Current);
            Assert.AreEqual(1, en.BatchNumber);

            Assert.True(en.MoveNext());
            Assert.AreEqual(new[] { 6, 7, 8, 9, 10 }, en.Current);
            Assert.AreEqual(2, en.BatchNumber);

            Assert.True(en.MoveNext());
            Assert.AreEqual(new[] { 11, 12, 13, 14, 15 }, en.Current);
            Assert.AreEqual(3, en.BatchNumber);

            Assert.True(en.MoveNext());
            Assert.AreEqual(new[] { 16, 17, 18, 19, 20 }, en.Current);
            Assert.AreEqual(4, en.BatchNumber);

            Assert.True(en.MoveNext());
            Assert.AreEqual(new[] { 21 }, en.Current);
            Assert.AreEqual(5, en.BatchNumber);
            Assert.False(en.MoveNext());
        }

        [Test]
        public void TestEnumeratorFetchesCorrectBatchItemsInReverseOrder ()
        {
            var en = new BatchEnumerator<int, int>(
                Items.AsQueryable(),
                q => q,
                qq => qq.OrderByDescending(q => q),
                null,
                5
            );
            Assert.AreEqual(0, en.BatchNumber);

            Assert.True(en.MoveNext());
            Assert.AreEqual(new[] { 21, 20, 19, 18, 17 }, en.Current);
            Assert.AreEqual(1, en.BatchNumber);

            Assert.True(en.MoveNext());
            Assert.AreEqual(new[] { 16, 15, 14, 13, 12 }, en.Current);
            Assert.AreEqual(2, en.BatchNumber);

            Assert.True(en.MoveNext());
            Assert.AreEqual(new[] { 11, 10, 9, 8, 7 }, en.Current);
            Assert.AreEqual(3, en.BatchNumber);

            Assert.True(en.MoveNext());
            Assert.AreEqual(new[] { 6, 5, 4, 3, 2 }, en.Current);
            Assert.AreEqual(4, en.BatchNumber);

            Assert.True(en.MoveNext());
            Assert.AreEqual(new[] { 1 }, en.Current);
            Assert.AreEqual(5, en.BatchNumber);
            Assert.False(en.MoveNext());
        }

        [Test]
        public void TestEnumeratorFiltersEnumeration ()
        {
            var en = new BatchEnumerator<int, int>(
                Items.AsQueryable(),
                q => q,
                null,
                q => q < 4,
                5
            );
            Assert.AreEqual(0, en.BatchNumber);
            Assert.True(en.MoveNext());
            Assert.AreEqual(3, en.Current.Count());
            Assert.AreEqual(new[] { 1, 2, 3 }, en.Current);
            Assert.False(en.MoveNext());
        }

        [Test]
        public void TestResetResetsPage ()
        {
            var en = new BatchEnumerator<int, int>(
                Items.AsQueryable(),
                q => q,
                null,
                null,
                5
            );

            Assert.True(en.MoveNext());
            Assert.True(en.MoveNext());
            Assert.AreEqual(2, en.BatchNumber);
            en.Reset();
            Assert.AreEqual(0, en.BatchNumber);
        }

        [Test]
        public void TestBaseEnumeratorCurrentProperty ()
        {
            var en = new BatchEnumerator<int, int>(
                Items.AsQueryable(),
                q => q,
                null,
                null,
                5
            );
            var baseEnumerator = (IEnumerator)en;
            baseEnumerator.MoveNext();
            Assert.IsInstanceOf<IEnumerable<int>>(baseEnumerator.Current);
            var value = (IEnumerable<int>)baseEnumerator.Current;
            Assert.AreEqual(5, value.Count());
        }

        [Test]
        public void TestBatchSizeWithoutInitializationHasValueGreaterThan0 ()
        {
            var en = new BatchEnumerator<int, int>(
                Items.AsQueryable(),
                q => q,
                null,
                null
            );

            Assert.AreEqual(10, en.BatchSize);
        }

        [Test]
        public void TestSetPaddingWithNoIteration ()
        {
            var en = new BatchEnumerator<int, int>(
                Items.AsQueryable(),
                q => q,
                null,
                null,
                5
            );
            en.Padding = 6;
            Assert.AreEqual(6, en.Padding);
        }

        [Test]
        public void TestSetPaddingWithNoIterationAfterReset ()
        {
            var en = new BatchEnumerator<int, int>(
                Items.AsQueryable(),
                q => q,
                null,
                null,
                5
            );
            en.MoveNext();
            en.Reset();
            en.Padding = 6;
            Assert.AreEqual(6, en.Padding);
        }

        [Test]
        public void TestSetPaddingInsideIterationThrowsException ()
        {
            Assert.Throws<ArgumentException>(() => {
                var en = new BatchEnumerator<int, int>(
                Items.AsQueryable(),
                q => q,
                null,
                null,
                5
            );
                en.MoveNext();
                en.Padding = 6;
            });
        }

        [Test]
        public void TestBatchSizeWithInitializationHasCorrectValue ()
        {
            var en = new BatchEnumerator<int, int>(
                Items.AsQueryable(),
                q => q,
                null,
                null,
                5
            );

            Assert.AreEqual(5, en.BatchSize);
        }

        [Test]
        public void TestEnumeratorThrowsExceptionWithNullResponseArgument ()
        {

            Assert.Throws<ArgumentNullException>(() => {
                var en = new BatchEnumerator<int, int>(
                    Items.AsQueryable(),
                    null,
                    null,
                    null
                );
            });
        }

        [Test]
        public void TestEnumeratorThrowsExceptionWithNullItems ()
        {
            Assert.Throws<ArgumentNullException>(() => {
                var en = new BatchEnumerator<int, int>(
                    null,
                    q => q,
                    null,
                    null,
                    5
                );
            });
        }

        [Test]
        [TestCase("Items")]
        [TestCase("Order")]
        [TestCase("Response")]
        [TestCase("CurrentItems")]
        [TestCase("Filter")]
        public void TestDispose (string propertyName)
        {
            var en = new BatchEnumerator<int, int>(
                Items.AsQueryable(),
                q => q,
                qq => qq.OrderBy(q => q),
                null,
                5
            );

            en.Dispose();

            var property = en.GetType().GetField(
                propertyName,
                BindingFlags.NonPublic | BindingFlags.Instance
            );
            var propertyValue = property.GetValue(en);
            Assert.AreEqual(null, propertyValue);
        }
    }
}
