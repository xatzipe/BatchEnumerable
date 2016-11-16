using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xatzipe.BatchEnumerable.Db.Tests.TestData
{
    public class TestDbInitializer : DropCreateDatabaseAlways<TestDbContext>
    {
    }
}
