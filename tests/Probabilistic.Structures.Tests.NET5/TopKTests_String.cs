﻿using Probabilistic.Structures.TopKImpl;

namespace TopKVal.Tests
{
    [TestFixture]
    public class TopKTests_String
    {
        private TopK<string> subject;

        [SetUp]
        public void Setup()
        {
            subject = new(k: 3, depth: 4, width: 100, decay: 1.05);
        }

        [Test]
        public void Count_ReturnsZero_WhenQueriedForUnknownInteger()
        {
            Assert.That(subject.Count("foo"), Is.EqualTo(0));
        }

        [Test]
        public void Count_ReturnsOne_ForSingleItem()
        {
            subject.Add("foo");
            Assert.That(subject.Count("foo"), Is.EqualTo(1));
        }

        [Test]
        public void Count_ReturnsBiggerCount_ForMultipleItems()
        {
            subject.Add("foo");
            subject.Add("foo");
            subject.Add("foo");
            Assert.That(subject.Count("foo"), Is.EqualTo(3));
        }

        [Test]
        public void Count_MultipleIntegersCanBeCounted()
        {
            subject.Add("foo");
            subject.Add("foo");
            subject.Add("foo");
            subject.Add("bar");
            subject.Add("bar");
            subject.Add("baz");
            Assert.Multiple(() =>
            {
                Assert.That(subject.Count("foo"), Is.EqualTo(3));
                Assert.That(subject.Count("bar"), Is.EqualTo(2));
                Assert.That(subject.Count("baz"), Is.EqualTo(1));
                Assert.That(subject.Count("qux"), Is.EqualTo(0));
            });
        }

        [Test]
        public void Query_ReturnsFalse_ForEmptyTopK()
        {
            Assert.That(subject.Any("foo"), Is.False);
        }

        [Test]
        public void Query_ReturnsTrue_ForAnItemInTheTopK()
        {
            subject.Add("foo");
            subject.Add("foo");
            subject.Add("foo");
            subject.Add("foo");
            subject.Add("bar");
            subject.Add("bar");
            subject.Add("bar");
            subject.Add("baz");
            subject.Add("baz");
            subject.Add("qux");

            Assert.That(subject.Any("foo"), Is.True);
        }

        [Test]
        public void Query_ReturnsFalse_ForAnItemDroppedOutOfTheTopK()
        {
            subject.Add("foo");
            subject.Add("foo");
            subject.Add("foo");
            subject.Add("foo");
            subject.Add("bar");
            subject.Add("bar");
            subject.Add("bar");
            subject.Add("baz");
            subject.Add("baz");
            subject.Add("qux");

            Assert.That(subject.Any("qux"), Is.False);
        }

        [Test]
        public void Query_ReturnsFalse_ForAnItemNeverAddedToTheTopK()
        {
            subject.Add("foo");
            subject.Add("foo");
            subject.Add("foo");
            subject.Add("foo");
            subject.Add("bar");
            subject.Add("bar");
            subject.Add("bar");
            subject.Add("baz");
            subject.Add("baz");
            subject.Add("qux");

            Assert.That(subject.Any("quux"), Is.False);
        }

        [Test]
        public void Top_ReturnsEmptyArray_ForEmptyTopK()
        {
            var result = subject.Top().ToArray();
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void Top_ReturnsCountOfSingleItem()
        {
            subject.Add("foo");
            subject.Add("foo");
            subject.Add("foo");

            var result = subject.Top();

            Assert.That(result, Has.Length.EqualTo(1));
            Assert.That(result[0].Data, Is.EqualTo("foo"));
            Assert.That(result[0].Count, Is.EqualTo(3));
        }

        [Test]
        public void Top_ReturnsOrderedCountOfMultipleItems()
        {
            subject.Add("foo");
            subject.Add("foo");
            subject.Add("foo");
            subject.Add("bar");
            subject.Add("bar");
            subject.Add("baz");

            var result = subject.Top();

            Assert.That(result, Has.Length.EqualTo(3));
            Assert.Multiple(() =>
            {
                Assert.That(result[0].Data, Is.EqualTo("foo"));
                Assert.That(result[0].Count, Is.EqualTo(3));
                Assert.That(result[1].Data, Is.EqualTo("bar"));
                Assert.That(result[1].Count, Is.EqualTo(2));
                Assert.That(result[2].Data, Is.EqualTo("baz"));
                Assert.That(result[2].Count, Is.EqualTo(1));
            });
        }

        [Test]
        public void Top_DoesNotCountMoreThanKItems()
        {
            subject.Add("foo");
            subject.Add("foo");
            subject.Add("foo");
            subject.Add("foo");
            subject.Add("bar");
            subject.Add("bar");
            subject.Add("bar");
            subject.Add("baz");
            subject.Add("baz");
            subject.Add("qux");

            var result = subject.Top();

            Assert.That(result, Has.Length.EqualTo(3));
            Assert.Multiple(() =>
            {
                Assert.That(result[0].Data, Is.EqualTo("foo"));
                Assert.That(result[0].Count, Is.EqualTo(4));
                Assert.That(result[1].Data, Is.EqualTo("bar"));
                Assert.That(result[1].Count, Is.EqualTo(3));
                Assert.That(result[2].Data, Is.EqualTo("baz"));
                Assert.That(result[2].Count, Is.EqualTo(2));
            });
        }
    }
}