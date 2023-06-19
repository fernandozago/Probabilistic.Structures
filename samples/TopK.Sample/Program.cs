using Probabilistic.Structures.HeavyKeeperImpl;

HeavyKeeper<int> topK = new(k: 10, depth: 5, width: 1000, decay: 0.9);

var consuming = Task.Run(async () =>
{
    int iter = 0;
    while (true)
    {
        await Task.Delay(1000);
        Console.WriteLine($"Iteration # {++iter}");
        foreach (var item in topK.Top())
        {
            Console.WriteLine(item);
        }
        Console.WriteLine();
    }
});

await Task.WhenAll(consuming, Produce(), Produce());

async Task Produce()
{
    while (true)
    {
        topK.Add(Random.Shared.Next(1, 200));
        await Task.Delay(10);
    }
}