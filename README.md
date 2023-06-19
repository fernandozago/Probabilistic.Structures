# Probabilistic.Structures

- Added TopK Probabilistic Structure
```C#

/// <param name="k">Size of the Top items that you want be tracked</param>
/// <param name="depth">How many arrays you want to create to keep track of items fingerprints</param>
/// <param name="width">How many buckets we have inside each array</param>
/// <param name="decay">How often the items will be decaying</param>
TopK<int> topk = new TopK<int>(4, 5, 1000, 0.9);

// Add as many as you want
topk.Add(5);
topk.Add(4);
topk.Add(4);
topk.Add(3);
topk.Add(3);
topk.Add(3);
topk.Add(2);
topk.Add(2);
topk.Add(2);
topk.Add(2);
topk.Add(1);
topk.Add(1);
topk.Add(1);
topk.Add(1);
topk.Add(1);

//{ Value: '1', Count: 5 }
//{ Value: '2', Count: 4 }
//{ Value: '3', Count: 3 }
//{ Value: '4', Count: 2 }
foreach (var item in topk.Top())
{
    Console.WriteLine(item);
}

```
