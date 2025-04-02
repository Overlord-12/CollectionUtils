using NUnit.Framework;
using Utils.Collections;

namespace Utils.Test.Collections
{
    [TestFixture]
    public class MultipleKeyTreeGenericTest
    {
        [Test]
        public void TestWithStringKeys()
        {
            MultipleKeyTree<string, string> tree = new MultipleKeyTree<string, string>();
            string[] keys = { "level1", "level2", "level3" };
            string value = "test value";

            tree.Add(keys, value);
            bool found = tree.TryGetValue(keys, out string retrievedValue);

            Assert.IsTrue(found);
            Assert.AreEqual(value, retrievedValue);
        }

        [Test]
        public void TestWithIntKeys()
        {
            MultipleKeyTree<int, string> tree = new MultipleKeyTree<int, string>();
            int[] keys = { 1, 2, 3 };
            string value = "test value";

            tree.Add(keys, value);
            bool found = tree.TryGetValue(keys, out string retrievedValue);

            Assert.IsTrue(found);
            Assert.AreEqual(value, retrievedValue);
        }

        [Test]
        public void TestRemoveWithStringKeys()
        {
            MultipleKeyTree<string, string> tree = new MultipleKeyTree<string, string>();
            string[] keys = { "level1", "level2", "level3" };
            string value = "test value";

            tree.Add(keys, value);
            bool foundBefore = tree.TryGetValue(keys, out string _);
            tree.Remove(keys);
            bool foundAfter = tree.TryGetValue(keys, out string _);

            Assert.IsTrue(foundBefore);
            Assert.IsFalse(foundAfter);
        }

        [Test]
        public void TestClearWithIntKeys()
        {
            MultipleKeyTree<int, string> tree = new MultipleKeyTree<int, string>();
            int[] keys1 = { 1, 2, 3 };
            int[] keys2 = { 4, 5, 6 };
            string value = "test value";

            tree.Add(keys1, value);
            tree.Add(keys2, value);
            bool foundBefore = tree.TryGetValue(keys1, out string _);
            tree.Clear();
            bool foundAfter = tree.TryGetValue(keys1, out string _);

            Assert.IsTrue(foundBefore);
            Assert.IsFalse(foundAfter);
        }

        [Test]
        public void TestPerformanceWithValueTypes()
        {
            MultipleKeyTree<int, string> tree = new MultipleKeyTree<int, string>();
            int iterationsCount = 10000;
            List<int[]> keysList = new List<int[]>();

            for (int i = 0; i < iterationsCount; i++)
            {
                keysList.Add(new[] { i, i * 2, i * 3 });
            }

            for (int i = 0; i < iterationsCount; i++)
            {
                tree.Add(keysList[i], $"value_{i}");
            }

            for (int i = 0; i < iterationsCount; i++)
            {
                bool found = tree.TryGetValue(keysList[i], out string value);
                Assert.IsTrue(found);
                Assert.AreEqual($"value_{i}", value);
            }

            for (int i = 0; i < iterationsCount; i++)
            {
                tree.Remove(keysList[i]);
                bool found = tree.TryGetValue(keysList[i], out string _);
                Assert.IsFalse(found);
            }
        }
    }
}
