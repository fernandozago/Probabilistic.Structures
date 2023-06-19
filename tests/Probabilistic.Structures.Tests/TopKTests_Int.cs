using Probabilistic.Structures.HeavyKeeperImpl;

namespace TopKVal.Tests
{
    [TestFixture]
    public class TopKTests_Int
    {
        private readonly HeavyKeeper<int> subject = new(k: 3, depth: 4, width: 100, decay: 1.05);

        [Test]
        public void Count_ReturnsZero_WhenQueriedForUnknownInteger()
        {
            subject.Reset();
            Assert.That(subject.Count(1), Is.EqualTo(0));
        }

        [Test]
        public void Count_ReturnsOne_ForSingleItem()
        {
            subject.Reset();
            subject.Add(1);
            Assert.That(subject.Count(1), Is.EqualTo(1));
        }

        [Test]
        public void Count_ReturnsBiggerCount_ForMultipleItems()
        {
            subject.Reset();
            subject.Add(1);
            subject.Add(1);
            subject.Add(1);
            Assert.That(subject.Count(1), Is.EqualTo(3));
        }

        [Test]
        public void Count_MultipleIntegersCanBeCounted()
        {
            subject.Reset();
            subject.Add(1);
            subject.Add(1);
            subject.Add(1);
            subject.Add(2);
            subject.Add(2);
            subject.Add(3);
            Assert.Multiple(() =>
            {
                Assert.That(subject.Count(1), Is.EqualTo(3));
                Assert.That(subject.Count(2), Is.EqualTo(2));
                Assert.That(subject.Count(3), Is.EqualTo(1));
                Assert.That(subject.Count(4), Is.EqualTo(0));
            });
        }

        [Test]
        public void Query_ReturnsFalse_ForEmptyTopK()
        {
            subject.Reset();
            Assert.That(subject.Any(1), Is.False);
        }

        [Test]
        public void Query_ReturnsTrue_ForAnItemInTheTopK()
        {
            subject.Reset();
            subject.Add(1);
            subject.Add(1);
            subject.Add(1);
            subject.Add(1);
            subject.Add(2);
            subject.Add(2);
            subject.Add(2);
            subject.Add(3);
            subject.Add(3);
            subject.Add(4);

            Assert.That(subject.Any(1), Is.True);
        }

        [Test]
        public void Query_ReturnsFalse_ForAnItemDroppedOutOfTheTopK()
        {
            subject.Reset();
            subject.Add(1);
            subject.Add(1);
            subject.Add(1);
            subject.Add(1);
            subject.Add(2);
            subject.Add(2);
            subject.Add(2);
            subject.Add(3);
            subject.Add(3);
            subject.Add(4);

            Assert.That(subject.Any(4), Is.False);
        }

        [Test]
        public void Query_ReturnsFalse_ForAnItemNeverAddedToTheTopK()
        {
            subject.Reset();
            subject.Add(1);
            subject.Add(1);
            subject.Add(1);
            subject.Add(1);
            subject.Add(2);
            subject.Add(2);
            subject.Add(2);
            subject.Add(3);
            subject.Add(3);
            subject.Add(4);

            Assert.That(subject.Any(5), Is.False);
        }

        [Test]
        public void Top_ReturnsEmptyArray_ForEmptyTopK()
        {
            subject.Reset();
            var result = subject.Top().ToArray();
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void Top_ReturnsCountOfSingleItem()
        {
            subject.Reset();
            subject.Add(1);
            subject.Add(1);
            subject.Add(1);

            var result = subject.Top().ToArray();

            Assert.Multiple(() =>
            {
                Assert.That(result, Has.Length.EqualTo(1));
                Assert.That(result[0].Data, Is.EqualTo(1));
                Assert.That(result[0].Count, Is.EqualTo(3));
            });
        }

        [Test]
        public void Top_ReturnsOrderedCountOfMultipleItems()
        {
            subject.Reset();
            subject.Add(1);
            subject.Add(1);
            subject.Add(1);
            subject.Add(2);
            subject.Add(2);
            subject.Add(3);

            var result = subject.Top();

            Assert.That(result, Has.Length.EqualTo(3));
            Assert.Multiple(() =>
            {
                Assert.That(result[0].Data, Is.EqualTo(1));
                Assert.That(result[0].Count, Is.EqualTo(3));
                Assert.That(result[1].Data, Is.EqualTo(2));
                Assert.That(result[1].Count, Is.EqualTo(2));
                Assert.That(result[2].Data, Is.EqualTo(3));
                Assert.That(result[2].Count, Is.EqualTo(1));
            });
        }

        [Test]
        public void Top_ReturnsOrderedCountOfMultipleItems_NegativeNumbers()
        {
            subject.Reset();
            subject.Add(-1);
            subject.Add(-1);
            subject.Add(-1);
            subject.Add(-2);
            subject.Add(-2);
            subject.Add(-3);

            var result = subject.Top();

            Assert.That(result, Has.Length.EqualTo(3));
            Assert.Multiple(() =>
            {
                Assert.That(result[0].Data, Is.EqualTo(-1));
                Assert.That(result[0].Count, Is.EqualTo(3));
                Assert.That(result[1].Data, Is.EqualTo(-2));
                Assert.That(result[1].Count, Is.EqualTo(2));
                Assert.That(result[2].Data, Is.EqualTo(-3));
                Assert.That(result[2].Count, Is.EqualTo(1));
            });
        }

        [Test]
        public void Top_DoesNotCountMoreThanKItems()
        {
            subject.Reset();
            subject.Add(1);
            subject.Add(1);
            subject.Add(1);
            subject.Add(1);
            subject.Add(2);
            subject.Add(2);
            subject.Add(2);
            subject.Add(3);
            subject.Add(3);
            subject.Add(4);

            var result = subject.Top();

            Assert.That(result, Has.Length.EqualTo(3));
            Assert.Multiple(() =>
            {
                Assert.That(result[0].Data, Is.EqualTo(1));
                Assert.That(result[0].Count, Is.EqualTo(4));
                Assert.That(result[1].Data, Is.EqualTo(2));
                Assert.That(result[1].Count, Is.EqualTo(3));
                Assert.That(result[2].Data, Is.EqualTo(3));
                Assert.That(result[2].Count, Is.EqualTo(2));
            });
        }

        [Test]
        public void Top_RandomValues_InOrderOfCount()
        {
            HeavyKeeper<int> internalSubject = new(k: 3, depth: 4, width: 1000, decay: 1.05);
            const int numItems = 1000;
            var random = new Random(Guid.NewGuid().GetHashCode());

            // Generate random values
            var values = Enumerable.Range(0, numItems)
                .AsParallel()
                .Select(_ => Math.Abs(Guid.NewGuid().GetHashCode()) % 100)
                .ToList();

            // Add values to TopK
            foreach (var value in values)
            {
                internalSubject.Add(value);
            }

            // Get the top items from TopK
            var topItems = internalSubject.Top().OrderByDescending(x => x.Count).ThenBy(x => x.Data).ToArray();

            // Sort values in descending order of count
            var sortedValues = values
                .Where(x => topItems.Select(x => x.Data).Contains(x))
                .GroupBy(x => x)
                .OrderByDescending(x => x.Count()).ThenBy(x => x.Key)
                .ToDictionary(x => x.Key, x => x.Count());



            // Assert the position of each value in the topItems array matches its position in sortedValues
            for (int i = 0; i < topItems.Length; i++)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(sortedValues.ContainsKey(topItems[i].Data), Is.True);
                    Assert.That(topItems[i].Count, Is.EqualTo(sortedValues[topItems[i].Data]));
                });
            }

            internalSubject.Reset();
            Assert.That(internalSubject.Top(), Has.Length.EqualTo(0));
        }
    }
}