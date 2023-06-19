using BenchmarkDotNet.Running;
using Probabilistic.Structures.Benchmarks;
using Probabilistic.Structures.Benchmarks.BloomFilter.Benchmarks;
using Probabilistic.Structures.Benchmarks.HeavyKeeper.Benchmarks;

BenchTypes type;
do
{
    Console.Write("Which test you want to run? ( B=BloomFilter / H=HeavyKeeper / E=Exit )? -> ");
    var c = char.ToUpperInvariant(Console.ReadKey().KeyChar);
    if (c == 'B')
    {
        type = BenchTypes.BloomFilter;
        break;
    }

    if (c == 'H')
    {
        type = BenchTypes.HeavyKeeper;
        break;
    }

    Console.WriteLine();
} while (true);

Console.WriteLine(type.ToString());

do
{
    Console.Write("Which test you want to run? ( F=Full / S=Simple / E=Exit )? -> ");
    switch (char.ToUpperInvariant(Console.ReadKey().KeyChar))
    {
        case 'F':
            Run_FullBench();
            return;
        case 'S':
            RunSimpleBench();
            return;
        case 'E':
            return;
    }

    Console.WriteLine();
} while (true);


void RunSimpleBench()
{
    switch (type)
    {
        case BenchTypes.BloomFilter:
            Console.WriteLine($"\nRunning {nameof(BloomFilter_Simple)}");
            BenchmarkRunner.Run<BloomFilter_Simple>();
            break;
        case BenchTypes.HeavyKeeper:
            Console.WriteLine($"\nRunning {nameof(HeavyKeeper_Simple)}");
            BenchmarkRunner.Run<HeavyKeeper_Simple>();
            break;
    }
}

void Run_FullBench()
{
    switch (type)
    {
        case BenchTypes.BloomFilter:
            Console.WriteLine($"\nRunning {nameof(BloomFilter_Full)}");
            BenchmarkRunner.Run<BloomFilter_Full>();
            break;
        case BenchTypes.HeavyKeeper:
            Console.WriteLine($"\nRunning {nameof(HeavyKeeper_Full)}");
            BenchmarkRunner.Run<HeavyKeeper_Full>();
            break;
    }
    
}