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
    public class DbContextBatchEnumerableExtensionsWithDifferentTypesTests
    {

        public TestDbContext Context;
        public int TableSize = 1000000;

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
        public void TestContextBatchEnumerableIteratesInBatches ()
        {
            var items = Context.Batch<TestDbModel, string>(
                ot => ot.OrderBy(t => t.Id),
                t => t.Name,
               null,
               100000
            );
            var counter = 0;
            foreach (var itemBatch in items) {
                Assert.AreEqual(100000, itemBatch.Count());
                counter++;
                Assert.AreEqual(100000, itemBatch.Count());
            }
            Assert.AreEqual(10, counter);

        }

        protected void Seed (TestDbContext context)
        {
            var totalItemsCount = Context.TestDbModels.Count();
            if (totalItemsCount > TableSize) {
                return;
            }
            int j = 0;
            while (totalItemsCount < TableSize) {
                totalItemsCount += InsertBatch(context, j);
                j++;
            }
        }

        private int InsertBatch (TestDbContext context, int j)
        {
            var names = new List<string>();
            var names2 = new List<string>();
            var names3 = new List<string>();
            var names4 = new List<string>();
            var names5 = new List<string>();
            var names6 = new List<string>();
            var names7 = new List<string>();
            var names8 = new List<string>();
            var names9 = new List<string>();
            var names10 = new List<string>();
            var names11 = new List<string>();
            var names12 = new List<string>();
            var names13 = new List<string>();
            var names14 = new List<string>();
            var names15 = new List<string>();
            var names16 = new List<string>();
            var names17 = new List<string>();
            var names18 = new List<string>();
            var names19 = new List<string>();
            var names20 = new List<string>();
            for (var i = 0; i < 1000; i++) {
                names.Add(GetName(j, i));
                names2.Add(GetName(j + 1, i));
                names3.Add(GetName(j + 2, i));
                names4.Add(GetName(j + 3, i));
                names5.Add(GetName(j + 5, i));
                names6.Add(GetName(j + 6, i));
                names7.Add(GetName(j + 7, i));
                names8.Add(GetName(j + 8, i));
                names9.Add(GetName(j + 9, i));
                names10.Add(GetName(j + 10, i));
                names11.Add(GetName(j + 11, i));
                names12.Add(GetName(j + 12, i));
                names13.Add(GetName(j + 13, i));
                names14.Add(GetName(j + 14, i));
                names15.Add(GetName(j + 15, i));
                names16.Add(GetName(j + 16, i));
                names17.Add(GetName(j + 17, i));
                names18.Add(GetName(j + 18, i));
                names19.Add(GetName(j + 19, i));
                names20.Add(GetName(j + 20, i));
            }
            context.Database.ExecuteSqlCommand(String.Format("insert into TestDbModels (Name) Values {0}", String.Join(",", names.Select(s => "('" + s + "')"))));
            context.Database.ExecuteSqlCommand(String.Format("insert into TestDbModels (Name) Values {0}", String.Join(",", names2.Select(s => "('" + s + "')"))));
            context.Database.ExecuteSqlCommand(String.Format("insert into TestDbModels (Name) Values {0}", String.Join(",", names3.Select(s => "('" + s + "')"))));
            context.Database.ExecuteSqlCommand(String.Format("insert into TestDbModels (Name) Values {0}", String.Join(",", names4.Select(s => "('" + s + "')"))));
            context.Database.ExecuteSqlCommand(String.Format("insert into TestDbModels (Name) Values {0}", String.Join(",", names5.Select(s => "('" + s + "')"))));
            context.Database.ExecuteSqlCommand(String.Format("insert into TestDbModels (Name) Values {0}", String.Join(",", names6.Select(s => "('" + s + "')"))));
            context.Database.ExecuteSqlCommand(String.Format("insert into TestDbModels (Name) Values {0}", String.Join(",", names7.Select(s => "('" + s + "')"))));
            context.Database.ExecuteSqlCommand(String.Format("insert into TestDbModels (Name) Values {0}", String.Join(",", names8.Select(s => "('" + s + "')"))));
            context.Database.ExecuteSqlCommand(String.Format("insert into TestDbModels (Name) Values {0}", String.Join(",", names9.Select(s => "('" + s + "')"))));
            context.Database.ExecuteSqlCommand(String.Format("insert into TestDbModels (Name) Values {0}", String.Join(",", names10.Select(s => "('" + s + "')"))));
            context.Database.ExecuteSqlCommand(String.Format("insert into TestDbModels (Name) Values {0}", String.Join(",", names11.Select(s => "('" + s + "')"))));
            context.Database.ExecuteSqlCommand(String.Format("insert into TestDbModels (Name) Values {0}", String.Join(",", names12.Select(s => "('" + s + "')"))));
            context.Database.ExecuteSqlCommand(String.Format("insert into TestDbModels (Name) Values {0}", String.Join(",", names13.Select(s => "('" + s + "')"))));
            context.Database.ExecuteSqlCommand(String.Format("insert into TestDbModels (Name) Values {0}", String.Join(",", names14.Select(s => "('" + s + "')"))));
            context.Database.ExecuteSqlCommand(String.Format("insert into TestDbModels (Name) Values {0}", String.Join(",", names15.Select(s => "('" + s + "')"))));
            context.Database.ExecuteSqlCommand(String.Format("insert into TestDbModels (Name) Values {0}", String.Join(",", names16.Select(s => "('" + s + "')"))));
            context.Database.ExecuteSqlCommand(String.Format("insert into TestDbModels (Name) Values {0}", String.Join(",", names17.Select(s => "('" + s + "')"))));
            context.Database.ExecuteSqlCommand(String.Format("insert into TestDbModels (Name) Values {0}", String.Join(",", names18.Select(s => "('" + s + "')"))));
            context.Database.ExecuteSqlCommand(String.Format("insert into TestDbModels (Name) Values {0}", String.Join(",", names19.Select(s => "('" + s + "')"))));
            context.Database.ExecuteSqlCommand(String.Format("insert into TestDbModels (Name) Values {0}", String.Join(",", names20.Select(s => "('" + s + "')"))));
            return 20000;
        }
        private string GetName (int j, int i)
        {
            return String.Format("TestName_{0}_{1}", j, i);
        }
    }
}
