using BenchmarkDotNet.Attributes;

namespace Probabilistic.Structures.Benchmarks;

public class TopK_SimpleBenchmark : BaseTopKBenchmarks
{
    [Params(10)]
    public override int K { get; set; }

    [Params(5)]
    public override int Depth { get; set; }
}

public class TopK_FullBench : BaseTopKBenchmarks
{
    [Params(10, 25, 50, 100)]
    public override int K { get; set; }

    [Params(5, 10, 20, 40)]
    public override int Depth { get; set; }
}