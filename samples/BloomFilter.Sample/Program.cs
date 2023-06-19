using Probabilistic.Structures.BloomFilterImpl;

BloomFilter<int> bf = new BloomFilter<int>(0.001, 1000);

bf.Add(1);
Console.WriteLine($"1 exists ? {bf.Exists(1)}");
Console.WriteLine($"2 exists ? {bf.Exists(2)}");