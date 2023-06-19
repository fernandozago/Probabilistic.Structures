using BenchmarkDotNet.Running;
using Probabilistic.Structures.Benchmarks.HeavyKeeper.Benchmarks;

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
    Console.WriteLine($"\nRunning {nameof(HeavyKeeper_Simple)}");
    BenchmarkRunner.Run<HeavyKeeper_Simple>();
}

void Run_FullBench()
{
    Console.WriteLine($"\nRunning {nameof(HeavyKeeper_Simple)}");
    BenchmarkRunner.Run<HeavyKeeper_Simple>();
}