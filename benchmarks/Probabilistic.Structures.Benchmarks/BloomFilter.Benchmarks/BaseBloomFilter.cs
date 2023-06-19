using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Probabilistic.Structures.BloomFilterImpl;

namespace Probabilistic.Structures.Benchmarks.BloomFilter.Benchmarks
{
    [MemoryDiagnoser]
    [SimpleJob(RuntimeMoniker.Net50)]
    [SimpleJob(RuntimeMoniker.Net60)]
    [SimpleJob(RuntimeMoniker.Net70)]
    public abstract class BaseBloomFilter
    {

        private BloomFilter<int> _bloom;
        private readonly Random _random =
#if NET5_0
            new(Guid.NewGuid().GetHashCode());
#else
                Random.Shared;
#endif

        public abstract double ErrorRate { get; set; }

        public abstract int Items { get; set; }

        [GlobalSetup]
        public void GlobalSetup()
        {
            _bloom = new BloomFilter<int>(ErrorRate, Items);
            _bloom.Add(1000);
        }

        [Benchmark]
        public void Add()
        {
            _bloom.Add(10);
        }

        [Benchmark]
        public void AddRandom()
        {
            _bloom.Add(_random.Next(0, Items));
        }

        [Benchmark]
        public void AddRandomFull()
        {
            _bloom.Add(_random.Next(int.MinValue, int.MaxValue));
        }

        [Benchmark]
        public void Existing()
        {
            _bloom.Exists(1000);
        }

        [Benchmark]
        public void NonExisting()
        {
            _bloom.Exists(1001);
        }

    }
}
