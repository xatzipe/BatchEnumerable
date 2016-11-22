using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Xtzp.Linq.BatchEnumerable.Db.Tests.TestData;

namespace Xtzp.Linq.BatchEnumerable.Db.Tests
{
    [TestFixture]
    public class DbBatchEnumerableAggregateTest
    {
        public TestDbContext Context;

        [OneTimeSetUp]
        public void OneTimeSetUp ()
        {
            Context = new TestDbContext();
            Seed(Context);

        }

        [OneTimeTearDown]
        public void OneTimeTearDown ()
        {
            Context.Dispose();
        }

        [Test]
        public void TestAggregateBatchIteratesInBatchesWithDbContext ()
        {
            var parentBatch = Context.Batch<TestParent, string>(
                pp => pp.OrderBy(p => p.Id),
                p => "Parent: " + p.FirstName + " " + p.LastName,
                null,
                50
            );
            var childrenBatch = Context.Batch<TestChild, string>(
                pp => pp.OrderBy(p => p.Id),
                p => "Child: " + p.FirstName + " " + p.LastName,
                null,
                50
            );
            var intList = new[] { 10, 20, 30, 40, 50, 60, 70 };
            var intBatch = intList.Batch<int, string>(i => i.ToString());
            var stringList = new[] { "TestName1", "TestName2", "TestName3", "TestName4" };
            var stringBatch = stringList.Batch<string>();
            var aggregator = Aggregate.Enumerable.AggregateBatch<string>(
                new IBatchEnumerable<string>[] {
                    parentBatch,
                    childrenBatch,
                    intBatch,
                    stringBatch,
                }
            );

            int count = 0;
            int sum = 0;
            foreach (var b in aggregator) {
                count++;
                sum += b.Count();
            }
            Assert.AreEqual(6, count);
            var expectedSum = Context.TestParents.Count() +
                Context.TestChildren.Count() +
                intList.Count() +
                stringList.Count();
            Assert.AreEqual(expectedSum, sum);
        }


        private void Seed (TestDbContext context)
        {
            if (context.TestParents.Count() == 0) {
                for (var i = 1; i <= 100; i++) {
                    context.TestParents.Add(new TestParent() {
                        FirstName = "ParentName-" + i,
                        LastName = "ParentLastName-" + i
                    });
                }
            }

            if (context.TestChildren.Count() == 0) {
                for (var i = 1; i <= 100; i++) {
                    context.TestChildren.Add(new TestChild() {
                        FirstName = "ChildName-" + i,
                        LastName = "ChildLastName-" + i
                    });
                }
            }
            context.SaveChanges();
        }
    }
}
