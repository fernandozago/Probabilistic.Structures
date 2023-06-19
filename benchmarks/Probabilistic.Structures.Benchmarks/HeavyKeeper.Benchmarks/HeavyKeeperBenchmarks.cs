using BenchmarkDotNet.Attributes;

namespace Probabilistic.Structures.Benchmarks.HeavyKeeper.Benchmarks;

public class HeavyKeeperBenchmarks : BaseHeavyKeeper
{
    [Params(10)]
    public override int K { get; set; }

    [Params(5)]
    public override int Depth { get; set; }
}

public class TopK_FullBench : BaseHeavyKeeper
{
    [Params(10, 25, 50, 100)]
    public override int K { get; set; }

    [Params(5, 10, 20, 40)]
    public override int Depth { get; set; }
}