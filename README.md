# Probabilistic.Structures

- Added Heavy Keeper Probabilistic Data Structure
- Top-K: What are the k most frequent values in the data stream?
  * Install the package: https://www.nuget.org/packages/Probabilistic.Structures

```C#
using Probabilistic.Structures.TopKImpl;

/// <param name="k">Size of the Top items that you want be tracked</param>
/// <param name="depth">How many arrays you want to create to keep track of items fingerprints</param>
/// <param name="width">How many buckets we have inside each array</param>
/// <param name="decay">How often the items will be decaying</param>
HeavyKeeper<int> topk = new HeavyKeeper<int>(4, 5, 1000, 0.9);

// Add as many as you want
for (var i = 0; i <= 5; i++)
    for (var z = 1; z <= i; z++)
    {
        topk.Add(z);
    }

//{ Value: '1', Count: 5 }
//{ Value: '2', Count: 4 }
//{ Value: '3', Count: 3 }
//{ Value: '4', Count: 2 }
foreach (var item in topk.Top())
{
    Console.WriteLine(item);
}
```
![image](https://github.com/fernandozago/Probabilistic.Structures/assets/12010709/7c4f1450-867b-4978-9b1e-066ad7a32352)

- Added Bloom Filter Probabilistic Data Structure
  * Install the package: https://www.nuget.org/packages/Probabilistic.Structures

```C#
using Probabilistic.Structures.BloomFilterImpl;

BloomFilter<int> bf = new BloomFilter<int>(0.001, 1000);

bf.Add(1);
Console.WriteLine($"1 exists ? {bf.Exists(1)}"); //Possibly
Console.WriteLine($"2 exists ? {bf.Exists(2)}"); //No
```
![image](https://github.com/fernandozago/Probabilistic.Structures/assets/12010709/50866c4a-7b24-4d9e-aac9-558191291ab4)


