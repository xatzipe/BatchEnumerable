using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xtzp.Linq.BatchEnumerable.Db.Tests.TestData
{
    public class TestDbContext : DbContext
    {

        public TestDbContext () : base("connection.for.tests")
        {
            Database.SetInitializer<TestDbContext>(new TestDbInitializer());
        }

        public DbSet<TestDbModel> TestDbModels { get; set; }
        public DbSet<TestParent> TestParents { get; set; }
        public DbSet<TestChild> TestChildren { get; set; }
    }
}
