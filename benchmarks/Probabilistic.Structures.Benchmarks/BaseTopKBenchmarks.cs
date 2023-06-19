using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Probabilistic.Structures.HeavyKeeperImpl;

namespace Probabilistic.Structures.Benchmarks;

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net50)]
[SimpleJob(RuntimeMoniker.Net60)]
[SimpleJob(RuntimeMoniker.Net70)]
public abstract class BaseTopKBenchmarks
{

#nullable disable
    private HeavyKeeper<int> topK;
    private int _firstItem;
    private int _lastItem;

    private readonly Random _random =
#if NET5_0
        new (Guid.NewGuid().GetHashCode());
#else
        Random.Shared;
#endif

    public abstract int K { get; set; }

    public abstract int Depth { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        topK = new HeavyKeeper<int>(K, Depth, K * 100, 0.9);
        Parallel.For(0, K * 100, _ =>
        {
            topK.Add(_random.Next(0, K * 2));
            topK.Add(K / 2);
        });

        _firstItem = topK.Top().First().Data;
        _lastItem = topK.Top().Last().Data;
        Parallel.For(1, topK.Top().Last().Count, _ =>
        {
            topK.Add(K * 2);
        });

        Console.WriteLine($"Top: {K} - Depth: {Depth} - Width: {K * 100}");
        foreach (var i in topK.Top())
        {
            Console.WriteLine(i.ToString());
        }
        Console.WriteLine($"First: {_firstItem}");
        Console.WriteLine($"Last: {_lastItem}");
        Console.WriteLine($"CountNonHeap [{K * 2}]: {topK.Count(K * 2)}");
    }

    [GlobalCleanup]
    public async Task CleanUp()
    {
        await Task.Delay(TimeSpan.FromSeconds(2));
        Console.WriteLine($"Top: {K} - Depth: {Depth} - Width: {K * 100}");
        foreach (var i in topK.Top())
        {
            Console.WriteLine(i.ToString());
        }
        await Task.Delay(TimeSpan.FromSeconds(2));
    }

    [Benchmark]
    public void Add()
    {
        topK.Add(K / 2);
    }

    [Benchmark]
    public void AddRandomRange()
    {
        topK.Add(_random.Next(0, K * 2));
    }

    [Benchmark]
    public void AddRandomFull()
    {
        topK.Add(_random.Next(int.MinValue, int.MaxValue));
    }

    [Benchmark]
    public void CountTopFirst()
    {
        topK.Count(_firstItem);
    }

    [Benchmark]
    public void CountTopLast()
    {
        topK.Count(_lastItem);
    }

    [Benchmark]
    public void CountNonHeap()
    {
        topK.Count(K * 2);
    }

}