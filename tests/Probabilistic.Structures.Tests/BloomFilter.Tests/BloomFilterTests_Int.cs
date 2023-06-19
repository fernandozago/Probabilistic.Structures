using Probabilistic.Structures.BloomFilterImpl;

namespace Probabilistic.Structures.Tests.BloomFilter.Tests;

[TestFixture]
public class BloomFilterTests
{

    [Test]
    public void TestBloom_Int()
    {
        BloomFilter<int> subject = new(0.1, 4);

        for (var i = 1; i < 6; i++)
        {
            subject.Add(i);
            Assert.That(subject.Exists(i), Is.True);

            var exists = subject.Exists(i + 1);
            if (exists)
            {
                Console.WriteLine($"False Positive -- {i + 1}");
                break;
            }
            else
            {
                Assert.That(subject.Exists(i + 1), Is.False);
            }
        }
    }

    [Test]
    public void TestBloom_String()
    {
        BloomFilter<string> subject = new(0.1, 4);

        for (var i = 1; i < 10; i++)
        {
            subject.Add(i.ToString());
            Assert.That(subject.Exists(i.ToString()), Is.True);

            var exists = subject.Exists((i + 1).ToString());
            if (exists)
            {
                Console.WriteLine($"False Positive -- {i + 1}");
                break;
            }
            else
            {
                Assert.That(subject.Exists((i + 1).ToString()), Is.False);
            }
        }
    }
}