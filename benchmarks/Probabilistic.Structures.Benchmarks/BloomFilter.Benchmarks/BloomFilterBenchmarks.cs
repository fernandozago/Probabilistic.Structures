using BenchmarkDotNet.Attributes;

namespace Probabilistic.Structures.Benchmarks.BloomFilter.Benchmarks
{
    public class BloomFilter_Simple : BaseBloomFilter
    {
        [Params(0.01)]
        public override double ErrorRate { get; set; }

        [Params(500)]
        public override int Items { get; set; }
    }


    public class BloomFilter_Full : BaseBloomFilter
    {
        [Params(0.1, 0.001, 0.0001)]
        public override double ErrorRate { get; set; }

        [Params(1000, 2000, 5000)]
        public override int Items { get; set; }
    }
}
