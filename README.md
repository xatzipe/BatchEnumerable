# BatchEnumerable

AppVeyor: [![AppVeyor](https://ci.appveyor.com/api/projects/status/ou790a8fl3gqtmp7?svg=true)](https://ci.appveyor.com/project/xatzipe/batchenumerable)

Batch Enumerable is a library that enhances LINQ to iterate enumerables in batches.

It includes:
  - BatchEnumerable - Takes an Enumerable and iterates it in batches
  - MultipleBatchEnumerable - Takes an List of Enumerables and iterates them in batches under a single iteration proccess
  - BatchEnumerableAggregate  - Takes an List of BatchEnumerables and iterates them in batches under a single iteration proccess

### Batch Enumerable
```cs
var itemsList = new[] {
        1, 2, 3, 4, 5,
        6, 7, 8, 9, 10,
        11, 12, 13, 14, 15,
        16, 17, 18, 19, 20,
        21, 22, 23, 24, 25,
    };
    
var batchEnumerable = new BatchEnumerable<int, int>(itemsList.AsQueryable(), i => i, null, null, 5);    

for (var j = 0; j < 4; j++) {
    Assert.AreEqual(5, batchEnumerable.Count());
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
```

### Multiple Batch Enumerable

```cs

var itemList = new[] {
    new [] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 }.AsQueryable(),
    new [] { 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22 }.AsQueryable()
};

var en = new MultipleBatchEnumerator<int, int>(itemList, q => q, qq => qq.OrderBy(q => q), null, 5);

Assert.True(en.MoveNext());
Assert.AreEqual(new[] { 1, 2, 3, 4, 5 }, en.Current);

Assert.True(en.MoveNext());
Assert.AreEqual(new[] { 6, 7, 8, 9, 10 }, en.Current);

Assert.True(en.MoveNext());
Assert.AreEqual(new[] { 11, 12, 12, 13, 14 }, en.Current);
            
Assert.True(en.MoveNext());
Assert.AreEqual(new[] { 15, 16, 17, 18, 19, }, en.Current);

Assert.True(en.MoveNext());
Assert.AreEqual(new[] { 20, 21, 22 }, en.Current);

Assert.False(en.MoveNext());
```

### Db Batch Enumerable

```cs

var items = Context.Batch<TestDbModel, string>(ot => ot.OrderBy(t => t.Id), t => t.Name, null, 100000);
var counter = 0;
foreach (var itemBatch in items) {
    Assert.AreEqual(100000, itemBatch.Count());
}
            
```

### Batch Enumerable Aggregate

```cs
var parentBatch = Context.Batch<TestParent, string>( pp => pp.OrderBy(p => p.Id),
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

foreach (var b in aggregator) {
    ...
}
```