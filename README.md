# Probabilistic.Structures

- Added TopK Probabilistic Structure
```C#

public class YourClass
{
  /// <param name="k">Size of the Top items that you want be tracked</param>
  /// <param name="depth">How many arrays you want to create to keep track of items fingerprints</param>
  /// <param name="width">How many buckets we have inside each array</param>
  /// <param name="decay">How often the items will be decaying</param>
  private readonly TopK<int> topk = new(k: 3, depth: 4, width: 100, decay: 0.9);

  public void AddItem(int item) 
  {
    //Add as many as you want
    topk.Add(item);
  }

  public void PrintRank() 
  {
    foreach (var item in topk.Top()) 
    {
      Console.WriteLine(item);
    }
  }
}

```
