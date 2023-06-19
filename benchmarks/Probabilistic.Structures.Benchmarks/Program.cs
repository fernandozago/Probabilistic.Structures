﻿// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Running;
using Probabilistic.Structures.Benchmarks;

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
    Console.WriteLine($"\nRunning {nameof(TopK_SimpleBenchmark)}");
    BenchmarkRunner.Run<TopK_SimpleBenchmark>();
}

void Run_FullBench()
{
    Console.WriteLine($"\nRunning {nameof(TopK_FullBench)}");
    BenchmarkRunner.Run<TopK_FullBench>();
}